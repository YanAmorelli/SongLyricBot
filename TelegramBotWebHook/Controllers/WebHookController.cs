using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TelegramBotWebHook.Services;
using Telegram.Bot.Types;

namespace TelegramBotWebHook.Controllers
{
    public class WebhookController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromServices] HandleUpdateService handleUpdateService,
                                              [FromBody] Update update)
        {
            await handleUpdateService.ReplyLyricAsync(update);
            return Ok();
        }
    }
}