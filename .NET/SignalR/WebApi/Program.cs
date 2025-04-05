var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddCors(o =>
{
    o.AddPolicy("AllowAnyOrigin", p => p
        .AllowAnyHeader()
        .AllowAnyOrigin()
        .AllowAnyHeader());
});
builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("AllowAnyOrigin");
app.MapHub<MyHub>("/hub");

app.UseHttpsRedirection();

app.Run();
