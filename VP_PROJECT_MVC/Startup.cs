using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VP_PROJECT_MVC.Startup))]
namespace VP_PROJECT_MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
