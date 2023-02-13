namespace APlaceToPrrLong.Models
{
    public class GenericListResponse<T>
    {
        public List<T>? Data { get; set; }
        public string Message { get; set; }
        public int Status { get; set; }
        public string? Error { get; set; }

        public GenericListResponse(List<T>? data, string message, int status)
        {
            Data = data;
            Message = message;
            Status = status;
        }
        public GenericListResponse(List<T>? data, string message, int status, string? error)
        {
            Data = data;
            Message = message;
            Status = status;
            Error = error;
        }
    }
}
