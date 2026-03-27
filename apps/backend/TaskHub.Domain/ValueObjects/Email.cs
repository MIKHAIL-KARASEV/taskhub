namespace TaskHub.Domain.ValueObjects;

using System.Text.RegularExpressions;
using TaskHub.Domain.Common.Exceptions;

public sealed class Email : IEquatable<Email>
{
    private static readonly Regex EmailRegex =
        new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);

    private Email(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Email Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("Email is required");
        }

        value = value.Trim().ToLowerInvariant();

        if (value.Length > 256)
        {
            throw new DomainException("Email is too long");
        }

        if (!EmailRegex.IsMatch(value))
        {
            throw new DomainException("Invalid email format");
        }

        return new Email(value);
    }

    public override bool Equals(object? obj)
        => obj is Email email && Value == email.Value;

    public bool Equals(Email? other)
        => other is not null && Value == other.Value;

    public override int GetHashCode()
        => Value.GetHashCode();

    public static implicit operator string(Email email)
        => email.Value;

    public static bool operator ==(Email? left, Email? right)
        => Equals(left, right);

    public static bool operator !=(Email? left, Email? right)
        => !Equals(left, right);
}