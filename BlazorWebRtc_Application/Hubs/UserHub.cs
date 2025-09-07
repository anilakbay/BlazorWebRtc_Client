using BlazorWebRtc_Application.Interface.Services.Manager;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System.Security.Claims;

namespace BlazorWebRtc_Application.Hubs
{
    public class UserHub : Hub
    {
        private readonly IConnectionManager connectionManager;
        public UserHub(IConnectionManager connectionManager)
        {
            this.connectionManager = connectionManager;
        }

        public override Task OnConnectedAsync()
        {
            var connectionId = Context.ConnectionId;
            var userId = Context.GetHttpContext()?.Request.Query["userId"].ToString();
            connectionManager.AddConnection(userId, connectionId);
            var result = connectionManager.GetAllUserIds();


            Clients.All.SendAsync("UserStatusChanged", JsonConvert.SerializeObject(result), true).GetAwaiter();

            return base.OnConnectedAsync();
        }




        public override Task OnDisconnectedAsync(Exception? exception)
        {
            var connectionId = Context?.ConnectionId;
            connectionManager.RemoveConnection(connectionId);
            var result = connectionManager.GetAllUserIds();


            Clients.All.SendAsync("UserStatusChanged", JsonConvert.SerializeObject(result), true).GetAwaiter();
            return base.OnDisconnectedAsync(exception);
        }
    }
}