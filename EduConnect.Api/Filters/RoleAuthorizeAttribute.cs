using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

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
                // Resolve the logger service from the context
                var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<RoleAuthorizeAttribute>>();

                var user = context.HttpContext.User;

                if (!user.Identity.IsAuthenticated)
                {
                    logger.LogWarning("User is not authenticated.");
                    context.Result = new ForbidResult();
                    return;
                }

                var userRoles = user.Claims
                                    .Where(c => c.Type == ClaimTypes.Role)
                                    .Select(c => c.Value)
                                    .ToList();

                logger.LogInformation($"User roles: {string.Join(", ", userRoles)}");

                if (!_roles.Any(role => userRoles.Contains(role)))
                {
                    logger.LogWarning($"User does not have the required role. Required roles: {string.Join(", ", _roles)}");
                    context.Result = new ForbidResult();
                    return;
                }

                await next();
            }

        }
    }

