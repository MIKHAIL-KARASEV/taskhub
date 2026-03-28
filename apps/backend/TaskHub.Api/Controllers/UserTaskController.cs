using Microsoft.AspNetCore.Mvc;
using TaskHub.Application.UserTasks.Create;
using TaskHub.Application.UserTasks.Complete;
using TaskHub.Application.UserTasks.Uncomplete;
using TaskHub.Application.UserTasks.Update;
using TaskHub.Application.UserTasks.Delete;
using TaskHub.Application.UserTasks.Queries.GetMyTasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace TaskHub.Api.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
[Route("api/usertasks")]
public class UserTaskController : ControllerBase
{
    private readonly CreateUserTaskHandler _createHandler;

    public UserTaskController(CreateUserTaskHandler createHandler)
    {
        _createHandler = createHandler;
    }

    [HttpGet]
    public async Task<IActionResult> GetMyTasks(
        [FromServices] GetMyTasksHandler handler,
        CancellationToken ct)
    {
        var tasks = await handler.Handle(ct);
        return Ok(tasks);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateUserTaskCommand command,
        CancellationToken ct)
    {
        var taskId = await _createHandler.Handle(command, ct);

        return Ok(new { taskId });
    }

    [HttpPost("{id:guid}/complete")]
    public async Task<IActionResult> Complete(
        Guid id,
        [FromServices] CompleteUserTaskHandler handler,
        CancellationToken ct)
    {
        await handler.Handle(new CompleteUserTaskCommand(id), ct);

        return NoContent();
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(
        Guid id,
        [FromBody] UpdateUserTaskCommand request,
        [FromServices] UpdateUserTaskHandler handler,
        CancellationToken ct)
    {
        var command = request with { TaskId = id };

        await handler.Handle(command, ct);

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(
        Guid id,
        [FromServices] DeleteUserTaskHandler handler,
        CancellationToken ct)
    {
        await handler.Handle(new DeleteUserTaskCommand(id), ct);

        return NoContent();
    }

    [HttpPost("{id:guid}/uncomplete")]
    public async Task<IActionResult> Uncomplete(
        Guid id,
        [FromServices] UncompleteUserTaskHandler handler,
        CancellationToken ct)
    {
        await handler.Handle(new UncompleteUserTaskCommand(id), ct);

        return NoContent();
    }
}