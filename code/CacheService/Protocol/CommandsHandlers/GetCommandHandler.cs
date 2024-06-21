using Microsoft.Extensions.Caching.Memory;
using Serilog.Core;

namespace CacheService.Protocol.CommandsHandlers
{
    internal class GetCommandHandler
    {
        internal string Handle(Logger logger, MemoryCache cacheManager, string sender, string message)
        {
            return "Get command handled";
        }
    }
}
