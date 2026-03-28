namespace TaskHub.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using TaskHub.Application.Users.Register;
using TaskHub.Application.Users.Login;

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

    [HttpPost("login")]
    public async Task<IActionResult> Login(
        [FromServices] LoginUserHandler handler,
        [FromBody] LoginUserCommand command,
        CancellationToken ct)
    {
        var token = await handler.Handle(command, ct);
        return Ok(new { token });
    }
}