using Microsoft.EntityFrameworkCore;
using WnpTalk.API.Data;
using WnpTalk.API.Functions.User;

namespace WnpTalk.API.Functions.ChatRoom
{
    public class ChatRoomFunction : IChatRoomFunction
    {
        WnpTalkContext _wnpTalkContext;
        IUserFunction _userFunction;
        public ChatRoomFunction(WnpTalkContext chatAppContext, IUserFunction userFunction)
        {
            _wnpTalkContext = chatAppContext;
            _userFunction = userFunction;
        }
        public async Task<IEnumerable<User.User>> GetListChatRoom(int userId)
        {
            var entities = await _wnpTalkContext.TblChatRooms
                .Where(x => x.UserId == userId)
                .ToListAsync();

            var result = entities.Select(x => _userFunction.GetUserById(x.RoomId));

            if (result == null) result = new List<User.User>();

            return result;
        }
    }
}
