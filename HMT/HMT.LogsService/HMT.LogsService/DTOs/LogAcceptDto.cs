namespace HMT.LogsService.DTOs
{
    public class LogAcceptDto
    {
        public int LogLevel { get; set; }
        public string? Message { get; set; }
        public string? Exception { get; set; }
    }
}
