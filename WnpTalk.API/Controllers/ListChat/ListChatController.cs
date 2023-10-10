using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WnpTalk.API.Functions.Message;
using WnpTalk.API.Functions.User;
using WnpTalk.API.Functions.UserFriend;

namespace WnpTalk.API.Controllers.ListChat
{
    [Route("[controller]")]
    [ApiController]
    public class ListChatController : Controller
    {
        IUserFunction _userFunction;
        IUserFriendFunction _userFriendFunction;
        IMessageFunction _messageFunction;

        public ListChatController(IUserFunction userFunction, 
            IUserFriendFunction userFriendFunction, 
            IMessageFunction messageFunction)
        {
            _userFunction = userFunction;
            _userFriendFunction = userFriendFunction;
            _messageFunction = messageFunction;
        }

        [HttpPost("Initialize")]
        public async Task<ActionResult> Initialize([FromBody] int userId)
        {
            var response = new ListChatInitializeResponse
            {
                User = _userFunction.GetUserById(userId),
                UserFriends = await _userFriendFunction.GetListUserFriend(userId),
                LastestMessages = await _messageFunction.GetLastestMessage(userId)
            };

            return Ok(response);
        }
    }
}
