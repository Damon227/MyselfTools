using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CQ.Foundation.AspNetCore.Filters;
using IdentityDemo.Authentications;
using IdentityDemo.Authorization;
using IdentityDemo.Middlewares;
using IdentityDemo.Swagger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using SwaggerGenOptions = Swashbuckle.AspNetCore.SwaggerGen.SwaggerGenOptions;

namespace IdentityDemo
{
    public class Startup
    {
        public Startup(IHostingEnvironment env, IConfiguration configuration)
        {
            HostingEnvironment = env;
            Configuration = configuration;
        }

        public IHostingEnvironment HostingEnvironment { get; }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddAuthentication().AddSessionAuthentication();
            services.AddSingleton<IAuthorizationHandler, PermissionRequirementHandler>();

            // See more at https://docs.microsoft.com/en-us/aspnet/core/security/authorization/introduction
            services.AddAuthorization(options =>
            {
                options.AddPolicy("User", policy=>policy.Requirements.Add(new PermissionRequirement("User")));
                options.AddPolicy("Admin", policy => policy.Requirements.Add(new PermissionRequirement("Admin")));
            });

            services.AddSwaggerGen(options =>
            {
                string defaultDocName = HostingEnvironment.ApplicationName;

                options.SwaggerDoc(defaultDocName, new Info
                {
                    Title = Configuration["Application:Name"] ?? $"{typeof(Startup).Namespace}.Docs",
                    Version = Configuration["Application:Version"] ?? "1.0",
                    Description = Configuration["Application:Description"] ?? $"Api of {typeof(Startup).Namespace}"
                });
                options.SwaggerDoc("1_0", new Info
                {
                    Title = Configuration["Application:Name"] ?? $"{typeof(Startup).Namespace}.Docs",
                    Version = "1.0",
                    Description = Configuration["Application:Description"] ?? $"Api of {typeof(Startup).Namespace}"
                });
                options.SwaggerDoc("2_0", new Info
                {
                    Title = Configuration["Application:Name"] ?? $"{typeof(Startup).Namespace}.Docs",
                    Version = "2.0",
                    Description = Configuration["Application:Description"] ?? $"Api of {typeof(Startup).Namespace}"
                });
                options.CustomSchemaIds(t => t.FullName);
                options.DescribeAllEnumsAsStrings();
                options.DescribeStringEnumsInCamelCase();
                options.DescribeAllParametersInCamelCase();
                options.DocInclusionPredicate(Swagger.SwaggerGenOptions.GetDocInclusionPredicate(defaultDocName));
                options.DocumentFilter<DocumentFilter>();
                options.IgnoreObsoleteActions();
                options.IgnoreObsoleteProperties();
                options.OperationFilter<OperationFilter>();
                options.SchemaFilter<SchemaFilter>();
                options.TagActionsBy(Swagger.SwaggerGenOptions.TagSelector);
                options.IncludeXmlComments(Path.Combine(HostingEnvironment.WebRootPath, HostingEnvironment.ApplicationName + ".xml"));
            });

            services.AddUndulicate();
            //services.AddUndulicateWithRedis(Configuration.GetSection("RedisOptions"));
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                //app.UseHsts();
            }

            app.UseMiddleware<InternalServerErrorExceptionHandlerMiddleware>();
            app.UseMiddleware<SessionMiddleware>();
            app.UseMiddleware<AuthenticationMiddleware>();

            app.UseHttpsRedirection();
            app.UseMvc();

            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                options.DocumentTitle = "Api Documents of " + GetType().Namespace;
                options.RoutePrefix = "docs";
                //options.DefaultModelRendering(ModelRendering.Example);
                options.DisplayRequestDuration();
                //options.DocExpansion(DocExpansion.None);
                options.EnableDeepLinking();
                options.EnableFilter();
                options.EnableValidator();
                options.SwaggerEndpoint($"/swagger/{HostingEnvironment.ApplicationName}/swagger.json", HostingEnvironment.ApplicationName);
                options.SwaggerEndpoint("/swagger/1_0/swagger.json", HostingEnvironment.ApplicationName + " - 1.0");
                options.SwaggerEndpoint("/swagger/2_0/swagger.json", HostingEnvironment.ApplicationName + " - 2.0");
            });
        }
    }
}
