using GraphQL;
using GraphQL.Persistance;
using GraphQL.Persistance.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

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

builder.Services.AddGraphQLServer()
    .AddQueryType<Query>()
    .AddProjections()
    .AddSorting()
    .AddFiltering();

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapGraphQL();

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
    }
});

app.Run();
