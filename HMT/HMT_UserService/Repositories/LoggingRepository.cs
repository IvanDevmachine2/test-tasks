using HMT_UserService.Models;
using HMT_UserService.Interfaces;
using System.Text;
using System.Text.Json;

public class LoggingService : ILoggingRepository
{
    private readonly HttpClient _httpClient;

    public LoggingService(HttpClient httpClient) => _httpClient = httpClient;

    public async Task SendLogToServiceAsync(LogEntry logEntry)
    {
        var logJson = JsonSerializer.Serialize(logEntry);
        var content = new StringContent(logJson, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("/api/Logs/entry", content);

        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            throw new Exception($"Ошибка при отправке лога в сервис: {response.StatusCode}. Ошибка: {errorContent}");
        }
    }
}