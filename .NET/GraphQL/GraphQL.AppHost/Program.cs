var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres")
                      .WithPgAdmin();

builder.AddProject<Projects.GraphQL>("graphql")
    .WithReference(postgres);

builder.Build().Run();
