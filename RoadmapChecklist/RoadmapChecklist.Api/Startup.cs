using Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Data.Infrastructure.Repository;
using Data.Infratructure.UnitOfWork;
using RoadmapChecklist.Service.User;

namespace RoadmapChecklist.Api
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

            services.AddDbContext<RoadmapChecklistDbContext>(opt => 
                opt.UseSqlServer(Configuration.GetConnectionString("Default")));
            
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));

            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

            services.AddTransient(typeof(IUserService), typeof(UserService));
            
            services.AddSwaggerGen();  
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
            app.UseSwagger();  
            app.UseSwaggerUI(c =>  
            {  
               c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Test1 Api v1");  
            });  
        }
    }
}
