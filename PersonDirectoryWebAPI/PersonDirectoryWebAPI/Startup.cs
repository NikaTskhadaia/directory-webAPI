using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using PersonDirectory.Domain.Interfaces;
using PersonDirectory.Domain.Models;
using PersonDirectory.Persistence;
using PersonDirectory.Persistence.Data;
using PersonDirectory.Persistence.Repository;
using PersonDirectory.WebAPI.ActionFilters;
using PersonDirectory.WebAPI.CustomExceptionMiddleware;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.EntityFrameworkCore;

namespace PersonDirectory.WebAPI
{
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
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.Configure<RequestLocalizationOptions>(
                options =>
                {
                    var supportedCultures = new List<CultureInfo>
                    {
                        new CultureInfo("en"),
                        new CultureInfo("ge")
                    };
                    options.DefaultRequestCulture = new RequestCulture(culture: "en", uiCulture: "en");
                    options.SupportedCultures = supportedCultures;
                    options.SupportedUICultures = supportedCultures;
                });
            object p = services.AddAutoMapper(typeof(Startup), typeof(PersonRepository));
            services.AddControllers();
            services.AddDbContext<PeopleDb>(options => options.UseSqlServer(
                    Configuration.GetConnectionString("Desktop")));
            services.AddScoped<ValidationFilterAttribute>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PersonDirectory", Version = "v1" });
            });
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IPersonRepository, PersonRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<PersonModel> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PersonDirectory v1"));
            }

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseRequestLocalization(
                app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>().Value
                );

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
