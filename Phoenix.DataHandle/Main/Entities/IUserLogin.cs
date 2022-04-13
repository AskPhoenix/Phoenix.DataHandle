namespace Phoenix.DataHandle.Main.Entities
{
    public interface IUserLogin
    {
        IUser User { get; }
        IOneTimeCode VerificationOneTimeCode { get; }
    }
}
