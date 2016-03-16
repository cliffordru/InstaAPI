using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(InstaAPI.Startup))]

namespace InstaAPI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
