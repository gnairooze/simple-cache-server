using Serilog;
using System.Net;
using System.Net.Sockets;
using System.Numerics;

namespace Talk
{
    public class ServerListener
    {
        public int Port { get; set; }
        public IPAddress AllowedIPAddressess { get; set; } = IPAddress.Any;

        public event EventHandler<MessageEventArgs>? CommandReceived;

        private readonly ILogger _logger;

        public ServerListener(ILogger logger)
        {
            _logger = logger;
        }

        public void Start()
        {
            try
            {
                using var listener = new TcpListener(AllowedIPAddressess, Port);
                listener.Start();

                _logger.Information($"Server is listening on {AllowedIPAddressess}:{Port}");

                while (true)
                {
                    var client = listener.AcceptTcpClient();
                    var clientEndPoint = client.Client.RemoteEndPoint;

                    _logger.Information($"Client connected from {clientEndPoint}");

                    var clientHandler = new ClientHandler(_logger);
                    clientHandler.MessageReceived += (sender, e) =>
                    {
                        CommandReceived?.Invoke(this, new MessageEventArgs(e.Message));
                    };

                    _ = clientHandler.HandleAsync(client);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "error");
                throw;
            }
        }
    }
}
