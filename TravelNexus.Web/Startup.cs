using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(TravelNexus.Web.Startup))]

namespace TravelNexus.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {

            ConfigureAuth(app);   
        }

        
    }
}
