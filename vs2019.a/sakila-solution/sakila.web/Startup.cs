using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
//Linha adicionada devido a atualização do Browser(LiveReload) - Arquivos modificados launchSettings.json, sakila.web.csproj e statup.cs
using Westwind.AspNetCore.LiveReload;

namespace sakila.web
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
            //Bloco if adicionado devido a atualização do Browser(LiveReload) - Arquivos modificados launchSettings.json, sakila.web.csproj e statup.cs
            //if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            //{    
                //services.AddLiveReload(config =>
                //{
                //    config.LiveReloadEnabled = true;
                //    config.ClientFileExtensions = ".cshtml,.css,.js,.htm,.html";
                //    config.FolderToMonitor = "~/../";
                //});
            //}

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //Linha adicionada devido a atualização do Browser(LiveReload) - Arquivos modificados launchSettings.json, sakila.web.csproj e statup.cs
                //app.UseLiveReload();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
