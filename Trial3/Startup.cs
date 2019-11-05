using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Trial3.Startup))]
namespace Trial3
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
