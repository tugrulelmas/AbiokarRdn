using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AbiokaRdn.Host
{
    public class Startup
    {
        private readonly Infrastructure.Framework.RestHelper.Startup startup;

        public Startup(IHostingEnvironment env) {
            startup = new Infrastructure.Framework.RestHelper.Startup(env);
        }

        public IConfigurationRoot Configuration => startup.Configuration;

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            startup.ConfigureServices(services, ApplicationService.Bootstrapper.Initialise);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            startup.Configure(app, env, loggerFactory);
        }
    }
}
