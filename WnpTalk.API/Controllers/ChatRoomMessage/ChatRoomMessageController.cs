using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WnpTalk.API.Controllers.Message;
using WnpTalk.API.Functions.ChatRoomMessage;
using WnpTalk.API.Functions.Message;
using WnpTalk.API.Functions.User;

namespace WnpTalk.API.Controllers.ChatRoomMessage
{
    [Route("[controller]")]
    [ApiController]
    public class ChatRoomMessageController : Controller
    {
        IChatRoomMessageFunction _chatRoomMessageFunction;
        IUserFunction _userFunction;

        public ChatRoomMessageController(IChatRoomMessageFunction chatRoomMessageFunction, IUserFunction userFunction)
        {
            _chatRoomMessageFunction = chatRoomMessageFunction;
            _userFunction = userFunction;
        }

        [HttpPost("Initialize")]
        public async Task<ActionResult> Initialize([FromBody] ChatRoomMessageInitalizeRequest request)
        {
            var response = new ChatRoomMessageInitalizeResponse
            {
                RoomInfo = _userFunction.GetUserById(request.ToRoomId),
                ChatRoomMessages = await _chatRoomMessageFunction.GetChatRoomMessages(request.FromUserId, request.ToRoomId)
            };

            return Ok(response);
        }

        [HttpGet("GetAllMessages")]
        public async Task<ActionResult> GetAllMessages()
        {
            var response = new ChatRoomMessageInitalizeResponse
            {

                ChatRoomMessages = await _chatRoomMessageFunction.GetChatRoomMessagesAll()
            };

            return Ok(response);
        }
    }
}
