using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Application;
using Microsoft.AspNetCore.Mvc;
using Domain;
using Application.DataTransfer;

namespace WebApp.Helpers
{
    public class LoggedIn : Attribute, IResourceFilter
    {
        private readonly string _role;
        public LoggedIn(string role)
        {
            _role = role;
        }

        public LoggedIn()
        {

        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            var user = context.HttpContext.RequestServices.GetService<UserDto>();

            if(!user.IsLogged)
            {
                context.Result = new UnauthorizedResult();
            } else
            {
                if (_role != null)
                {
                    if (user.RoleName != _role)
                    {
                        context.Result = new UnauthorizedResult();
                    }
                }
            }
        }
    }
}
