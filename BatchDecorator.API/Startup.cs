using BatchDecorator.API.Decorators;
using BatchDecorator.API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using BatchDecorator.API.Models;
using BatchDecorator.API.Workers;
using Hangfire;
using Hangfire.MemoryStorage;

namespace BatchDecorator.API
{
    public class Startup
    {
        public static IServiceProvider ServiceProvider;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddTransient<IBatchProcess, BatchProcess>();
            services.AddTransient<FileDownloadDecorator>();
            services.AddTransient<ExecuteSPDecorator>();
            services.AddTransient<FileUploadDecorator>();

            #region ---HangFire---
            services.AddHangfire(config => { config.UseMemoryStorage(); });
            #endregion

            #region ---Configure---

            services.Configure<FTPConfigModel>(Configuration.GetSection("FTP"));

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            #region ---HangFire---
            app.UseHangfireDashboard();
            app.UseHangfireServer();
            // every minute
            RecurringJob.AddOrUpdate<BatchWorker>(x => x.DoWork(), cronExpression: "0 * * ? * *", TimeZoneInfo.Local);

            #endregion

            ServiceProvider = app.ApplicationServices;

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
