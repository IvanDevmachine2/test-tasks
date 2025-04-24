using HMT.Foundation.DTOs;

namespace HMT_UserService.Interfaces
{
    public interface IUserRepository
    {
        Task<List<UsersFrontDto>?> GetAllAsync();
        Task<UsersFrontDto?> GetByIdAsync(Guid id);
        Task<string?> GetPassByIdAsync(Guid id);
        Task<string?> AddAsync(UsersDataDto user);
        Task <string?> UpdateAsync(UsersDataDto user, Guid id);
        Task<string?> DeleteAsync(Guid id);
    }
}
