namespace WeatherApp.Services.Shared
{
    public class Result
    {
        public bool IsSuccess { get; }
        public Error? Error { get; }

        protected Result(bool isSuccess, Error? error = null)
        {
            IsSuccess = isSuccess;
            Error = error;
        }

        public static Result Success() => new Result(true);
        public static Result Failure(Error error) => new Result(false, error);

        public static implicit operator Result(Error error) => Failure(error);
    }

    public class Result<T> : Result 
    { 
        public T? Value { get; }

        private Result(T value) : base(true) => Value = value;

        private Result(Error error) : base(false, error) { }

        public static implicit operator Result<T>(T value) => new Result<T>(value);
        public static implicit operator Result<T>(Error error) => new Result<T>(error);
    }
}
