using Logistics.CQRS.EventSourcing;
using static Slapper.AutoMapper;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;
var settings = new AppConfiguration();
config.Bind(settings);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton(settings); 
builder.Services.AddScoped<EventStore>(); 
builder.Services.AddScoped<ParcelCommandHandler>();
builder.Services.AddScoped<ParcelEventHandler>();
builder.Services.AddScoped<ParcelQueryHandler>();
builder.Services.AddScoped<ParcelSateQueryHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
