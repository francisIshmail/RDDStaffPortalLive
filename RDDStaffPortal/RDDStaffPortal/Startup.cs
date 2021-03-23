using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;



[assembly: OwinStartup(typeof(RDDStaffPortal.Startup))]

namespace RDDStaffPortal
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            
            app.MapSignalR();
        }
    }
}