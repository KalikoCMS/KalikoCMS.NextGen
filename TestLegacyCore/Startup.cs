namespace TestSiteCore {
    using System;
    using KalikoCMS.Data;
    using KalikoCMS.Mvc;
    using KalikoCMS.UI;
    using KalikoCMS.Mvc.Extensions;
    using KalikoCMS.Mvc.Framework;
    using KalikoCMS.Data.InMemory;
    using KalikoCMS.Data.Repositories.Interfaces;
    using KalikoCMS.Data.SqlServer;
    using KalikoCMS.Legacy.Data;
    using KalikoCMS.Legacy.Data.Repositories;
    using KalikoCMS.ServiceLocation;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Razor;
    using Microsoft.Extensions.DependencyInjection;
    using Serilog;
    using Services;

    public class Startup {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IServiceProvider ConfigureServices(IServiceCollection services) {
            services.AddMemoryCache();
            services.AddMvc(options => {
                // add custom binder to beginning of collection
                options.ModelBinderProviders.Insert(0, new CmsPageBinderProvider());
            });

            services.Configure<RazorViewEngineOptions>(options => {
                options.FileProviders.Add(new CmsEmbeddedFileProvider());
            });

            var dependencyInjectionProvider = new DependencyInjectionProvider();
            return dependencyInjectionProvider.Initialize(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }
            
            var log = new LoggerConfiguration()
                .WriteTo.RollingFile("Logs\\log-{Date}.log")
                .CreateLogger();

            Log.Logger = log;

            app.UseStaticFiles();

            app.UseMvc(routes => {
                routes.MapRoute(name: "home", template: "", defaults: new { controller = "Home", action = "Index" });

                routes.MapCms(false);

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

        }
    }
}