using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ASPPresentation.Startup))]
namespace ASPPresentation
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
