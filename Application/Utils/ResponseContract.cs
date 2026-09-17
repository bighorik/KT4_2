namespace Application.Utils
{
    public class ResponseContract<T>
    {
        public T? Data { get; set; }
        public bool Ok { get; set; }
        public string? ErrorCode { get; set; }
        public object? Details { get; set; }

        public ResponseContract() { }

        public ResponseContract(T data)
        {
            Data = data;
            Ok = true;
        }

        public ResponseContract(string errorCode)
        {
            ErrorCode = errorCode;
            Ok = false;
        }

        public ResponseContract(string errorCode, object details)
        {
            ErrorCode = errorCode;
            Ok = false;
            Details = details;
        }
    }











    public class ResponseContract
    {
        public bool Ok { get; set; } = true;
        public string? ErrorCode { get; set; }
        public object? Details { get; set; }

        public ResponseContract() { }

        public ResponseContract(string errorCode)
        {
            ErrorCode = errorCode;
            Ok = false;
        }

        public ResponseContract(string errorCode, object details)
        {
            ErrorCode = errorCode;
            Ok = false;
            Details = details;
        }
    }
}
