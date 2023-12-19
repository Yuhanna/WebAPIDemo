using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using WebAPIDemo.Data;
using WebAPIDemo.Filters.OperationFilter;

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
 {//To add authorization Header to swagger
        c.OperationFilter<AuthorizationHeaderOperationFilter>();
     c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme        //AuthorizationHeaderOperationFilter linked to this class
         {
        Scheme = "Bearer",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        In = ParameterLocation.Header
 }); 

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


