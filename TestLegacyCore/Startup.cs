namespace TestLegacyCore {
    using System;
    using KalikoCMS.Mvc;
    using KalikoCMS.Mvc.Framework;
    using KalikoCMS.ServiceLocation;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Serilog;
    using Services;

    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddControllersWithViews(options => {
                options.ModelBinderProviders.Insert(0, new CmsPageBinderProvider());
            });

            // Register transformer to use with endpoint routing
            services.AddScoped<CmsTransformer>();
            // Transformer can be overriden in order to manipulate matching pattern
            //services.AddScoped<CustomCmsTransformer>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }
            else {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                //app.UseHsts();
            }

            var log = new LoggerConfiguration()
                .WriteTo.RollingFile("Logs\\log-{Date}.log")
                .CreateLogger();

            Log.Logger = log;

            //app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapDynamicControllerRoute<CmsTransformer>("{**path}");
                endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}