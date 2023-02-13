namespace APlaceToPrrLong.Models
{
    public class GenericResponse<T>
    {
        public T? Data { get; set; }
        public string Message { get; set; }
        public int Status { get; set; }
        public string? Error { get; set; }
        public GenericResponse(T? data, string message, int status, string? error)
        {
            Data = data;
            Message = message;
            Status = status;
            Error = error;
        }

        public GenericResponse(T? data, string message, int status)
        {
            Data = data;
            Message = message;
            Status = status;
        }
    }
}
