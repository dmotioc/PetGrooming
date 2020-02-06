using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PetGroomingApplication.Startup))]
namespace PetGroomingApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
