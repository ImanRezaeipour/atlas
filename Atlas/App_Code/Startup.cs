using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(OnlineTrafficsStartup))]
public class OnlineTrafficsStartup
{
    public void Configuration(IAppBuilder app)
    {
        app.MapSignalR();
    }
}

