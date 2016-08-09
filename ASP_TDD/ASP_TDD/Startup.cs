using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ASP_TDD.Startup))]
namespace ASP_TDD
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
