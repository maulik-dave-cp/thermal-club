using ThermalClub.Modules.Core.Authorization;
using ThermalClub.Modules.Core.Data;
using ThermalClub.Modules.Core.Modules;
using ThermalClub.Modules.Core.ViewEngine;
using ThermalClub.Modules.CurrentProject.Helpers;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;

namespace ThermalClub
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public ILifetimeScope AutofacContainer { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var config = new ThermalConfiguration();
            Configuration.Bind("DefaultConnectionString", Configuration.GetConnectionString("DefaultConnectionString"));
            Configuration.Bind("Aj", config);
            services.AddSingleton(config);
         

            services.AddDbContext<SqlContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnectionString")), ServiceLifetime.Transient);

            // Add Hangfire services.
            services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(Configuration.GetConnectionString("DefaultConnectionString"), new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    DisableGlobalLocks = true
                }));

            // Add the processing server as IHostedService
            services.AddHangfireServer();

            
            //var awsOption = Configuration.GetAWSOptions();
            //awsOption.Credentials = new BasicAWSCredentials(Configuration["AWS:AccessKey"], Configuration["AWS:SecretKey"]);
            //services.AddDefaultAWSOptions(awsOption);
            //services.AddAWSService<IAmazonS3>();
            //services.AddAWSService<IAmazonTextract>();

            services.AddRouting(options => options.LowercaseUrls = true);

            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.ViewLocationExpanders.Add(new AjViewLocationExpander());
            });

            services.AddAutoMapper(typeof(Startup));
            services.AddControllersWithViews();

            services.AddMvc(option => option.EnableEndpointRouting = true)
                .AddControllersAsServices()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
            });

            services.AddControllersWithViews();

            //services.AddResponsiveFileManager(options =>
            //{
            //    //
            //    options.MaxSizeUpload = 32;
            //});

            // Add services to the collection. Don't build or return
            // any IServiceProvider or the ConfigureContainer method
            // won't get called. Don't create a ContainerBuilder
            // for Autofac here, and don't call builder.Populate() - that
            // happens in the AutofacServiceProviderFactory for you.
            services.AddOptions();
        }

        // ConfigureContainer is where you can register things directly
        // with Autofac. This runs after ConfigureServices so the things
        // here will override registrations made in ConfigureServices.
        // Don't build the container; that gets done for you by the factory.
        public void ConfigureContainer(ContainerBuilder builder)
        {
            // Register your own things directly with Autofac here. Don't
            // call builder.Populate(), that happens in AutofacServiceProviderFactory
            // for you.
            builder.RegisterModule(new AutofacModule());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // If, for some reason, you need a reference to the built container, you
            // can use the convenience extension method GetAutofacRoot.
            AutofacContainer = app.ApplicationServices.GetAutofacRoot();

            if (env.IsDevelopment())
            {
                app.UseRouteDebugger("/core/routes");
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseSession();

            //app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseResponseCaching();

            //app.UseResponsiveFileManager();

            app.UseRouting();

            app.UseAuthorization();

            app.UseAjEndpoints();

            ModuleRegister.RegisterBackgroundJobs();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapControllerRoute(
                //    name: "default",
                //    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints
                    .MapHangfireDashboard("/hangfire-jobs", new DashboardOptions()
                    {
                        // We need to pass an empty list or Hangfire will use a local authorization filter
                        Authorization = new[]
                        {
                            new HangfireDashboardAuthorizationFilter()
                        },
                        IgnoreAntiforgeryToken = true
                    });
            });
        }
    }
}
