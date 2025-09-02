namespace RealEstate.SharedKernel.Result
{
    public class Result
    {
        public bool IsSuccess { get; protected set; }
        public bool IsFailure => !IsSuccess;
        public string Error { get; protected set; } = string.Empty;
        public List<string> Errors { get; protected set; } = new();
        public string Message { get; set; } = string.Empty;

        protected Result(bool isSuccess, string error)
        {
            IsSuccess = isSuccess;
            Error = error;
        }

        protected Result(bool isSuccess, List<string> errors)
        {
            IsSuccess = isSuccess;
            Errors = errors;
            Error = string.Join("; ", errors);
        }

        public static Result Success() => new(true, string.Empty);
        public static Result Failure(string error) => new(false, error);
        public static Result Failure(List<string> errors) => new(false, errors);
    }

    public class Result<T> : Result
    {
        public T? Value { get; protected set; }

        protected Result(T? value, bool isSuccess, string error) : base(isSuccess, error)
        {
            Value = value;
        }

        protected Result(T? value, bool isSuccess, List<string> errors) : base(isSuccess, errors)
        {
            Value = value;
        }

        public static Result<T> Success(T value, string message = "Operation completed successfully") => new(value, true, string.Empty);
        public static Result<T> Failure(string error, string message = "Operation failed")
            => new(default, false, error);

        public static Result<T> Failure(List<string> errors, string message = "Operation failed")
            => new(default, false, errors);

        public static implicit operator Result<T>(T value) => Success(value);
    }
}
