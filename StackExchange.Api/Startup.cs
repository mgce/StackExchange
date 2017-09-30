using System;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using React.AspNet;
using StackExchange.Bootstraper.Modules;
using StackExchange.Core.Entities;
using StackExchange.Core.Settings;
using StackExchange.Infrastructure;
using StackExchange.Infrastructure.EF;
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
            //React Configuration
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddReact();

            services.AddMvc();

            //DbContext Configuration
            var connection = "Data Source=DESKTOP-477PQQ8;Initial Catalog=StackExchange;Integrated Security=True;Pooling=False";
            services.AddDbContext<Context>(options => options.UseSqlServer(connection));
            services.AddTransient<CompanyInitializer>();

            //JWT Settings
            services.Configure<JWTSettings>(Configuration.GetSection("JWTSettings"));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = Configuration.GetSection("JWTSettings:Issuer").Value,
                    ValidateAudience = true,
                    ValidAudience = Configuration.GetSection("JWTSettings:Audience").Value,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.ASCII.GetBytes(
                            Configuration.GetSection("JWTSettings:SecretKey").Value)),
                };
            }).AddCookie(options =>
            {
                options.LoginPath = "/login";
                options.ExpireTimeSpan = TimeSpan.FromHours(1);
            });

            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<Context>().AddDefaultTokenProviders();

            var builder = new ContainerBuilder();
            builder.Populate(services);
            builder.RegisterModule(new RepositoryModule());
            builder.RegisterModule(new CommandsModule());
            builder.RegisterModule(new OtherModule());

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

            app.UseReact(config =>
            {
                config.AddScript("~/Scripts/First.jsx")
                .AddScript("~/Scripts/Second.jsx");
            });
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
            //companySeeder.Seed().Wait();
        }
    }
}
