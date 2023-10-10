using Microsoft.EntityFrameworkCore;
using WnpTalk.API.Data;
using WnpTalk.API.Entities;
using WnpTalk.API.Functions.User;

namespace WnpTalk.API.Functions.Message
{
    public class MessageFunction : IMessageFunction
    {
        WnpTalkContext _wnpTalkContext;
        IUserFunction _userFunction;
        public MessageFunction(WnpTalkContext chatAppContext, IUserFunction userFunction)
        {
            _wnpTalkContext = chatAppContext;
            _userFunction = userFunction;
        }

        public async Task<int> AddMessage(int fromUserId, int toUserId, string message)
        {
            var entity = new TblMessage
            {
                FromUserId = fromUserId,
                ToUserId = toUserId,
                Content = message,
                SendDateTime = DateTime.Now,
                IsRead = false
            };

            _wnpTalkContext.TblMessages.Add(entity);
            var result = await _wnpTalkContext.SaveChangesAsync();

            return result;
        }

        public async Task<IEnumerable<LastestMessage>> GetLastestMessage(int userId)
        {
            var result = new List<LastestMessage>();

            var userFriends = await _wnpTalkContext.TblUserFriends
                .Where(x => x.UserId == userId).ToListAsync();

            foreach (var userFriend in userFriends)
            {
                var lastMessage = await _wnpTalkContext.TblMessages
                    .Where(x => (x.FromUserId == userId && x.ToUserId == userFriend.FriendId)
                             || (x.FromUserId == userFriend.FriendId && x.ToUserId == userId))
                    .OrderByDescending(x => x.SendDateTime)
                    .FirstOrDefaultAsync();

                if (lastMessage != null)
                {
                    result.Add(new LastestMessage
                    {
                        UserId = userId,
                        Content = lastMessage.Content,
                        UserFriendInfo = _userFunction.GetUserById(userFriend.FriendId),
                        Id = lastMessage.Id,
                        IsRead = lastMessage.IsRead,
                        SendDateTime = lastMessage.SendDateTime
                    });
                }
            }
            return result;
        }

        public async Task<IEnumerable<Message>> GetMessages(int fromUserId, int toUserId)
        {
            var entities = await _wnpTalkContext.TblMessages
                .Where(x => (x.FromUserId == fromUserId && x.ToUserId == toUserId)
                    || (x.FromUserId == toUserId && x.ToUserId == fromUserId))
                .OrderBy(x => x.SendDateTime)
                .ToListAsync();

            return entities.Select(x => new Message
            {
                Id = x.Id,
                Content = x.Content,
                FromUserId = x.FromUserId,
                ToUserId = x.ToUserId,
                SendDateTime = x.SendDateTime,
                IsRead = x.IsRead,
            });
        }
    }
}
