using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WebAPIDemo.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(Options =>
{
    Options.UseSqlServer(builder.Configuration.GetConnectionString("ShirtStoreManagement"));
}
);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();//For Swagger Document
builder.Services.AddSwaggerGen(c =>
    {
      //  c.OperationFilter<AuthorizationHeaderOperationFilter>
});////For Swagger Document

var app = builder.Build();

// Configure the HTTP request pipeline.


if(app.Environment.IsDevelopment())//For Swagger Document
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();

app.MapControllers();   


app.Run();


