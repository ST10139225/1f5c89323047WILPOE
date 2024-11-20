    using KNN.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Authorization.Infrastructure;
    using Microsoft.AspNetCore.Identity; 
    using System.Threading.Tasks;
 namespace KNN.Authorization
{

        public class BlogAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, Blog>
        {
            private readonly UserManager<ApplicationUser> userManager;

            public BlogAuthorizationHandler(UserManager<ApplicationUser> userManager)
            {
                this.userManager = userManager;
            }

            protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, Blog resource)
            {
                var applicationUser = await userManager.GetUserAsync(context.User);

                if ((requirement.Name == Operations.Update.Name || requirement.Name == Operations.Delete.Name) && applicationUser == resource.Creator)
                {
                    context.Succeed(requirement);
                }
            }
        }
    }
 
