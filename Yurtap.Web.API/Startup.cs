using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Yurtap.Business.Abstract;
using Yurtap.Business.Concrete;
using Yurtap.Core.DataAccess.EntityFramework;
using Yurtap.DataAccess.Abstract;
using Yurtap.DataAccess.Concrete.EntityFramework;
using Yurtap.Web.API.Middlewares;

namespace Yurtap.Web.API
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            //services.AddDbContext<YurtapDbContext>(x => x.UseSqlServer(Configuration["AppSettings:ConnectionString"]));
            services.AddTransient<IKisiDal, EfKisiDal>();
            services.AddTransient<IKisiBll, KisiBll>();
            services.AddTransient<IOgrenciDal, EfOgrenciDal>();
            services.AddTransient<IOgrenciBll, OgrenciBll>();
            services.AddTransient<IPersonelDal, EfPersonelDal>();
            services.AddTransient<IPersonelBll, PersonelBll>();
            services.AddTransient<IYoklamaDal, EfYoklamaDal>();
            services.AddTransient<IYoklamaService, YoklamaBll>();
            services.AddTransient<IYoklamaBaslikDal, EfYoklamaBaslikDal>();
            services.AddTransient<IYoklamaBaslikBll, YoklamaBaslikBll>();
            services.AddTransient<IKullaniciDal, EfKullaniciDal>();
            services.AddTransient<IKullaniciBll, KullaniciBll>();
            services.AddTransient<IKullaniciRolBll, KullaniciRolBll>();
            services.AddTransient<IKullaniciRolDal, EfKullaniciRolDal>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    //istek karşılığnda encrypt edilen token başkaları tarafından encrypt edilmesin diye
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("TokenSettings:TokenKey").Value)),
                    //Kullanıcıyı doğrulamama 
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };
            });

            //services.AddCors(options =>
            //{
            //    options.AddPolicy("AllowAll",
            //        builder =>
            //        {
            //            builder//.WithOrigins("https://uygulama.ehliyetadresi.com", "http://localhost:4200", "http://localhost:4201")
            //            .AllowAnyOrigin()
            //            .AllowAnyMethod()
            //            .AllowAnyHeader()
            //            .AllowCredentials()
            //            .SetIsOriginAllowedToAllowWildcardSubdomains()
            //            .Build();
            //        });
            //});


            //services.AddCors(options =>
            //{
            //    options.AddPolicy("AllowMyOrigin",
            //    builder => builder.WithOrigins("http://localhost:4200", "http://localhost:52609"));
            //});

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.ConfigureExceptionHandler();

            app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().AllowCredentials());
            app.UseAuthentication();
            app.UseMiddleware<AuthenticationMiddleware>();
            app.UseMvc();

        }
    }
}
