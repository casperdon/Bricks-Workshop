namespace NotificationLibrary.Workshop.Sms.Exceptions;

public class InvalidTelephoneNumberException : Exception
{
    public InvalidTelephoneNumberException(Models.TelephoneNumber telephoneNumber)
        : base(telephoneNumber.Number)
    {
    }

    public InvalidTelephoneNumberException(string telephoneNumber)
        : base($"The telephone number '{telephoneNumber}' is not valid.")
    {
    }
}
