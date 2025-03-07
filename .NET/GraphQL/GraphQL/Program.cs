using GraphQL;
using GraphQL.Persistance;
using GraphQL.Persistance.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using Serilog;
using SevenTechnology.Modules.ReadModel.GraphQL.Types;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

builder.AddServiceDefaults();

builder.Services.AddHttpContextAccessor();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<WriteDbContext>(x =>
{
    x.UseNpgsql(builder.Configuration.GetConnectionString("Postgres"));
    x.ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
    x.ConfigureWarnings(
       b => b.Log(
           (RelationalEventId.TransactionCommitted, LogLevel.Information),
           (RelationalEventId.TransactionStarted, LogLevel.Information),
           (RelationalEventId.TransactionRolledBack, LogLevel.Information)));

    x.LogTo(s => System.Diagnostics.Debug.WriteLine(s), (eventId, logLevel) => logLevel >= LogLevel.Information
                           || eventId == RelationalEventId.TransactionStarted
                           || eventId == RelationalEventId.TransactionCommitted
                           || eventId == RelationalEventId.TransactionRolledBack)
     .EnableDetailedErrors(true)
     .EnableSensitiveDataLogging(true);
});

builder.Services.AddGraphQLServer().AddAuthorization()
    .AddType<LTreeType>()
    .AddQueryType<Query>()
    .AddProjections()
    .AddSorting()
    .AddFiltering();

builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o => {
    var key = Encoding.UTF8.GetBytes("4f1feeca525de4cab064656007da3edac7895a87ff0ea865693300fb8b6e8f9d");
    var signingKey = new SymmetricSecurityKey(key);
    o.RequireHttpsMetadata = false;
    o.SaveToken = false;
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = "https://localhost:7045",
        ValidAudience = "https://localhost:7045",
        IssuerSigningKey = signingKey,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddAuthorization();

builder.Services.AddCors();

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}
app.UseHttpsRedirection();

app.UseCors(builder =>
{
    builder.AllowAnyMethod();
    builder.AllowAnyOrigin();
    builder.AllowAnyHeader();
});

app.UseAuthentication();
app.UseAuthorization();

app.MapGraphQL();//.RequireAuthorization();

app.MapPost("migration", async () =>
{
    WriteDbContext context = app.Services.CreateScope().ServiceProvider.GetRequiredService<WriteDbContext>();
    await context.Database.MigrateAsync();

    var isArticle = context.Set<Article>().Any();
    if (!isArticle)
    {
        Article article1 = new()
        {
            Code = "Article_1",
            Name = "Article 1",
            Description = "Description 1",
            Unit = "kg"
        };
        Article article2 = new()
        {
            Code = "Article_2",
            Name = "Article 2",
            Description = "Description 2",
            Unit = "kg"
        };
        context.Set<Article>().AddRange(new[] { article1, article2 });

        Contractor contractor1 = new()
        {
            Code = "Contractor_1",
            Name = "Contractor 1",
            Description = "Description 1"
        };
        Contractor contractor2 = new()
        {
            Code = "Contractor_2",
            Name = "Contractor 2",
            Description = "Description 2"
        };
        context.Set<Contractor>().AddRange(new[] { contractor1, contractor2 });

        Document document1 = new()
        {
            Number = "Document_1",
            ExecuteDate = DateTime.Now.ToUniversalTime(),
            Description = "Description 1",
            Contractor = contractor1,
        };
        Document document2 = new()
        {
            Number = "Document_2",
            ExecuteDate = DateTime.Now.AddDays(1).ToUniversalTime(),
            Description = "Description 2",
            Contractor = contractor2,
        };
        context.Set<Document>().AddRange(new[] { document1, document2 });

        DocumentItem documentItem1 = new()
        {
            Document = document1,
            Article = article1,
            Quantity = 100,
            Description = "Description 1",
        };
        DocumentItem documentItem2 = new()
        {
            Document = document2,
            Article = article2,
            Quantity = 200,
            Description = "Description 2",
        };
        context.Set<DocumentItem>().AddRange(new[] { documentItem1, documentItem2 });

        documentItem1 = new()
        {
            Document = document2,
            Article = article1,
            Quantity = 100,
            Description = "Description 1",
        };
        documentItem2 = new()
        {
            Document = document1,
            Article = article2,
            Quantity = 200,
            Description = "Description 2",
        };
        context.Set<DocumentItem>().AddRange(new[] { documentItem1, documentItem2 });

        context.SaveChanges();

        Group group1 = new()
        {
            Id = 1,
            Code = "A",
            Path = "A"        
        };

        Group group2 = new()
        {
            Id = 2,
            Code = "B",
            Path = "A.B"
        };

        Group group3 = new()
        {
            Id = 3,
            Code = "C",
            Path = "A.B.C"
        };

        context.Set<Group>().AddRange(new[] { group1, group2, group3 });

        context.SaveChanges();
    }
});

app.MapGet("/authorization/admin", () =>
{
    var key = Encoding.UTF8.GetBytes("4f1feeca525de4cab064656007da3edac7895a87ff0ea865693300fb8b6e8f9d");
    var securityKey = new SymmetricSecurityKey(key);
    var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);
    var claims = new[] {
                new Claim(ClaimTypes.NameIdentifier, 1.ToString()),
                new Claim(ClaimTypes.Name, "Admin"),
                new Claim(ClaimTypes.Role, "Admin"),
            };

    var token = new JwtSecurityToken("https://localhost:7045", "https://localhost:7045", claims,
        expires: DateTime.Now.AddYears(1),
        signingCredentials: signingCredentials);

    var tokenHandler = new JwtSecurityTokenHandler();
    return tokenHandler.WriteToken(token);
});

app.MapGet("/authorization/user", () =>
{
    var key = Encoding.UTF8.GetBytes("4f1feeca525de4cab064656007da3edac7895a87ff0ea865693300fb8b6e8f9d");
    var securityKey = new SymmetricSecurityKey(key);
    var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);
    var claims = new[] {
                new Claim(ClaimTypes.NameIdentifier, 1.ToString()),
                new Claim(ClaimTypes.Name, "User"),
                new Claim(ClaimTypes.Role, "User"),
            };

    var token = new JwtSecurityToken("https://localhost:7045", "https://localhost:7045", claims,
        expires: DateTime.Now.AddYears(1),
        signingCredentials: signingCredentials);

    var tokenHandler = new JwtSecurityTokenHandler();
    return tokenHandler.WriteToken(token);
});


app.Run();
