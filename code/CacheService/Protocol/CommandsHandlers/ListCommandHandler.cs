using Microsoft.Extensions.Caching.Memory;
using ProtocolHandlerInterfaces;
using Serilog.Core;

namespace CacheService.Protocol.CommandsHandlers
{
    internal class ListCommandHandler(Logger logger, IMemoryCache cache): ICommandHandler
    {
        public Logger HandlerLogger { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        internal static string Handle(Logger logger, MemoryCache cacheManager, string sender, string message)
        {
            return "List command handled";
        }

        public string Handle(string sender, string message)
        {
            throw new NotImplementedException();
        }
    }
}
