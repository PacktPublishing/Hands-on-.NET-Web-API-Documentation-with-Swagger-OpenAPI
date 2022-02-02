using System.IO;
using Cms.Data.Repository.Repositories;
using Cms.WebApi.Mappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
//using Microsoft.OpenApi.Models;

#pragma warning disable CS1591

[assembly: ApiController]
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

            //     var xmlPath = Path.Combine(System.AppContext.BaseDirectory, "Cms.WebAPI.xml");
            //     s.IncludeXmlComments(xmlPath);
            // });

            services.AddOpenApiDocument(c =>
            {
                c.DocumentName = "v1";
                c.PostProcess = doc =>
                {
                    doc.Info.Version = "v1";
                    doc.Info.Title = "CMS OpenAPI";
                    doc.Info.Description = "OpenAPI Specification for CMS";
                    doc.Info.License = new NSwag.OpenApiLicense()
                    {
                        Name = "MIT",
                    };
                    doc.Info.Contact = new NSwag.OpenApiContact()
                    {
                        Name = "Praveenkumar Bouna",
                        Email = "hello@codewithpraveen.com"
                    };
                };
            });

            services.AddControllers(c =>
            {
                c.Filters.Add(new ProducesResponseTypeAttribute(500));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            // app.UseSwagger();
            // app.UseSwaggerUI();

            app.UseOpenApi();
            app.UseSwaggerUi3();

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
