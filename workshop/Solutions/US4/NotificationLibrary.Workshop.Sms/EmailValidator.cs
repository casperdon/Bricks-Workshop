using NotificationLibrary.Workshop.Sms.Exceptions;

namespace NotificationLibrary.Workshop.Sms;

internal static class TelephoneNumberValidator
{
    public static void AssertValid(this Models.TelephoneNumber telephoneNumber)
    {
        if (!telephoneNumber.IsValid())
        {
            throw new InvalidTelephoneNumberException(telephoneNumber);
        }
    }
}
