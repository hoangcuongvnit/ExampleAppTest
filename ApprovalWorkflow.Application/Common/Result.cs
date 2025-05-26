namespace ApprovalWorkflow.Application.Common
{
    public class Result<T>
    {
        public T Value { get; set; }
        public bool IsSuccess { get; set; }
        public string? ErrorMessage { get; set; }
        public Result(T value, bool isSuccess, string? errorMessage = null)
        {
            Value = value;
            IsSuccess = isSuccess;
            ErrorMessage = errorMessage;
        }
        public static Result<T> Success(T value) => new Result<T>(value, true);
        public static Result<T> Failure(string errorMessage) => new Result<T>(default, false, errorMessage);
    }
}
