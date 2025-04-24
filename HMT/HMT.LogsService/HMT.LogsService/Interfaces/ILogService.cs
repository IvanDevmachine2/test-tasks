using HMT.LogsService.DTOs;
using HMT.LogsService.Models;

namespace HMT.LogsService.Interfaces
{
    public interface ILogService
    {
        Task<string?> AcceptLogAsync(LogAcceptDto log);
        Task<List<LogFrontDto>> GetAllLogsAsync();
        Task<List<LogFrontDto>> GetLogsByTypeAsync(int level);
    }
}
