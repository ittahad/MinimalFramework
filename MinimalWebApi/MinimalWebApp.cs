﻿using Microsoft.AspNetCore.Builder;
using MinimalFramework;
using Swashbuckle.AspNetCore.SwaggerGen.ConventionalRouting;

namespace MinimalWebApi
{
    public class MinimalWebApp
    {
        public WebApplication? Application;

        private MinimalWebAppOptions _options;
        
        public MinimalWebApp(MinimalWebAppOptions options = null)
        {
            _options = options == null ? new MinimalWebAppOptions() { 
                UseSwagger = false,
                CommandLineArgs = new string[] { },
            } : options;
        }


        public void Start(Action<WebApplication> application = null)
        {
            if (Application == null) throw new Exception();
            Application.UseRouting();
            Application.UseAuthentication();
            Application.UseAuthorization();

            Application.UseMvc(config =>
            {
                string serviceName = Application.Configuration["ServiceName"];
                config.MapRoute(
                    name: "default",
                    template: serviceName + "/{controller}/{action}/{id?}");
                ConventionalRoutingSwaggerGen.UseRoutes(config.Routes);
            });

            if (_options.UseSwagger.HasValue && _options.UseSwagger.Value)
            {
                Application
                    .UseSwagger()
                    .UseSwaggerUI();
            }

            if (_options.StartUrl != null)
            {
                Application.Urls.Add(_options.StartUrl);
            }

            if (application != null)
            {
                application.Invoke(Application);
            }

            Application.Run();
        }

    }
}
