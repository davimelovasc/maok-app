namespace Maok.App.Utils
{
    public enum LogoutReason
    {
        ServerError,
        Unauthorized,
        SessionTimeout,
        UserLogout,
        RefreshTokenInvalid
    }
}
