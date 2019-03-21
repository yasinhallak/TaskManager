using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Localization.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using TaskManager.Data;
using TaskManager.Utility;
using TaskManager.Extensions;
using TaskManager.hubs;
using Rotativa;
using Rotativa.AspNetCore;
using System.Data.SqlClient;
using System.Resources;
using Microsoft.AspNetCore.Mvc.Routing;
using TaskManager.Enums;

namespace TaskManager
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            MappingProfile.Run();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {


            services.AddLocalization(opts => { opts.ResourcesPath = "AdamResurces"; });
            //services.Configure<CookiePolicyOptions>(options =>
            //{
            //    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            //    options.CheckConsentNeeded = context => true;
            //    options.MinimumSameSitePolicy = SameSiteMode.None;
            //});
            services.AddLocalization();
            services.Configure<RouteOptions>(options =>
            {
                options.ConstraintMap.Add("lang", typeof(LanguageRouteConstraint));
            });
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<IdentityUser, IdentityRole>(
                    option =>
                    {
                        option.Password.RequireDigit = false;
                        option.Password.RequiredLength = 6;
                        option.Password.RequireNonAlphanumeric = false;
                        option.Password.RequireUppercase = false;
                        option.Password.RequireLowercase = false;
                    }
                    )
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultUI()
                .AddDefaultTokenProviders();
            services.AddScoped<IDbInitializer, DbInitializer>();

            services.AddMvc()

                .AddViewLocalization(
                    LanguageViewLocationExpanderFormat.Suffix,
                    opts => { opts.ResourcesPath = "AdamResurces"; })
                .AddDataAnnotationsLocalization()
              .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new List<CultureInfo>
                {
                    new CultureInfo("en-US"),
                    new CultureInfo("ar-SY"),
                };
                options.DefaultRequestCulture = new RequestCulture("en-US");
                // options.DefaultRequestCulture = new RequestCulture(culture: "en-US", uiCulture: "en-US");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
                //options.RequestCultureProviders = new[]{ new RouteDataRequestCultureProvider{
                //    IndexOfCulture=1,
                //    IndexofUICulture=1
                //}};
            });
            //services.Configure<RouteOptions>(options =>
            //{
            //    options.ConstraintMap.Add("culture", typeof(LanguageRouteConstraint));
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IDbInitializer dbInitializer)
        {
            var locOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(locOptions.Value);

            if (env.IsDevelopment())
            {

                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            var options = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(options.Value);
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();
            dbInitializer.Initialize();
            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //        name: "LocalizedDefault",
            //        template: "{culture:culture}/{area=Admin}/{controller=Employee}/{action=Index}/{id?}"
            //    );

            //    routes.MapRoute(
            //        name: "default",
            //        template: "{area=Admin}/{controller=Home}/{action=Index}/{id?}");

            //    routes.MapRoute(
            //       name: "default",
            //       template: "{*catchall}",
            //       defaults: new { controller = "Home", action = "RedirectToDefaultLanguage", culture = "en" });
            //});
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "Localization",
                    template: "{area=Admin}/{controller=city}/{action=index}/{id?}"
                );
               
                //routes.MapRoute(
                //    name: "default",
                //    template: "{*catchall}",
                //    defaults: new { controller = "task", action = "RedirectToDefaultLanguage", culture = "en" });
            });


            RotativaConfiguration.Setup(env);
        }

        //LanguageRouteConstraint class is below.You may put this into Startup.cs or anywhere
        //public class LanguageRouteConstraint : IRouteConstraint
        //{
        //    public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)

        //    {

        //        if (!values.ContainsKey("culture"))
        //            return false;

        //        var culture = values["culture"].ToString();
        //        return culture == "en" || culture == "ar";
        //    }
        //}
        // Overrided RouteDataRequestCultureProvider class is below.It is like ASP.NET's implemantation but this one is working :)
        //public class RouteDataRequestCultureProvider : RequestCultureProvider
        //{
        //    public int IndexOfCulture;
        //    public int IndexofUICulture;

        //    public override Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
        //    {
        //        if (httpContext == null)
        //            throw new ArgumentNullException(nameof(httpContext));

        //        string culture = null;
        //        string uiCulture = null;

        //        var twoLetterCultureName = httpContext.Request.Path.Value.Split('/')[IndexOfCulture]?.ToString();
        //        var twoLetterUICultureName = httpContext.Request.Path.Value.Split('/')[IndexofUICulture]?.ToString();

        //        if (twoLetterCultureName == "ar")
        //            culture = "ar-SY";
        //        else if (twoLetterCultureName == "en")
        //            culture = uiCulture = "en-US";

        //        if (twoLetterUICultureName == "ar")
        //            culture = "ar-SY";
        //        else if (twoLetterUICultureName == "en")
        //            culture = uiCulture = "en-US";

        //        if (culture == null && uiCulture == null)
        //            return NullProviderCultureResult;

        //        if (culture != null && uiCulture == null)
        //            uiCulture = culture;

        //        if (culture == null && uiCulture != null)
        //            culture = uiCulture;

        //        var providerResultCulture = new ProviderCultureResult(culture, uiCulture);

        //        return Task.FromResult(providerResultCulture);
        //    }
        //}

        public class LanguageRouteConstraint : IRouteConstraint
        {
            public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
            {
                if (!values.ContainsKey("lang"))
                {
                    return false;
                }

                var lang = values["lang"].ToString();

                return lang == "ee" || lang == "en" || lang == "ru";
            }
        }
        public class LocalizationPipeline
        {
            public void Configure(IApplicationBuilder app)
            {

                var supportedCultures = new List<CultureInfo>
                {
                    new CultureInfo("et"),
                    new CultureInfo("en"),
                    new CultureInfo("ru"),
                };

                var options = new RequestLocalizationOptions()
                {

                    DefaultRequestCulture = new RequestCulture(culture: "et", uiCulture: "et"),
                    SupportedCultures = supportedCultures,
                    SupportedUICultures = supportedCultures
                };

                options.RequestCultureProviders = new[] { new RouteDataRequestCultureProvider() { Options = options, RouteDataStringKey = "lang", UIRouteDataStringKey = "lang" } };

                app.UseRequestLocalization(options);
            }
        }
    }
}
