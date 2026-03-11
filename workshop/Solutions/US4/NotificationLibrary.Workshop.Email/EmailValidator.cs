using NotificationLibrary.Workshop.Email.Exceptions;

namespace NotificationLibrary.Workshop.Email;

internal static class EmailValidator
{
    public static void AssertValid(this Models.Email email)
    {
        if (!email.IsValid())
        {
            throw new InvalidEmailException(email);
        }
    }
}
