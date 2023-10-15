using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

namespace XICommServer.Web
{
    public class ShowInSwaggerAttributeFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var filteredApis = context.ApiDescriptions.Where(a => a.CustomAttributes().Any(x =>
                x.GetType() == typeof(ShowInSwaggerAttribute)));

            foreach (var path in swaggerDoc.Paths.ToList())
            {
                if (filteredApis.All(x => ("/" + x.RelativePath) != path.Key))
                    swaggerDoc.Paths.Remove(path.Key);
            }
        }
    }
}
