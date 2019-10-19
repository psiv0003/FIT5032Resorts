using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EFolio_Take10.Startup))]
namespace EFolio_Take10
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
