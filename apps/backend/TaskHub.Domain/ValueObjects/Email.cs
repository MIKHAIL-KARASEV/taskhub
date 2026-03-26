namespace TaskHub.Domain.ValueObjects;

using System.Text.RegularExpressions;

public sealed class Email : IEquatable<Email>
{
    private static readonly Regex EmailRegex =
        new (@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);

    public string Value { get; }

    private Email(string value)
    {
        Value = value;
    }

    public static Email Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Email is required");
        }

        value = value.Trim().ToLowerInvariant();

        if (!EmailRegex.IsMatch(value))
        {
            throw new ArgumentException("Invalid email");
        }

        return new Email(value);
    }

    public override bool Equals(object? obj)
        => obj is Email email && Value == email.Value;

    public bool Equals(Email? other)
    {
        if (other is null)
        {
            return false;
        }

        return Value == other.Value;
    }

    public override int GetHashCode()
        => Value.GetHashCode();

    public static bool operator ==(Email? left, Email? right)
        => Equals(left, right);

    public static bool operator !=(Email? left, Email? right)
        => !Equals(left, right);
}