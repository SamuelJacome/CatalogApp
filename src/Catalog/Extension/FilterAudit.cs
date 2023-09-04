using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Catalog.Extension
{
    public class FilterAudit : IActionFilter
    {
        //After
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                var message = context.HttpContext.User.Identity.Name + " Acessou: " +
                                context.HttpContext.Request.GetDisplayUrl();
                Console.WriteLine(message);
            }
        }
        //Before
        public void OnActionExecuting(ActionExecutingContext context)
        {
            // Oque fazer?
        }
    }
}