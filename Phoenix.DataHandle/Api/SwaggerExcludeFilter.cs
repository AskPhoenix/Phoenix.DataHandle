using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace Phoenix.DataHandle.Api
{
    public class SwaggerExcludeFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (schema.Properties is null)
                return;

            var toExclude = context.Type.GetProperties()
                .Where(t => t.GetCustomAttribute<JsonIgnoreAttribute>() != null);

            foreach (var property in toExclude)
            {
                var propertyToHide = schema.Properties.Keys
                    .SingleOrDefault(x => string.Equals(x, property.Name, StringComparison.OrdinalIgnoreCase));

                if (propertyToHide != null)
                    schema.Properties.Remove(propertyToHide);
            }
            
            //foreach (var property in toExclude)
            //    if (schema.Properties.ContainsKey(property.Name))
            //        schema.Properties.Remove(property.Name);
        }
    }
}
