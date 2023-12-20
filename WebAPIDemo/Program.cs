using Microsoft.AspNetCore.Mvc.Versioning;
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

builder.Services.AddApiVersioning(options => //For versioning and setting default version
{
    options.ReportApiVersions = true;//in header it show which endpoint support which version
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1,0);
    //options.ApiVersionReader = new HeaderApiVersionReader("X-API-Version");
});

//builder.Services.AddEndpointsApiExplorer();//For Swagger Document
builder.Services.AddVersionedApiExplorer(Options => Options.GroupNameFormat = "'v'VVV");//Sopport versionnig in Swagger documentation
builder.Services.AddSwaggerGen(c =>
 {
     c.SwaggerDoc("v1", new OpenApiInfo { Title = "My Web API v1", Version = "version 1"});
     c.SwaggerDoc("v2", new OpenApiInfo { Title = "My Web API v2", Version = "version 2" });
     //To add authorization Header to swagger
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
    app.UseSwaggerUI(
        options => //START For solving versioning of Swagger Documen's problem
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPI v1");
            options.SwaggerEndpoint("/swagger/v2/swagger.json", "WebAPI v2");
        }//END For solving versioning of Swagger Documen's problem
        );
}



app.UseHttpsRedirection();

app.MapControllers();   


app.Run();


