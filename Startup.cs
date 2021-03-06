using PerfectPolicyQuizTwo.Models.QuestionModel;
using PerfectPolicyQuizTwo.Models.QuizModel;
using PerfectPolicyQuizTwo.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerfectPolicyQuizTwo
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
            services.AddControllersWithViews();

            // create an in memory Database for storing session content
            services.AddDistributedMemoryCache();

            // Define the session parameters
            services.AddSession(opts =>
            {
                opts.IdleTimeout = TimeSpan.FromMinutes(3);
                opts.Cookie.HttpOnly = true;
                opts.Cookie.IsEssential = true;
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IApiRequest<Question>, ApiRequest<Question>>();
            //services.AddSingleton<IApiRequest<Teacher>, ApiTestRequest<Teacher>>();
            services.AddSingleton<IApiRequest<Quiz>, ApiRequest<Quiz>>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
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

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
