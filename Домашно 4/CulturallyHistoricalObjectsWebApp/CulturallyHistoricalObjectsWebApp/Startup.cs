using CulturallyHistoricalObjectsWebApp.Data;
using CulturallyHistoricalObjectsWebApp.Models;
using Microsoft.Owin;
using Owin;
using System.Data.Entity;

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
