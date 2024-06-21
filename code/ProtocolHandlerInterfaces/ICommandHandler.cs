using Serilog.Core;

namespace ProtocolHandlerInterfaces
{
    public interface ICommandHandler
    {
        string Handle(string sender, string message);
    }
}
