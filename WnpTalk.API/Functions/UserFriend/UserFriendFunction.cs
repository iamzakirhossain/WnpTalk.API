using Microsoft.EntityFrameworkCore;
using WnpTalk.API.Data;
using WnpTalk.API.Functions.User;

namespace WnpTalk.API.Functions.UserFriend
{
    public class UserFriendFunction : IUserFriendFunction
    {
        WnpTalkContext _wnpTalkContext;
        IUserFunction _userFunction;
        public UserFriendFunction(WnpTalkContext chatAppContext, IUserFunction userFunction)
        {
            _wnpTalkContext = chatAppContext;
            _userFunction = userFunction;
        }
        public async Task<IEnumerable<User.User>> GetListUserFriend(int userId)
        {
            var entities = await _wnpTalkContext.TblUserFriends
                .Where(x => x.UserId == userId)
                .ToListAsync();

            var result = entities.Select(x => _userFunction.GetUserById(x.FriendId));

            if (result == null) result = new List<User.User>();

            return result;
        }
    }
}
