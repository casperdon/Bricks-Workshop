namespace NotificationLibrary.Workshop.Sms.Models;

public class TelephoneNumber
{
    public string Number { get; }

    public TelephoneNumber(string number)
    {
        if (string.IsNullOrWhiteSpace(number))
            throw new ArgumentException("Telephone number cannot be null or empty.", nameof(number));

        // Additional formatting or validation can be added here if needed.

        Number = number;
    }

    public bool IsValid()
    {
        // Basic validation: digits only, length between 7 and 15
        var digitsOnly = System.Text.RegularExpressions.Regex.Replace(Number, @"\D", "");
        return digitsOnly.Length >= 7 && digitsOnly.Length <= 15;
    }

    public override string ToString() => Number;
}
