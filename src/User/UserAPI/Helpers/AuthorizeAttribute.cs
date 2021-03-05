using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserAPI.Entities;
using System.Web;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {

        public void OnAuthorization(AuthorizationFilterContext context)
        {
        var user = (User)context.HttpContext.Items["User"];
        bool sameOrigin = (bool)context.HttpContext.Items["Same-Origin"];
            if (user == null)
            {
                if (!sameOrigin)
                {

                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
                
                }
            // not logged in
        }
        }
    }
