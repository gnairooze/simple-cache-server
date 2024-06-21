using System.Net.Sockets;

namespace Talk
{
    public class ClientEventArgs(string clientEndPoint, Enums.ClientStatus status) : EventArgs
    {
        public Enums.ClientStatus Status { get; set; } = status;
        public string ClientEndpoint { get; set; } = clientEndPoint;
    }
}