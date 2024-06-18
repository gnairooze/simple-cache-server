// See https://aka.ms/new-console-template for more information

using System.Net.Sockets;
using System.Text;

var client = new TcpClient();
try
{
    // Connect to the server
    //ask for the server's IP address and port number
    Console.WriteLine("enter the server's IP address: (default 127.0.0.1)");
    string? ipAddress = Console.ReadLine();

    if (string.IsNullOrEmpty(ipAddress))
    {
        ipAddress = "127.0.0.1";
        Console.WriteLine("no ip address provided. 127.0.0.1 will be used.");
        
    }

    Console.WriteLine("enter the server's port number: (default 12345)");
    int port = 0;
    var portParsingSucceeded = int.TryParse(Console.ReadLine(), out port);

    if (!portParsingSucceeded)
    {
        port = 12345;
        Console.WriteLine("no valid port number provided. 12345 will be used.");
    }

    await client.ConnectAsync(ipAddress, port);
    Console.WriteLine($"connected to the server {ipAddress}:{port}.");

    // Get the stream to write/read messages
    NetworkStream stream = client.GetStream();

    // Send a message to the server
    bool exit = false;
    while (!exit)
    {
        //ask for the message to send
        Console.WriteLine("enter the message to send:");
        string? messageToSend = Console.ReadLine();

        while (string.IsNullOrEmpty(messageToSend))
        {
            Console.WriteLine("enter the message to send:");
            messageToSend = Console.ReadLine();
        }

        byte[] dataToSend = Encoding.ASCII.GetBytes(messageToSend);
        await stream.WriteAsync(dataToSend, 0, dataToSend.Length);
        Console.WriteLine($"sent: {messageToSend}");

        // Read the server's response (optional)
        byte[] buffer = new byte[1024];
        int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
        string response = Encoding.ASCII.GetString(buffer, 0, bytesRead);
        Console.WriteLine($"received: {response}");

        //ask if the user wants to send another message
        Console.WriteLine("do you want to send another message? (Y/n)");
        string? sendAnotherMessage = Console.ReadLine();

        if (sendAnotherMessage?.ToLower() == "n")
        {
            exit = true;
            break;
        }
    }

    // Close everything
    stream.Close();
}
catch (Exception e)
{
    Console.WriteLine("Exception: {0}", e.Message);
}
finally
{
    // Close the connection
    client.Close();
}
