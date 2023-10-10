using WnpTalk.API.Functions.User;

namespace WnpTalk.API.Controllers.ChatRoomMessage
{
    public class ChatRoomMessageInitalizeResponse
    {
        public User RoomInfo { get; set; } = null!;
        public IEnumerable<Functions.ChatRoomMessage.ChatRoomMessage> ChatRoomMessages { get; set; } = null!;
    }
}
