using ProxyService.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Configuration.AddEnvironmentVariables();

builder.Configuration.AddEnvironmentVariables(prefix: "ProxyOptions__");
var gradualMigration = Environment.GetEnvironmentVariable("GradualMigration");
Console.WriteLine($"gradualMigration =  {gradualMigration}");
var ProxyOptions__GradualMigration = Environment.GetEnvironmentVariable("ProxyOptions__GradualMigration");
Console.WriteLine($"ProxyOptions__GradualMigration =  {ProxyOptions__GradualMigration}");
builder.Services.Configure<ProxyOptions>(builder.Configuration);

var port = Environment.GetEnvironmentVariable("PORT") ?? "8000";
builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

builder.Services.AddHttpClient();
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.MapControllers();

app.Run();

