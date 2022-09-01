using Microsoft.AspNetCore.Mvc;

using Stuxy.Identity.Abstractions.Commands.Accounts;
using Stuxy.Identity.Contracts.v1._0.Requests.Accounts;

namespace Stuxy.Identity.API.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AccountsController : AbstractController
    {
        public AccountsController(IServiceProvider provider)
            : base(provider)
        {

        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAccount([FromBody] RegisterAccountRequest request)
        {
            await _bus.SendAsync(new RegisterAccountCommand(request));

            var response = GetResponse(false);

            if (response.Success)
                return CreatedAtAction(nameof(RegisterAccount), null);

            return BadRequest(response);
        }
    }
}
