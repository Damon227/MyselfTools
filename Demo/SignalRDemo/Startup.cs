using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KC.Foundation;
using KC.Foundation.Utilities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SignalRDemo
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //services.AddCors(options => options.AddPolicy("CorsPolicy",
            //    builder => builder.AllowAnyMethod()
            //        .AllowAnyHeader()
            //        .WithOrigins("http://localhost:5000")
            //        .AllowCredentials()));

            // 注入SignalR
            
            services.AddSignalR();

            services.AddSingleton(_ => new ObjectPool<Guid>(() => ID.NewSequentialGuid()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            // 添加SignalR路由
            app.UseSignalR(routes => routes.MapHub<MessageHub>("/messagehub"));
            
            app.UseMvc();
        }
    }
}
