using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Setup.Ioc;
using System;
using System.Linq;
using System.Security;
using System.Security.Authentication;
using System.Security.Claims;

namespace Setup.ActionFilter
{
    public class AuthorizeControl : ActionFilterAttribute
    {
        private readonly string[] _roles;
        private IHttpContextAccessor _httpContextAccessor;
        public AuthorizeControl(string roles = null)
        {
            if (roles != null)
            {
                _roles = roles.Split(",");
            }
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }


        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
             var userId = _httpContextAccessor?.HttpContext?.User?
              .FindFirst(x => x.Type == ClaimTypes.NameIdentifier)?
              .Value;

            if (!string.IsNullOrEmpty(userId))
            {
                base.OnActionExecuting(filterContext);
            }
            else
            {
                throw new AuthenticationException("Authorization Denied");
            }

        }

    }
}
