using Backend.Mappers;
using Backend.Models;
using Backend.Services;
using Polly;
using Polly.Extensions.Http;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddCors(options =>
{
	options.AddDefaultPolicy(builder =>
	{
		builder.WithOrigins("http://localhost:5173", "http://localhost:5174", "http://localhost:3000")
			.AllowAnyHeader()
			.AllowAnyMethod();
	});
});

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
}).AddPolicyHandler(GetRetryPolicy());
builder.Services.AddHttpClient<ImageCacheService>(client =>
{
	client.Timeout = TimeSpan.FromSeconds(30);
}).AddPolicyHandler(GetRetryPolicy());

// Configure AutoMapper
builder.Services.AddAutoMapper(cfg =>
{
	cfg.AddProfile<MovieMapperProfile>();
});

// Register services
builder.Services.AddSingleton<FeedService>();
builder.Services.AddSingleton<HashService>();
builder.Services.AddSingleton<ImageCacheService>();
builder.Services.AddTransient<ImageHashResolver>();
builder.Services.AddHostedService<FeedInitializer>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseCors();
app.MapControllers();

app.Run();

// Simple single retry policy for transient HTTP errors
static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
{
	return HttpPolicyExtensions
		.HandleTransientHttpError()
		.RetryAsync(1);
}