using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using PersonDirectory.ActionFilters;
using PersonDirectory.CustomExceptionMiddleware;
using PersonDirectory.Domain;
using PersonDirectory.Domain.Interfaces;
using PersonDirectory.Domain.Models;
using PersonDirectory.Persistence;
using PersonDirectory.Persistence.Data;
using PersonDirectory.Persistence.Repository;

namespace PersonDirectory
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
