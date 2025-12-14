using MovieRental.Data;
using MovieRental.Movie;
using MovieRental.Rental;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddEntityFrameworkSqlite().AddDbContext<MovieRentalDbContext>();

// Register application services
builder.Services.AddScoped<IRentalFeatures, RentalFeatures>();
builder.Services.AddScoped<IMovieFeatures, MovieFeatures>();
builder.Services.AddSingleton<MovieRental.PaymentProviders.PaymentProviderFactory>();

var app = builder.Build();

// Global exception handler must be first in pipeline to catch all errors
app.UseMiddleware<MovieRental.Middleware.GlobalExceptionHandlerMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Initialize database (creates if doesn't exist)
using (var client = new MovieRentalDbContext())
{
	client.Database.EnsureCreated();
}

app.Run();
