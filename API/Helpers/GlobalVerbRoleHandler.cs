using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace API.Helpers
{
    public class GlobalVerbRoleHandler : AuthorizationHandler<GlobalVerbRoleRequirement>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GlobalVerbRoleHandler(IHttpContextAccessor httpContextAccessor)
        {
            this._httpContextAccessor = httpContextAccessor;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, GlobalVerbRoleRequirement requirement)
        {
            // check whether the user has required roles for current verb
            var roles = context.User.FindAll(c => string.Equals(c.Type, ClaimTypes.Role)).Select(c => c.Value);
            var verb = _httpContextAccessor.HttpContext?.Request.Method;
            if (string.IsNullOrEmpty(verb)) { throw new Exception($"request cann't be null!"); }
            foreach (var role in roles)
            {
                if (requirement.IsAllowed(role, verb))
                {
                    context.Succeed(requirement);
                    return Task.CompletedTask;
                }
            }
            context.Fail();
            return Task.CompletedTask;
        }
    }
}