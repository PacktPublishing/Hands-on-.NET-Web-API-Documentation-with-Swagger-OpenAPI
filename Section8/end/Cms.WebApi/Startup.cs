using System.IO;
using Cms.Data.Repository.Repositories;
using Cms.WebApi.Mappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

#pragma warning disable CS1591

[assembly: ApiController]
[assembly: ApiConventionType(typeof(DefaultApiConventions))]
namespace Cms.WebApi
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ICmsRepository, InMemoryCmsRepository>();
            services.AddAutoMapper(typeof(CmsMapper));

            services.AddControllers(c =>
            {
                //c.Filters.Add(new ProducesResponseTypeAttribute(500));
            });

            services.AddVersionedApiExplorer(option =>
            {
                option.GroupNameFormat = "'v'V";
                option.SubstituteApiVersionInUrl = true;
            });

            services.AddApiVersioning(setupAction =>
            {
                setupAction.AssumeDefaultVersionWhenUnspecified = true;
                setupAction.DefaultApiVersion = new ApiVersion(1, 0);
            });

            var apiVersionDesc = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();
            foreach(var apiVersion in apiVersionDesc.ApiVersionDescriptions)
            {
                services.AddSwaggerGen(s =>
                {
                    s.SwaggerDoc(apiVersion.GroupName, new OpenApiInfo
                    {
                        Title = "CMS OpenAPI",
                        Version = apiVersion.GroupName,
                        Description = "OpenAPI Specification for CMS",
                        License = new OpenApiLicense()
                        {
                            Name = "MIT",
                        },
                        Contact = new OpenApiContact()
                        {
                            Name = "Praveenkumar Bouna",
                            Email = "hello@codewithpraveen.com"
                        },
                    });
                });
            };

            // services.AddSwaggerGen(s =>
            // {
            //     s.SwaggerDoc("v1", new OpenApiInfo
            //     {
            //         Title = "CMS OpenAPI",
            //         Version = "v1",
            //         Description = "OpenAPI Specification for CMS",
            //         License = new OpenApiLicense()
            //         {
            //             Name = "MIT",
            //         },
            //         Contact = new OpenApiContact()
            //         {
            //             Name = "Praveenkumar Bouna",
            //             Email = "hello@codewithpraveen.com"
            //         },
            //     });

            //     s.SwaggerDoc("v2", new OpenApiInfo
            //     {
            //         Title = "CMS OpenAPI",
            //         Version = "v2",
            //         Description = "OpenAPI Specification for CMS",
            //         License = new OpenApiLicense()
            //         {
            //             Name = "MIT",
            //         },
            //         Contact = new OpenApiContact()
            //         {
            //             Name = "Praveenkumar Bouna",
            //             Email = "hello@codewithpraveen.com"
            //         },
            //     });

            //     var xmlPath = Path.Combine(System.AppContext.BaseDirectory, "Cms.WebAPI.xml");
            //     s.IncludeXmlComments(xmlPath);
            // });

            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, 
            IWebHostEnvironment env,
            IApiVersionDescriptionProvider apiVersionDescriptionProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(s =>
            {
                foreach (var item in apiVersionDescriptionProvider.ApiVersionDescriptions)
                {
                    s.SwaggerEndpoint($"/swagger/{item.GroupName}/swagger.json", item.GroupName);
                }
                // s.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                // s.SwaggerEndpoint("/swagger/v2/swagger.json", "v2");
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

#pragma warning restore CS1591
