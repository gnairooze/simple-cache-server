using Microsoft.Extensions.Caching.Memory;
using ProtocolHandlerInterfaces;
using Serilog.Core;
using System.Runtime.CompilerServices;

namespace CacheService.Protocol
{
    public class CacheProtocolHandler(Logger logger) : IProtocolHandler
    {
        private readonly Logger _Logger = logger;
        private readonly MemoryCache _CacheManager = new (new MemoryCacheOptions());

        public string Handle(string sender, string message)
        {
            _Logger.Debug($"Handling message from {sender}: {message}");
            _Logger.Debug("Processing ...");

            var command = ExtractCommand(message);

            switch (command)
            {
                case Enums.Commands.Get:
                    return CommandsHandlers.GetCommandHandler.Handle(_Logger, _CacheManager, sender, message);
                case Enums.Commands.Set:
                    return CommandsHandlers.SetCommandHandler.Handle(_Logger, sender, message);
                case Enums.Commands.Delete:
                    return "Delete command handled";
                case Enums.Commands.List:
                    return "List command handled";
                case Enums.Commands.Info:
                    return "Info command handled";
                case Enums.Commands.Clients:
                    return "Clients command handled";
            }

            throw new InvalidOperationException("unknown cache command");
        }

        private Enums.Commands ExtractCommand(string message)
        {
            throw new NotImplementedException();
        }
    }
}
