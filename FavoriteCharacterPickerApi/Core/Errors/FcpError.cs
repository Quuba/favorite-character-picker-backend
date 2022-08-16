namespace FavoriteCharacterPickerApi.Core.Errors;

public enum FcpErrorType
{
    UserNotFound,
    UsernameIsTaken,
    UserAlreadyExists,
    BadCredentials,
    Unknown
}

public class FcpError : Exception
{
    //TODO: read about custom exceptions
    public FcpErrorType ErrorType { get; private set; }

    public FcpError(FcpErrorType errorType)
    {
        ErrorType = errorType;
    }
}