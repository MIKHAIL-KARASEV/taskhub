namespace TaskHub.Domain.ValueObjects;

using System.Text.RegularExpressions;

public class Email : IEquatable<Email>
{
    public string Value { get; }

    public Email(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Email is required");
        }

        value = value.Trim().ToLowerInvariant();

        if (!Regex.IsMatch(value, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
        {
            throw new ArgumentException("Invalid email");
        }

        Value = value;
    }

    public override bool Equals(object? obj)
        => obj is Email email && Value == email.Value;

    public bool Equals(Email? other)
        => other?.Value == Value;

    public override int GetHashCode()
        => Value.GetHashCode();
}