namespace SocketsIoSample.Interfaces
{
    public interface ISocketIOClient
    {
        void Init(string address, string ns = "/");

        void SendMessage(string message);
    }
}