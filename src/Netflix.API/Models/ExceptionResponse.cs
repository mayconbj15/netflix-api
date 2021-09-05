namespace Netflix.API.Models
{
    public class ExceptionResponse
    {
        public string Message { get; set; }

        public string InnerExceptionMessage { get; set; }
    }
}