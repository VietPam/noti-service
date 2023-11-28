using Microsoft.AspNetCore.SignalR;
using Serilog;

namespace noti_service.Hubs
{
    public class NotiHub: Hub
    {
        public NotiHub() { }

        public override Task OnConnectedAsync()
        {
            string id = Context.ConnectionId;
            Log.Information(string.Format("User: new connection {0}", id));
            return base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            string id = Context.ConnectionId;
            Log.Information(string.Format("User: remove client {0}", id));
            await Program.api_user.disconnectUserAsync(id);
            await base.OnDisconnectedAsync(exception);
        }

        public async Task UpdateUser(string code)
        {
            string id= Context.ConnectionId;
            if (await Program.api_user.updateUserAsync(id, code))
            {
                Log.Information(string.Format("Reconnect ok"));
                await Clients.Caller.SendAsync("UpdateUser", "ok");
            }
            else
            {
                Log.Information(string.Format("Reconnect fail"));
                await Clients.Caller.SendAsync("UpdateUser", "fail");
            }
        }

        public async Task ConnectUser()
        {
            string id = Context.ConnectionId;
            bool tmp = Program.api_noti.getListNotiSignalR(id);
            if(tmp)
            {
                Log.Information(string.Format("Send message ok"));
            }
            else
            {
                Log.Information(string.Format("Send message fail"));
            }
        }
    }
}
