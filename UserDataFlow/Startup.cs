using FirebaseAdmin;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;
using Google.Apis.Auth.OAuth2;
using UserDataFlow.Interface;
using UserDataFlow.Models;
using UserDataFlow.Repository;

namespace UserDataFlow
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

            services.AddTransient<IUser, UserRepository>();

            services.AddTransient<IRoles, RolesRepository>();

            services.AddTransient<IAddress, AddressRepository>();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddTransient<IContactNumber, ContactNumberRepository>();

            services.AddDbContext<logesh_user_task_dbContext>(c =>
                c.UseSqlServer(Configuration.GetConnectionString("ConnectionString")));

            services.AddControllers().AddNewtonsoftJson();

            services.AddSingleton(Configuration);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Version = "v1",
                    Title = "User Flow APIs",
                    Description =
                        "My First Backend db design with api integration in asp.net core with entity framework core",
                    Contact = new OpenApiContact()
                    {
                        Name = "Logesh Palani",
                        Email = "logesh.01@hotmail.com",
                        Url = new Uri("https://logeshpalani.blogspot.com/")
                    }
                });
            });

            // Building default instance
            var defaultInstance = FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "fcm-key.json"))
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

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My user flow v1");
            });

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}