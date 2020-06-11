using Microsoft.AspNetCore.Hosting;
using Presentation_MVC.Areas.Identity;

[assembly: HostingStartup(typeof(IdentityHostingStartup))]

namespace Presentation_MVC.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => { });
        }
    }
}