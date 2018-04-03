using KalikoCMS.ServiceLocator;
using KalikoCMS.UI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace TestSiteCore
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.FileProviders.Add(new CmsEmbeddedFileProvider());
            });

            SimpleInjectorProvider.Registere(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            SimpleInjectorProvider.InitializeContainer(app);

            var log = new LoggerConfiguration()
                .WriteTo.RollingFile("log-{Date}.txt")
                .CreateLogger();
            //log.Information("Serilog init");
            Log.Logger = log;


            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            //app.Run(async (context) =>
            //{
            //    var tester = new Tester();
            //    await context.Response.WriteAsync(tester.WhoAmI);
            //});
        }
    }
}
