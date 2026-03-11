namespace NotificationLibrary.Workshop.Email.Models;

public class Email
{
    public string Address { get; }

    public Email(string address)
    {
        if (string.IsNullOrWhiteSpace(address))
            throw new ArgumentException("Email address cannot be null or empty.", nameof(address));

        // Additional formatting or validation can be added here if needed.

        Address = address;
    }

    public bool IsValid()
    {
        // Basic validation: check for presence of '@' and a domain part.
        var atIndex = Address.IndexOf('@');
        if (atIndex <= 0 || atIndex == Address.Length - 1)
            return false;
        var domainPart = Address[(atIndex + 1)..];
        return domainPart.Contains('.');
    }

    public override string ToString() => Address;
}
