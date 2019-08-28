using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Tabim.Core.Bll;
using Tabim.Core.Bll.Admin;
using Tabim.Core.Bll.Egitim;
using Tabim.Core.Bll.SMS;
using Tabim.Core.Bll.WebSite;
using Tabim.Core.Data.Context;
using Tabim.Core.Data.Model.Core;
using Tabim.Core.Service.Helpers;
using Tabim.Ortak.Bll.Paramtrink;
using Tabim.UyeTakip.Bll;

namespace Tabim.Core.Service
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            /* services
                 .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                 .AddJwtBearer(options =>
                 {
                     options.TokenValidationParameters = new TokenValidationParameters
                     {
                         ValidateIssuer = false,
                         ValidateAudience = false,
                         ValidateLifetime = true,
                         ValidateIssuerSigningKey = true,
                         ValidIssuer = Configuration["Settings:TokenIssuer"],
                         ValidAudience = Configuration["Settings:TokenAudience"],
                         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Settings:TokenSecurityKey"]))
                     };
                 });*/
            //services.AddCors();

               services
                .AddDbContext<TabimDbContext>(opt => opt.UseSqlServer(Configuration["Settings:ConnectionString"]));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt => {
                 opt.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuerSigningKey = true,
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["Settings:TokenSecurityKey"])),
                     ValidateIssuer = false,
                     ValidateAudience = false,
                     ValidateLifetime = false,
                     ValidIssuer = Configuration.GetValue<string>("JwtIssuer"),
                     ValidAudience = Configuration.GetValue<string>("JwtAudience"),
                    
                 };
             });
            /*
            services.AddAuthorization(options =>
            {
                //options.AddPolicy("role içerisinde kontrol", policy => policy. policy.RequireRole("SuperAdministrator", "ChannelAdministrator"));
                //policy.RequereClaim verdiğin haklar içinden şart kontorlü
                options.AddPolicy("DomainAuthorization", policy => policy.Requirements.Add())//
            });
           */

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder//.WithOrigins("https://uygulama.ehliyetadresi.com", "http://localhost:4200", "http://localhost:4201")
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
                        .SetIsOriginAllowedToAllowWildcardSubdomains()
                        .Build();
                    });
            });

            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

            services.AddSingleton(Configuration);

            services.AddTransient<IUsers, Users>();
            services.AddTransient<IApplicationAccounts, ApplicationAccounts>();
            services.AddTransient<IEgitim, Egitim>();
            services.AddTransient<IWebSite, WebSite>();
            services.AddTransient<ISMSFactory, SMSFactory>();
            services.AddTransient<IOrtak, Tabim.Core.Bll.Ortak>();
            services.AddTransient<IDomainIslemleri, DomainIslemleri>();
            services.AddTransient<IKurumIslemleri, KurumIslemleri>();
            services.AddTransient<IOdemeIslemleri, OdemeIslemleri>();
            services.AddTransient<IUyeIslemleri, UyeIslemleri>();


            //services.Configure<CloudinarySettings>(Configuration.GetSection("CloudinarySettings"));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();
           // app.UseCors("AllowAll");
            app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().AllowCredentials());
            app.UseAuthentication();
            app.UseMvc();

        }
    }
}
