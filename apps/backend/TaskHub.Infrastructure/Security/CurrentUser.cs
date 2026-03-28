namespace TaskHub.Infrastructure.Security;

using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using TaskHub.Application.Abstractions;

public class CurrentUser : ICurrentUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid UserId =>
        Guid.Parse(_httpContextAccessor.HttpContext!
            .User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
}