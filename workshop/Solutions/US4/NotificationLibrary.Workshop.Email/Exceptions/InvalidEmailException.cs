namespace NotificationLibrary.Workshop.Email.Exceptions;

public class InvalidEmailException : Exception
{
    public InvalidEmailException(Models.Email email)
        : base(email.Address)
    {
    }

    public InvalidEmailException(string email)
        : base($"The email address '{email}' is not valid.")
    {
    }
}
