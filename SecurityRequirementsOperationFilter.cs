//Setup av swaggerGen
public class SecurityRequirementsOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        // Policy names map to scopes
        var requiredScopes = context.MethodInfo
            .GetCustomAttributes(true)
            .OfType<AuthorizeAttribute>()
            .Select(attribute => attribute.Policy!)
            .Distinct()
            .ToList();

        if (requiredScopes.Count > 0)
        {
            operation.Responses ??= [];

            var scheme = new OpenApiSecuritySchemeReference("bearer", context.Document);

            operation.Security =
            [
                new OpenApiSecurityRequirement
                {
                    [scheme] = requiredScopes
                }
            ];
        }
    }
}