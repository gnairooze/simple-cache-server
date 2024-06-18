using Serilog;
using System.Net.Sockets;
using System.Text;

namespace Talk
{
    internal class ClientHandler(ILogger logger)
    {
        private readonly ILogger _logger = logger;
        internal event EventHandler<MessageEventArgs>? MessageReceived;

        public async Task HandleAsync(TcpClient client)
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
                    byte[] response = Encoding.UTF8.GetBytes("Server received your message!");
                    await stream.WriteAsync(response);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "error while handling client");
            }
            finally
            {
                client.Close();
                _logger.Information("Client disconnected");
            }
        }

    }
}