namespace WnpTalk.API.Entities
{
    public class TblChatRoomMessage
    {
        public int Id { get; set; }
        public int FromUserId { get; set; }
        public int ToRoomId { get; set; }
        public string Content { get; set; } = null!;
        public DateTime SendDateTime { get; set; }
        public bool IsRead { get; set; }
    }
}
