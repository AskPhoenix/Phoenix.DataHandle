using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Phoenix.DataHandle.Api.Swagger
{
    public class AuthorizedActionFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation is null)
                throw new ArgumentNullException(nameof(operation));
            if (context is null)
                throw new ArgumentNullException(nameof(context));

            object[] methodAttributes = context.MethodInfo.GetCustomAttributes(true);

            bool needsAuth = methodAttributes.OfType<AuthorizeAttribute>().Any() ||
                (context.MethodInfo.DeclaringType != null
                    && context.MethodInfo.DeclaringType.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any()
                    && !methodAttributes.OfType<AllowAnonymousAttribute>().Any());

            if (needsAuth)
            {
                operation.Security = new List<OpenApiSecurityRequirement>()
                {
                    new () { {DocumentationHelper.JWTSecurityScheme, Array.Empty<string>() } }
                };
            }
        }
    }
}
