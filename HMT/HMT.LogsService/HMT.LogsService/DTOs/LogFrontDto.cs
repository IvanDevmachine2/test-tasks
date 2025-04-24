namespace HMT.LogsService.DTOs
{
    public class LogFrontDto
    {
        public DateTime Timestamp { get; set; }
        public string LogLevel { get; set; }
        public string Message { get; set; }
        public string? Exception { get; set; }
    }
}
