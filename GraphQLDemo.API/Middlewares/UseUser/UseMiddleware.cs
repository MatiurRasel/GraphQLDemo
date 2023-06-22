using FirebaseAdminAuthentication.DependencyInjection.Models;
using GraphQLDemo.API.Models;
using HotChocolate.Resolvers;
using System.Security.Claims;

namespace GraphQLDemo.API.Middlewares.UseUser
{
    public class UseMiddleware
    {
        private readonly FieldDelegate _next;

        public UseMiddleware(FieldDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(IMiddlewareContext context)
        {
            if (context.ContextData.TryGetValue("ClaimsPrincipal", out object rawClaimsPrincipal)
                && rawClaimsPrincipal is ClaimsPrincipal claimsPrincipal)
            {
                bool emailVerified = bool.TryParse(claimsPrincipal.FindFirstValue(FirebaseUserClaimType.EMAIL_VERIFIED),
                    out bool result) && result;

                User user = new User()
                {
                    Id = claimsPrincipal.FindFirstValue(FirebaseUserClaimType.ID),
                    Email = claimsPrincipal.FindFirstValue(FirebaseUserClaimType.EMAIL),
                    UserName = claimsPrincipal.FindFirstValue(FirebaseUserClaimType.USERNAME),
                    EmailVerified = emailVerified,
                };

                context.ContextData.Add("User", user);
            }
            
           await _next(context);
        } 
    }
}
