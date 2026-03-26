namespace TaskHub.Application.Common.Exceptions;

public sealed class UserAlreadyExistsException : Exception
{
    public UserAlreadyExistsException()
        : base("User already exists")
    {
    }
}