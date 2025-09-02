namespace RealEstate.SharedKernel.Result
{
    public class Result
    {
        public bool IsSuccess { get; protected set; }
        public bool IsFailure => !IsSuccess;
        public string Error { get; protected set; } = string.Empty;
        public List<string> Errors { get; protected set; } = new();
        public string Message { get; protected set; } = string.Empty;

        protected Result(bool isSuccess, string error, string message = "")
        {
            IsSuccess = isSuccess;
            Error = error;
            Message = message;
        }

        protected Result(bool isSuccess, List<string> errors, string message = "")
        {
            IsSuccess = isSuccess;
            Errors = errors;
            Error = string.Join("; ", errors);
            Message = message;
        }

        public static Result Success(string message = "Operation completed successfully")
            => new(true, string.Empty, message);

        public static Result Failure(string error, string message = "Operation failed")
            => new(false, error, message);

        public static Result Failure(List<string> errors, string message = "Operation failed")
            => new(false, errors, message);
    }

    public class Result<T> : Result
    {
        public T? Value { get; protected set; }

        protected Result(T? value, bool isSuccess, string error, string message = "")
            : base(isSuccess, error, message)
        {
            Value = value;
        }

        protected Result(T? value, bool isSuccess, List<string> errors, string message = "")
            : base(isSuccess, errors, message)
        {
            Value = value;
        }

        public static Result<T> Success(T value, string message = "Operation completed successfully")
            => new(value, true, string.Empty, message);

        public static Result<T> Failure(string error, string message = "Operation failed")
            => new(default, false, error, message);

        public static Result<T> Failure(List<string> errors, string message = "Operation failed")
            => new(default, false, errors, message);

        public static implicit operator Result<T>(T value) => Success(value);
    }
}
