// See https://aka.ms/new-console-template for more information

using System;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Serilog;
using Talk;

//create instance of ServerListener and pass a serilog instance that logs to console to it
Serilog.ILogger logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

var serverListener = new ServerListener(logger) { 
    Port = 12345,
    AllowedIPAddressess = System.Net.IPAddress.Any
};

serverListener.CommandReceived += (sender, e) =>
{
    logger.Information($"Program | Command received: {e.Message}");
};

serverListener.Start();

Console.WriteLine("Press any key to exit");
Console.Read();