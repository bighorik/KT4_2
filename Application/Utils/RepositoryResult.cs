namespace Application.Utils
{
    public class RepositoryResult<T>
    {
        public T? Data { get; set; }
        public bool Ok { get; set; }
        public string? ErrorCode { get; set; }
        public object? Details { get; set; }

        public RepositoryResult() { }

        public RepositoryResult(T data)
        {
            Data = data;
            Ok = true;
        }

        public RepositoryResult(string errorCode)
        {
            ErrorCode = errorCode;
            Ok = false;
        }

        public RepositoryResult(string errorCode, object details)
        {
            ErrorCode = errorCode;
            Ok = false;
            Details = details;
        }

        public static bool operator ==(RepositoryResult<T>? left, string? right)
            => left != null && left.ErrorCode == right;

        public static bool operator !=(RepositoryResult<T>? left, string? right)
            => left != null && left.ErrorCode != right;

        public static bool operator ==(string left, RepositoryResult<T>? right)
            => right != null && left == right.ErrorCode;

        public static bool operator !=(string left, RepositoryResult<T>? right)
            => right != null && left != right.ErrorCode;

        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public static class RepositoryResult
    {
        public static string NotFound => nameof(NotFound);
    }
}
