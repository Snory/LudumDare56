public class Result
{
    public bool IsSuccess { get; private set; }
    public string Message { get; private set; }

    public Result(bool isSuccess, string message)
    {
        IsSuccess = isSuccess;
        Message = message;
    }
}