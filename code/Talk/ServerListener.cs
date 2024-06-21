using Serilog;
using System.Net;
using System.Net.Sockets;
using System.Numerics;

namespace Talk
{
    public class ServerListener(ILogger logger)
    {
        public int Port { get; set; }
        public IPAddress AllowedIPAddressess { get; set; } = IPAddress.Any;
        public List<ClientMetaData> ConnectedClients { get; } = [];

        public event EventHandler<MessageEventArgs>? CommandReceived;

        private readonly ILogger _logger = logger;

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
                    ClientMetaData clientMetadata = new() { 
                        Endpoint = clientEndPoint!.ToString()!
                    };

                    ConnectedClients.Add(clientMetadata);

                    var clientHandler = new ClientHandler(_logger);
                    clientHandler.MessageReceived += (sender, e) =>
                    {
                        CommandReceived?.Invoke(this, new MessageEventArgs(e.Message));
                    };
                    
                    clientHandler.ClientChanged += HandleClientChanged;

                    _ = clientHandler.HandleAsync(client, clientMetadata);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "error");
                throw;
            }
        }

        private void HandleClientChanged(object? sender, ClientEventArgs e)
        {
            switch (e.Status)
            {
                case Enums.ClientStatus.Connected:
                    break;
                case Enums.ClientStatus.Disconnected:
                    ConnectedClients.RemoveAll(c => c.Endpoint == e.ClientEndpoint);
                    break;
            }
        }
    }
}
