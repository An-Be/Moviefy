using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace Moviefy.Policy
{
    public class SpecificUserRequirement : AuthorizationHandler<SpecificUserRequirement>, IAuthorizationRequirement
    {

        public string UserName { get; set; }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, SpecificUserRequirement requirement)
        {

            if (context.User?.Identity?.Name?.Equals(UserName) ?? false)
            {
                context.Succeed(requirement);

            }
            else
            {
                context.Fail();
            }
            return Task.CompletedTask;
        }

        public SpecificUserRequirement(string username)
        {
            UserName = username;
        }


    }
}
