var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

//Routing

// "/Shirts"

app.MapGet("/shirts", () =>
{
    return "Reading all the shirts";
});

app.MapGet("/shirts/{id}", (int id) =>
{
    return $"Reading shirts with ID= {id}";
});

app.MapPost("/shirts", () =>
{
    return "Creating a shirt";
});

app.MapPut("/shirts/{id}",(int id) =>
{
    return $"Updating shirt with ID: {id}";
});

app.MapDelete("/shirts/{id}", (int id) =>
{
    return $"Delete shirt with ID: {id}";
});

app.Run();


