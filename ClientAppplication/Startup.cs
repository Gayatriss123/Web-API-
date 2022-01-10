using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ClientAppplication.Startup))]
namespace ClientAppplication
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
