using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Assig.Startup))]
namespace Assig
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
