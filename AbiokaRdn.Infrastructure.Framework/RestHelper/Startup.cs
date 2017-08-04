using AbiokaRdn.Infrastructure.Common.IoC;
using AbiokaRdn.Infrastructure.Framework.IoC;
using Castle.MicroKernel.Lifestyle;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AbiokaRdn.Infrastructure.Framework.RestHelper
{
    public class Startup
    {
        public Startup(IHostingEnvironment env) {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services, Action action) {
            // Configure regular ASP.NET Core services
            services.AddMvc();

            var container = new CastleContainer();
            DependencyContainer.SetContainer(container);

            container.Container.Populate(services);

            Bootstrapper.Initialise();
            action();

            container.Container.BeginScope();
            return DependencyContainer.Container.Resolve<IServiceProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, Microsoft.Extensions.Logging.ILoggerFactory loggerFactory) {
            app.UseMvc();
        }
    }
}
