namespace Phoenix.DataHandle.Sms
{
    public interface ISmsService
    {
        void Send(string destination, string body);
    }
}
