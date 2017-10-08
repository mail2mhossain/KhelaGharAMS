using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KhelaGharAMS.Startup))]
namespace KhelaGharAMS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
