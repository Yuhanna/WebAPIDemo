using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WebAPIDemo.Filters.OperationFilter
{
    public class AuthorizationHeaderOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if(operation.Security == null)
                operation.Security = new List<OpenApiSecurityRequirement>();

            var schema = new OpenApiSecurityScheme { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" } };
                operation.Security.Add(new OpenApiSecurityRequirement 
                {
                    [schema] = new List<string> { }
                });
        }
    }
}
