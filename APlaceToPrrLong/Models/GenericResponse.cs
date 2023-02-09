namespace APlaceToPrrLong.Models
{
    public class GenericResponse<T>
    {
        public T? Data { get; set; }
        public string Message { get; set; }
        public int Status { get; set; }
        public string? Error { get; set; }
    }
}
