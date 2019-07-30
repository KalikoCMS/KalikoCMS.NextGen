namespace TestSiteCore {
    using KalikoCMS.Mvc;
    using KalikoCMS.UI;
    using KalikoCMS.Mvc.Extensions;
    using KalikoCMS.Mvc.Framework;
    using KalikoCMS.Data.InMemory;
    using KalikoCMS.Data.SqlServer;
    using KalikoCMS.ServiceLocation;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Razor;
    using Microsoft.Extensions.DependencyInjection;
    using Serilog;

    public class Startup {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services) {
            services.AddMemoryCache();
            services.AddMvc(options => {
                // add custom binder to beginning of collection
                options.ModelBinderProviders.Insert(0, new CmsPageBinderProvider());
            });

            services.Configure<RazorViewEngineOptions>(options => {
//TODO: Fix                options.FileProviders.Add(new CmsEmbeddedFileProvider());
            });

            //services.AddDbContext<InMemoryCmsContext>();

            SimpleInjectorProvider.RegisterServices(services);
            SimpleInjectorProvider.RegisterDataProvider<SqlServerCmsContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            var simpleInjectorProvider = new SimpleInjectorProvider();
            simpleInjectorProvider.InitializeContainer(app);

            var log = new LoggerConfiguration()
                .WriteTo.RollingFile("Logs\\log-{Date}.log")
                .CreateLogger();

            Log.Logger = log;

            app.UseStaticFiles();

            BuildTestSite();

            app.UseMvc(routes => {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

        }

        private void BuildTestSite() {

        }
    }
}