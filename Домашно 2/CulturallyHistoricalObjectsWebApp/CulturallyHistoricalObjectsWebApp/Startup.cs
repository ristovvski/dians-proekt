using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CulturallyHistoricalObjectsWebApp.Startup))]
namespace CulturallyHistoricalObjectsWebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
