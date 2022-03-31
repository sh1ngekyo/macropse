namespace Macropse.Infrastructure.Module.Message
{
    public enum ResultCode
    {
        Success = 0,
        ParseCommandError = 1,
        IOError = 2,
        GlobalParseError = 3,
        ArgumentParseError = 4,
    }

    public interface IMessage
    {
        ResultCode ResultCode { get; }
        string Message { get; }
    }
}
