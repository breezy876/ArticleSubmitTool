using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ArticleSubmitTool.Startup))]
namespace ArticleSubmitTool
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
