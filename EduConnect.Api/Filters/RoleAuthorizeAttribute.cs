using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EduConnect.Api.Filters
{
    public class RoleAuthorizeAttribute : Attribute, IAsyncActionFilter
    {
        private readonly string[] _roles;

        public RoleAuthorizeAttribute(params string[] roles)
        {
            _roles = roles;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var user = context.HttpContext.User;

            if (!user.Identity.IsAuthenticated || !_roles.Any(role => user.IsInRole(role)))
            {
                context.Result = new ForbidResult(); // Kullanıcı yetkisiz
                return;
            }

            await next(); // İzin veriliyorsa sonraki işleme geç
        }
    }
}
