namespace HMT.LogsService.Models
{
    public class LogEntry
    {
        public Guid Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string LogLevel { get; set; }
        public string Message { get; set; }
        public string? Exception { get; set; }
    }
}
