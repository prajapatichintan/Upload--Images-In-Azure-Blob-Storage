using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ImgStoreOnAzure.Startup))]
namespace ImgStoreOnAzure
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
