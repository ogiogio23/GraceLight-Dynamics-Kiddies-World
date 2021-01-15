using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GldKiddiesWorld.Startup))]
namespace GldKiddiesWorld
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
