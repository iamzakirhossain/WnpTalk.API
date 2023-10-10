using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WnpTalk.API.Functions.Message;
using WnpTalk.API.Functions.User;

namespace WnpTalk.API.Controllers.Message
{
    [Route("[controller]")]
    [ApiController]
    public class MessageController : Controller
    {
        IMessageFunction _messageFunction;
        IUserFunction _userFunction;

        public MessageController(IMessageFunction messageFunction, IUserFunction userFunction)
        {
            _messageFunction = messageFunction;
            _userFunction = userFunction;
        }

        [HttpPost("Initialize")]
        public async Task<ActionResult> Initialize([FromBody] MessageInitalizeRequest request)
        {
            var response = new MessageInitalizeResponse
            {
                FriendInfo = _userFunction.GetUserById(request.ToUserId),
                Messages = await _messageFunction.GetMessages(request.FromUserId, request.ToUserId)
            };

            return Ok(response);
        }
    }
}
