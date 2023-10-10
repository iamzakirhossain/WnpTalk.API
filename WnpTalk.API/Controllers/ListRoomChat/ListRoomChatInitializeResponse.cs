using WnpTalk.API.Functions.ChatRoomMessage;
using WnpTalk.API.Functions.Message;
using WnpTalk.API.Functions.User;

namespace WnpTalk.API.Controllers.ListRoomChat
{
    public class ListRoomChatInitializeResponse
    {
        public User User { get; set; } = null!;
        public IEnumerable<User> UserRooms { get; set; } = null!;
        public IEnumerable<LastestChatRoomMessage> LastestChatRoomMessages { get; set; } = null!;
    }
}
