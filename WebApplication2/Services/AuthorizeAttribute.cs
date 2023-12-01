using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Controllers;
using WebApplication2.Models;

namespace WebApplication2.Services
{
    
        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
        public class AuthorizeAttribute : Attribute, IAuthorizationFilter
        {
            public void OnAuthorization(AuthorizationFilterContext context)
            {
                var account = (LoginModel)context.HttpContext.Items["User"];
                if (account == null)
                {
                    // not logged in
                    context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
                }
            }
        }
    }

