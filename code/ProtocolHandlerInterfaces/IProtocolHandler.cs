namespace ProtocolHandlerInterfaces
{
    public interface IProtocolHandler
    {
        string Handle(string sender, string message);
    }
}
