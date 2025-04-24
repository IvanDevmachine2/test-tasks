namespace HMT_UserService.Models
{
    public class LogEntry
    {
        public int LogLevel { get; set; }
        public string Message { get; set; }
        public string? Exception { get; set; }
    }
}
