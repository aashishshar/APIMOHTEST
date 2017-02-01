using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MOHFW_CLIENT_WEBAPP.Startup))]
namespace MOHFW_CLIENT_WEBAPP
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
