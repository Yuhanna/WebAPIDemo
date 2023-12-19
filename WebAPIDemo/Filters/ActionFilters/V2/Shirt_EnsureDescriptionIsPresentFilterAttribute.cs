using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebAPIDemo.Models;

namespace WebAPIDemo.Filters.ActionFilters.V2
{//For example of versioning
    public class Shirt_EnsureDescriptionIsPresentFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            var shirt = context.ActionArguments["shirt"] as Shirt;
            if (shirt != null && !shirt.ValidateDescription()) 
            {
                context.ModelState.AddModelError("shirt", "Description is required");
                var problemDetails = new ValidationProblemDetails(context.ModelState)
                {
                    Status = StatusCodes.Status400BadRequest
                };
                context.Result = new BadRequestObjectResult(problemDetails);
            }
        }
    } 
}
