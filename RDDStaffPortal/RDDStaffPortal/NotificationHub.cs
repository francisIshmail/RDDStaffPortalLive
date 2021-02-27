using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace RDDStaffPortal
{
    [AllowAnonymous]
    public class NotificationHub : Hub
    {
        
        //public void Hello()
        //{
        //    Clients.All.hello();
        //}

        //[HubMethodName("sendMessages")]
        //public static void SendMessages()
        //{
        //    IHubContext context = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
        //    context.Clients.All.updateMessages();
        //}
        //public static void Send()
        //{

        //    IHubContext context = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
        //    context.Clients.All.displayStatus();
        //}
    }
}