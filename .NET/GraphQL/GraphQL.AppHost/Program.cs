using GraphQL.AppHost;

var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres")
                      .WithPgAdmin();

builder.AddProject<Projects.GraphQL>("graphql")
    .WithReference(postgres)
    .WithMigrateCommand()
    .WithGraphQLCommand();

builder.Build().Run();
