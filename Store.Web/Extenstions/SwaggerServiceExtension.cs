using Microsoft.AspNetCore.Components.Web;
using Microsoft.OpenApi.Models;
using System.Net;

namespace Store.Web.Extenstions
{
    public static class SwaggerServiceExtension
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(
                    "V1",
                    new OpenApiInfo
                    {
                        Title = "Store Api",
                        Version = "V1",
                        Contact = new OpenApiContact
                        {
                            Name = "Route Academy",
                            Email = "Route@gmail.com",
                            Url = new Uri("https://twitter.com/routeacademy"),

                        }


                    });
                var securityScheme = new OpenApiSecurityScheme
                {
                    Description="JWT Authorization header using the Bearer schemae.Example:\"Authorization:Berar {token}\"",
                    Name= "Authorization",
                    In=ParameterLocation.Header,
                    Type=SecuritySchemeType.ApiKey,
                    Scheme="bearer",
                    Reference= new OpenApiReference
                    {
                        Id="bearer",
                        Type=ReferenceType.SecurityScheme
                    }

                };
                options.AddSecurityDefinition("bearer",securityScheme);
                var securityRequirments = new OpenApiSecurityRequirement
                {
                    {securityScheme,new[] { "bearer" } }
                };
                options.AddSecurityRequirement(securityRequirments);

            });
            return services;
        }
    }
}
