using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WnpTalk.API.Controllers.ListChat;
using WnpTalk.API.Functions.ChatRoom;
using WnpTalk.API.Functions.ChatRoomMessage;
using WnpTalk.API.Functions.Message;
using WnpTalk.API.Functions.User;
using WnpTalk.API.Functions.UserFriend;

namespace WnpTalk.API.Controllers.ListRoomChat
{
    [Route("[controller]")]
    [ApiController]
    public class ListRoomChatController : Controller
    {
        IUserFunction _userFunction;
        IChatRoomFunction _chatRoomFunction;
        IChatRoomMessageFunction _chatRoomMessageFunction;

        public ListRoomChatController(IUserFunction userFunction,
            IChatRoomFunction chatRoomFunction,
            IChatRoomMessageFunction chatRoomMessageFunction)
        {
            _userFunction = userFunction;
            _chatRoomFunction = chatRoomFunction;
            _chatRoomMessageFunction = chatRoomMessageFunction;
        }

        [HttpPost("Initialize")]
        public async Task<ActionResult> Initialize([FromBody] int userId)
        {
            var response = new ListRoomChatInitializeResponse
            {
                User = _userFunction.GetUserById(userId),
                UserRooms = await _chatRoomFunction.GetListChatRoom(userId),
                LastestChatRoomMessages = await _chatRoomMessageFunction.GetLastestChatRoomMessage(userId)
            };

            return Ok(response);
        }
    }
}
