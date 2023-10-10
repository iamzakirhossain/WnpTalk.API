using WnpTalk.API.Functions.Message;

namespace WnpTalk.API.Functions.ChatRoomMessage
{
    public interface IChatRoomMessageFunction
    {
        Task<int> AddChatRoomMessage(int fromUserId, int toRoomId, string message);
        Task<IEnumerable<LastestChatRoomMessage>> GetLastestChatRoomMessage(int userId);
        Task<IEnumerable<ChatRoomMessage>> GetChatRoomMessages(int fromUserId, int toRoomId);
        Task<IEnumerable<ChatRoomMessage>> GetChatRoomMessagesAll();
    }
}
