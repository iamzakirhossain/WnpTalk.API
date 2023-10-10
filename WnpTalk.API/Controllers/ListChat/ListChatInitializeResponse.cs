using WnpTalk.API.Functions.Message;
using WnpTalk.API.Functions.User;

namespace WnpTalk.API.Controllers.ListChat
{
    public class ListChatInitializeResponse
    {
        public User User { get; set; } = null!;
        public IEnumerable<User> UserFriends { get; set; } = null!;
        public IEnumerable<LastestMessage> LastestMessages { get; set; } = null!;
    }
}
