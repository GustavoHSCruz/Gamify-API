using System.Reflection;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Service.API.SwaggerConfigs
{
    public static class SwaggerConfigs
    {
        internal static void SwaggerBuilder(IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "API",
                    Version = "v1.0",
                    Description = "API para Sistema",
                    Contact = new OpenApiContact
                    {
                        Name = "Equipe de Desenvolvimento",
                        Email = "contact@geg.dev.com.br"
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Uso Interno"
                    }
                });

                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    In = ParameterLocation.Header,
                    Description = "Insira o token JWT no formato: Bearer {seu_token}",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT"
                });

                // opt.AddSecurityRequirement(new()
                // {
                //     {
                //         new OpenApiSecurityScheme
                //         {
                //             Reference = new OpenApiReference
                //             {
                //                 Type = ReferenceType.SecurityScheme,
                //                 Id = "Bearer"
                //             }
                //         },
                //         Array.Empty<string>()
                //     }
                // });


                opt.EnableAnnotations();

                // Incluir comentários XML para documentação
                // var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                // var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFilename);
                // opt.IncludeXmlComments(xmlPath);
                
                // 1. Carrega o XML da própria API (Controllers)
                var xmlFileApi = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPathApi = Path.Combine(AppContext.BaseDirectory, xmlFileApi);
                opt.IncludeXmlComments(xmlPathApi);

                // 2. Carrega o XML do Domain.Core (Onde está o RegisterUserRequest)
                // O truque é usar o typeof numa classe que existe lá dentro para pegar o Assembly correto
                var assemblyDomain = System.Reflection.Assembly.GetAssembly(typeof(Domain.Core.Requests.Public.RegisterUserRequest));
    
                if (assemblyDomain != null)
                {
                    var xmlFileDomain = $"{assemblyDomain.GetName().Name}.xml";
                    var xmlPathDomain = Path.Combine(AppContext.BaseDirectory, xmlFileDomain);
        
                    // Só tenta incluir se o arquivo realmente existir
                    if (File.Exists(xmlPathDomain))
                    {
                        opt.IncludeXmlComments(xmlPathDomain);
                    }
                }
            });
        }

        internal static void SwaggerApp(WebApplication app)
        {
            // ✅ CORREÇÃO: UseSwagger deve estar disponível em todos os ambientes
            // para que o swagger.json seja acessível pela interface customizada
            app.UseSwagger(c => { c.RouteTemplate = "swagger/{documentName}/swagger.json"; });

            app.UseSwaggerUI(opt =>
            {
                // Configuração da URL do JSON
                opt.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1.0");

                // ✅ Configurações para manter interface organizada e limpa
                opt.DocExpansion(DocExpansion.None); // Manter todas as rotas fechadas
                opt.DefaultModelExpandDepth(-1); // Manter models fechados  
                opt.DefaultModelsExpandDepth(-1); // Manter lista de models fechada
                opt.DefaultModelRendering(ModelRendering.Example); // Mostrar examples por padrão

                // Configurações de funcionalidade
                opt.DisplayRequestDuration();
                opt.EnableFilter();
                opt.EnableTryItOutByDefault();
                opt.EnablePersistAuthorization();
                opt.DisplayOperationId();
                opt.ShowExtensions();
                opt.EnableDeepLinking();
                opt.ShowCommonExtensions();
            });
        }
    }
}