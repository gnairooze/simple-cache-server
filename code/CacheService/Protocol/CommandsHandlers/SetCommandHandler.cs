using Serilog.Core;

namespace CacheService.Protocol.CommandsHandlers
{
    internal class SetCommandHandler
    {
        internal string Handle(Logger logger, string sender, string message)
        {
            return "Set command handled";
        }
    }
}
