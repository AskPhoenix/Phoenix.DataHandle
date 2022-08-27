using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

namespace Phoenix.DataHandle.Api
{
    public static class DocumentationHelper
    {
        public static OpenApiSecurityScheme JWTSecurityScheme => new()
        {
            Scheme = "bearer",
            BearerFormat = "JWT",
            Description = "Enter the JWT Bearer token.",
            In = ParameterLocation.Header,
            Name = "JWT Authentication",
            Type = SecuritySchemeType.Http,

            Reference = new()
            {
                Id = JwtBearerDefaults.AuthenticationScheme,
                Type = ReferenceType.SecurityScheme
            }
        };
    }
}
