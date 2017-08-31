using System;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Bootstraper.Modules;
using StackExchange.Core.Settings;
using StackExchange.Infrastructure;
using StackExchange.Infrastructure.SeedInitializers;

namespace StackExchange.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IContainer ApplicationContainer { get; private set; }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            
            services.AddDbContext<Context>(options => options.UseSqlServer("ConnectionString"));
            services.AddTransient<CompanyInitializer>();
            services.AddMvc();  
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = "http://localhost:5000",
                    ValidateIssuerSigningKey = true,
                    //IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt),
                };

            });

            var builder = new ContainerBuilder();
            builder.Populate(services);
            builder.RegisterModule(new RepositoryModule());
            builder.RegisterModule(new CommandsModule());

            ApplicationContainer = builder.Build();
            return new AutofacServiceProvider(ApplicationContainer);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, CompanyInitializer companySeeder)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var jwtSettings = app.ApplicationServices.GetService<JwtSettings>();
            

            app.UseAuthentication();
            app.UseMvc();
            companySeeder.Seed().Wait();
        }
    }
}
