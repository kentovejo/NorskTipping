using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Presentation.Web.Startup))]
namespace Presentation.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {           
            //app.MapSignalR();
            //ConfigureAuth(app);
        }
    }
}