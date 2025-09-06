using Backend.Mappers;
using Backend.Models;
using Backend.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure FeedSettings
builder.Services.Configure<FeedSettings>(
	builder.Configuration.GetSection("FeedSettings"));

// Configure HttpClient
builder.Services.AddHttpClient<FeedService>(client =>
{
	client.Timeout = TimeSpan.FromSeconds(30);
});

// Configure AutoMapper
builder.Services.AddAutoMapper(cfg =>
{
	cfg.AddProfile<MovieMapperProfile>();
});

// Register services
builder.Services.AddSingleton<FeedService>();
builder.Services.AddHostedService<FeedInitializer>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();

app.Run();