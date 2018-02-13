using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VendeeCrop.Startup))]
namespace VendeeCrop
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
