using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Web3.Startup))]
namespace Web3
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
