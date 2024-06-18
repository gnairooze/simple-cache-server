namespace Talk
{
    public class MessageEventArgs(string message) : EventArgs
    {
        public string Message { get; set; } = message;
    }
}