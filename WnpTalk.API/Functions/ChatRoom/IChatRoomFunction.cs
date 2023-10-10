namespace WnpTalk.API.Functions.ChatRoom
{
    public interface IChatRoomFunction
    {
        Task<IEnumerable<User.User>> GetListChatRoom(int userId);
    }
}
