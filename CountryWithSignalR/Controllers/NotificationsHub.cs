using Microsoft.AspNet.SignalR;

namespace CountryWithSignalR.Controllers
{
    public class NotificationsHub : Hub
    {
        public void NotifyAllClients(string msg, string name)
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<NotificationsHub>();
            context.Clients.All.displayNotification(msg,name);
        }
    }
}