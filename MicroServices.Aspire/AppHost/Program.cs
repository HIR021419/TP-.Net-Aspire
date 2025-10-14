var builder = DistributedApplication.CreateBuilder(args);

// paramètres
var todosDbName = "Todos";
var usersDbName = "Users";
var username = builder.AddParameter("username");
var password = builder.AddParameter("password");

// --- Base de données Todo ---
var todosDb = builder.AddPostgres("postgres-todos", username, password)
    .WithPgAdmin()
    .WithEnvironment("POSTGRES_DB", todosDbName)
    .WithBindMount("../TodoService/Data", "/docker-entrypoint-initdb.d")
    .AddDatabase(todosDbName);

// --- Base de données User ---
var usersDb = builder.AddPostgres("postgres-users", username, password)
    .WithPgAdmin()
    .WithEnvironment("POSTGRES_DB", usersDbName)
    .WithBindMount("../UserService/Data", "/docker-entrypoint-initdb.d")
    .AddDatabase(usersDbName);


var daprPubSub = builder.AddDaprPubSub("pubsub");

builder.AddProject<Projects.UserService>("userservice")
    .WithReference(usersDb)
    .WithDaprSidecar()
    .WithReference(daprPubSub);

builder.AddProject<Projects.TodoService>("todoservice")
    .WithReference(todosDb)
    .WithDaprSidecar()
    .WithReference(daprPubSub);

builder.Build().Run();