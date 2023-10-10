using Microsoft.AspNetCore.SignalR;
using WnpTalk.API.Functions.ChatRoomMessage;
using WnpTalk.API.Functions.Message;
using WnpTalk.API.Helpers;

namespace WnpTalk.API.Controllers.ChatHub
{
    public class ChatHub : Hub
    {
        UserOperator _userOperator;
        IMessageFunction _messageFunction;
        IChatRoomMessageFunction _chatRoomMessageFunction;
        private static readonly Dictionary<int, string> _connectionMapping
            = new Dictionary<int, string>();

        public ChatHub(UserOperator userOperator, IMessageFunction messageFunction, IChatRoomMessageFunction chatRoomMessageFunction)
        {
            _userOperator = userOperator;
            _messageFunction = messageFunction;
            _chatRoomMessageFunction = chatRoomMessageFunction;
        }

        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }

        public async Task SendMessageToRoom(int fromUserId, int toRoomId, string message)
        {

            var connectionIds = _connectionMapping.Where(x => x.Key == toRoomId)
                                                    .Select(x => x.Value).ToList();

            await _chatRoomMessageFunction.AddChatRoomMessage(fromUserId, toRoomId, message);

            //await Clients.Clients(connectionIds)
            //    .SendAsync("ReceiveMessage", fromUserId, message);

            await Clients.All.SendAsync("ReceiveMessage", fromUserId, message);


        }

        public async Task SendMessageToUser(int fromUserId, int toUserId, string message)
        {
            var connectionIds = _connectionMapping.Where(x => x.Key == toUserId)
                                                    .Select(x => x.Value).ToList();

            await _messageFunction.AddMessage(fromUserId, toUserId, message);

            await Clients.Clients(connectionIds)
                .SendAsync("ReceiveMessage", fromUserId, message);
        }

        public override Task OnConnectedAsync()
        {
            var userId = _userOperator.GetRequestUser()!.Id;
            if (!_connectionMapping.ContainsKey(userId))
                _connectionMapping.Add(userId, Context.ConnectionId);

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            _connectionMapping.Remove(_userOperator.GetRequestUser()!.Id);
            return base.OnDisconnectedAsync(exception);
        }
    }
}


