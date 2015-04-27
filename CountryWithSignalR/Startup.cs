using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(CountryWithSignalR.Startup))]

namespace CountryWithSignalR
{
    public class Startup
    {
          public void Configuration(IAppBuilder app)
            {
                app.MapSignalR();
            }
        
    }
}
