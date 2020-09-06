using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ZavoshSoftware.Startup))]
namespace ZavoshSoftware
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
