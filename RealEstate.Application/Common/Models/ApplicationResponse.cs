namespace RealEstate.Application.Common.Models
{
    public class ApplicationResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
        public List<string> Errors { get; set; } = new();

        public static ApplicationResponse<T> SuccessResponse(T data, string message = "Operation completed successfully")
        {
            return new ApplicationResponse<T>
            {
                Success = true,
                Message = message,
                Data = data
            };
        }

        public static ApplicationResponse<T> FailureResponse(string message, List<string>? errors = null)
        {
            return new ApplicationResponse<T>
            {
                Success = false,
                Message = message,
                Errors = errors ?? new List<string>()
            };
        }

        public static ApplicationResponse<T> FailureResponse(List<string> errors, string message = "Operation failed")
        {
            return new ApplicationResponse<T>
            {
                Success = false,
                Message = message,
                Errors = errors
            };
        }
    }
}
