namespace WnpTalk.API.Functions.ChatRoomMessage
{
    public class LastestChatRoomMessage
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User.User UserRoomInfo { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime SendDateTime { get; set; }
        public bool IsRead { get; set; }
    }
}
