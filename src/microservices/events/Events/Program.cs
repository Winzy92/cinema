using System.Text.Json;
using Events.Handlers;
using Events.Interfaces;
using Events.Models;
using Events.Services;
using Events.Workers;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(8082);
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Configuration.AddEnvironmentVariables();

builder.Services.AddHttpClient();
builder.Services.AddControllers();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
});

builder.Services.AddHostedService<MovieConsumerWorker>();
builder.Services.AddHostedService<PaymentConsumerWorker>();
builder.Services.AddHostedService<UserConsumerWorker>();

builder.Services.AddSingleton<KafkaProducerService>();

builder.Services.AddSingleton<IEventHandler<UserRequestMessage>, UserRequestHandler>();
builder.Services.AddSingleton<IEventHandler<PaymentEventMessage>, PaymentRequestHandler>();
builder.Services.AddSingleton<IEventHandler<MovieRequestMessage>, MovieRequestHandler>();

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