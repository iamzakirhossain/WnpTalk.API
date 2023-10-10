using Microsoft.EntityFrameworkCore;
using WnpTalk.API.Data;
using WnpTalk.API.Entities;
using WnpTalk.API.Functions.Message;
using WnpTalk.API.Functions.User;

namespace WnpTalk.API.Functions.ChatRoomMessage
{
    public class ChatRoomMessageFunction : IChatRoomMessageFunction
    {
        WnpTalkContext _wnpTalkContext;
        IUserFunction _userFunction;
        public ChatRoomMessageFunction(WnpTalkContext chatAppContext, IUserFunction userFunction)
        {
            _wnpTalkContext = chatAppContext;
            _userFunction = userFunction;
        }

        public async Task<int> AddChatRoomMessage(int fromUserId, int toRoomId, string message)
        {
            var entity = new TblChatRoomMessage
            {
                FromUserId = fromUserId,
                ToRoomId = toRoomId,
                Content = message,
                SendDateTime = DateTime.Now,
                IsRead = false
            };

            _wnpTalkContext.TblChatRoomMessages.Add(entity);
            var result = await _wnpTalkContext.SaveChangesAsync();

            return result;
        }

        public async Task<IEnumerable<LastestChatRoomMessage>> GetLastestChatRoomMessage(int userId)
        {
            var result = new List<LastestChatRoomMessage>();

            var userRooms = await _wnpTalkContext.TblChatRooms
                .Where(x => x.UserId == userId).ToListAsync();

            foreach (var userRoom in userRooms)
            {
                //var lastMessage = await _wnpTalkContext.TblChatRoomMessages
                //    .Where(x => (x.FromUserId == userId && x.ToRoomId == userRoom.RoomId)
                //             || (x.FromUserId == userRoom.RoomId && x.ToRoomId == userId))
                //    .OrderByDescending(x => x.SendDateTime)
                //    .FirstOrDefaultAsync();

                var lastMessage = await _wnpTalkContext.TblChatRoomMessages
                    .OrderByDescending(x => x.SendDateTime)
                    .FirstOrDefaultAsync();

                if (lastMessage != null)
                {
                    result.Add(new LastestChatRoomMessage
                    {
                        UserId = userId,
                        Content = lastMessage.Content,
                        UserRoomInfo = _userFunction.GetUserById(userRoom.RoomId),
                        Id = lastMessage.Id,
                        IsRead = lastMessage.IsRead,
                        SendDateTime = lastMessage.SendDateTime
                    });
                }
            }
            return result;
        }

        public async Task<IEnumerable<ChatRoomMessage>> GetChatRoomMessages(int fromUserId, int toRoomId)
        {
            //var entities = await _wnpTalkContext.TblChatRoomMessages
            //    .Where(x => (x.FromUserId == fromUserId && x.ToRoomId == toRoomId)
            //        || (x.FromUserId == toRoomId && x.ToRoomId == fromUserId))
            //    .OrderBy(x => x.SendDateTime)
            //    .ToListAsync();

            var entities = await _wnpTalkContext.TblChatRoomMessages
                .OrderBy(x => x.SendDateTime)
                .ToListAsync();

            return entities.Select(x => new ChatRoomMessage
            {
                Id = x.Id,
                Content = x.Content,
                FromUserId = x.FromUserId,
                ToRoomId = x.ToRoomId,
                SendDateTime = x.SendDateTime,
                IsRead = x.IsRead,
            });
        }

        public async Task<IEnumerable<ChatRoomMessage>> GetChatRoomMessagesAll()
        {
            //var entities = await _wnpTalkContext.TblChatRoomMessages
            //    .Where(x => (x.FromUserId == fromUserId && x.ToRoomId == toRoomId)
            //        || (x.FromUserId == toRoomId && x.ToRoomId == fromUserId))
            //    .OrderBy(x => x.SendDateTime)
            //    .ToListAsync();

            var entities = await _wnpTalkContext.TblChatRoomMessages
                .OrderBy(x => x.SendDateTime)
                .ToListAsync();

            return entities.Select(x => new ChatRoomMessage
            {
                Id = x.Id,
                Content = x.Content,
                FromUserId = x.FromUserId,
                ToRoomId = x.ToRoomId,
                SendDateTime = x.SendDateTime,
                IsRead = x.IsRead,
            });
        }
    }
}
