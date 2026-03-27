namespace TaskHub.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using TaskHub.Application.Users.Register;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register(
        [FromServices] RegisterUserHandler handler,
        [FromBody] RegisterUserCommand command,
        CancellationToken ct)
    {
        var userId = await handler.Handle(command, ct);

        return Ok(new { userId });
    }
}