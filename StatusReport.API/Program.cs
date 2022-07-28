using StatusReport.API.Models;
using StatusReport.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ICosmosDbService<User>>(InitializeCosmosClientInstanceAsync<User>(builder.Configuration.GetSection("CosmosDb"), "ContainerUser").GetAwaiter().GetResult());
builder.Services.AddSingleton<ICosmosDbService<Activity>>(InitializeCosmosClientInstanceAsync<Activity>(builder.Configuration.GetSection("CosmosDb"), "ContainerActivity").GetAwaiter().GetResult());

static async Task<CosmosDbService<T>> InitializeCosmosClientInstanceAsync<T>(IConfigurationSection configurationSection, string container)
{
    var databaseName = configurationSection["DatabaseName"];
    var containerName = configurationSection[container];
    var account = configurationSection["Account"];
    var key = configurationSection["Key"];

    var client = new Microsoft.Azure.Cosmos.CosmosClient(account, key);
    var database = await client.CreateDatabaseIfNotExistsAsync(databaseName);
    await database.Database.CreateContainerIfNotExistsAsync(containerName, "/id");

    var cosmosDbService = new CosmosDbService<T>(client, databaseName, containerName);
    return cosmosDbService;
}

//services.AddSingleton<ICosmosDbService>(InitializeCosmosClientInstanceAsync(Configuration.GetSection("CosmosDb")).GetAwaiter().GetResult());
var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}
app.UseCors(builder =>
{
    builder.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
});
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();