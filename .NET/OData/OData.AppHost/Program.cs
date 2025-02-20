var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres")
                      .WithPgAdmin();

builder.AddProject<Projects.OData>("odata")
    .WithReference(postgres);

builder.Build().Run();
