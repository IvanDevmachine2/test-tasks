using HMT_UserService.Models;

namespace HMT_UserService.Interfaces
{
    public interface ILoggingRepository
    {
        Task SendLogToServiceAsync(LogEntry logEntry);
    }
}
