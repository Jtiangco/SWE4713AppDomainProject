using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Project_for_App_Domain.Startup))]
namespace Project_for_App_Domain
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
