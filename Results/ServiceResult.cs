namespace Thebook.Results
{
    public class ServiceResult<T>
    {
        public bool Success { get; set; }
        public string Error { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
