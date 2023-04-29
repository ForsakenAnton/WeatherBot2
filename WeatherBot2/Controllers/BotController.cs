using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;
using WeatherBot2.Services;

namespace WeatherBot2.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BotController : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> Post(
            [FromBody] Update update,
            [FromServices] UpdateHandlersService updateHandlersService,
            CancellationToken cancellationToken)
        {
            await updateHandlersService.HandleUpdateAsync(
                update,
                cancellationToken);

            return Ok();
        }
    }
}
