using Serilog;
using System.Net.Sockets;
using System.Text;

namespace Talk
{
    internal class ClientHandler(ILogger logger)
    {
        private readonly ILogger _logger = logger;
        internal event EventHandler<MessageEventArgs>? MessageReceived;
        internal event EventHandler<ClientEventArgs>? ClientChanged;

        public async Task HandleAsync(TcpClient client, ClientMetaData clientMetadata)
        {
            try
            {
                using var stream = client.GetStream();
                byte[] buffer = new byte[1024];
                int bytesRead;

                while ((bytesRead = await stream.ReadAsync(buffer)) > 0)
                {
                    string receivedData = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    string message = $"Received: {receivedData}";
                    _logger.Debug(message);

                    //raise event carrying the message data
                    MessageReceived?.Invoke(this, new MessageEventArgs(receivedData));

                    // Send a response back to the client
                    string responseMessage = "Server received your message!";
                    byte[] response = Encoding.UTF8.GetBytes(responseMessage);
                    await stream.WriteAsync(response);
                    
                    clientMetadata.Transactions.Add(new Transaction()
                    {
                        RunDate = DateTime.Now,
                        Message = receivedData,
                        Response = responseMessage
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "error while handling client");
            }
            finally
            {
                var clientEndPoint = client.Client.RemoteEndPoint;

                client.Close();
                _logger.Information("Client disconnected");
                ClientChanged?.Invoke(this, new ClientEventArgs(clientEndPoint!.ToString()!, Enums.ClientStatus.Disconnected));
            }
        }

    }
}