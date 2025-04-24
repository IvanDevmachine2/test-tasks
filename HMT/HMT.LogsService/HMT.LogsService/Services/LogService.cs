using HMT.LogsService.Data;
using HMT.LogsService.DTOs;
using HMT.LogsService.Enums;
using HMT.LogsService.Interfaces;
using HMT.LogsService.Models;
using Microsoft.EntityFrameworkCore;

namespace HMT.LogsService.Services
{
    public class LogService(LogsDbContext context) : ILogService
    {
        private readonly LogsDbContext _context = context;

        public async Task<string?> AcceptLogAsync(LogAcceptDto log)
        {
            try
            {
                Guid logId = Guid.NewGuid();

                var logLevel = (LogLevels)log.LogLevel;

                var logEntry = new LogEntry()
                {
                    Id = logId,
                    LogLevel = logLevel.ToString(),
                    Message = log.Message,
                    Exception = log.Exception,
                    Timestamp = DateTime.UtcNow
                };

                await _context.LogEntries.AddAsync(logEntry);

                await _context.SaveChangesAsync();

                return $"Лог с id {logId} успешно создан";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }

        public async Task<List<LogFrontDto>> GetAllLogsAsync()
        {
            return
                await
                _context.LogEntries
                .Select(l => new LogFrontDto()
                {
                    LogLevel = l.LogLevel,
                    Exception = l.Exception,
                    Message = l.Message,
                    Timestamp = l.Timestamp
                })
                .ToListAsync();
        }

        public async Task<List<LogFrontDto>> GetLogsByTypeAsync(int level)
        {
            var logLevel = ((LogLevels)level).ToString();

            return
                await
                _context.LogEntries
                .Where(l => l.LogLevel == logLevel)
                .Select(l => new LogFrontDto()
                {
                    LogLevel = l.LogLevel,
                    Exception = l.Exception,
                    Message = l.Message,
                    Timestamp = l.Timestamp
                })
                .ToListAsync();
        }

        //можно ещё подобавлять разного рода фильтрацию логов
    }
}
