using HMT.Foundation.DTOs;
using HMT_UserService.Data;
using HMT_UserService.Interfaces;
using HMT_UserService.Models.DbModels;
using HMT.Foundation.Validators.Users;
using System.Text;
using Microsoft.EntityFrameworkCore;
using HMT_UserService.Models;

namespace HMT_UserService.Repositories
{
    public class UserRepository(AppDbContext context, UserCRUDValidator validator, ILoggingRepository logginInterface) : IUserRepository
    {
        private readonly AppDbContext _context = context;
        private readonly UserCRUDValidator _validator = validator;
        private readonly ILoggingRepository _logginInterface = logginInterface;

        public async Task<string?> AddAsync(UsersDataDto user)
        {
            var validationResponse = _validator.VAddEditUser(user);

            StringBuilder result = new();

            if (validationResponse.First().ResponseType == HMT.Foundation.Enums.VResponseTypes.Valid)
            {
                try
                {
                    var newUserId = Guid.NewGuid();

                    await _context.Users.AddAsync(new Users()
                    {
                        Id = newUserId,
                        Name = user.Name,
                        Email = user.Email,
                        Password = user.Password
                    });

                    await _context.SaveChangesAsync();

                    result.Append($"Создан пользователь c Id {newUserId}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                    await _logginInterface.SendLogToServiceAsync(new LogEntry()
                    {
                        Exception = ex.InnerException.Message.ToString(),
                        LogLevel = 2
                    });
                }
            }
            else
            {
                foreach (var error in validationResponse)
                {
                    await _logginInterface.SendLogToServiceAsync(new LogEntry()
                    {
                        LogLevel = 1,
                        Message = error.Info
                    });

                    result.Append($"{error.ResponseType} - {error.Info}");
                }
            }

            return result.ToString();
        }

        public async Task<string?> DeleteAsync(Guid id)
        {
            var wantedUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            try
            {
                _context.Users.Remove(wantedUser);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return $"Пользователь {wantedUser.Name} был успешно удалён!";
        }

        public async Task<List<UsersFrontDto>?> GetAllAsync()
        {
            try
            {
                var foundUsersData =
                    await
                    _context.Users
                    .Select(u => new UsersFrontDto()
                    {
                        Id = u.Id,
                        Email = u.Email,
                        Name = u.Name
                    }).ToListAsync();

                return foundUsersData;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }

        public async Task<UsersFrontDto?> GetByIdAsync(Guid id)
        {
            var wantedUserData =
                await _context.Users
                .FirstOrDefaultAsync(u => u.Id == id);

            try
            {
                if (wantedUserData is not null)
                    return new UsersFrontDto()
                    {
                        Id = wantedUserData.Id,
                        Name = wantedUserData.Name,
                        Email = wantedUserData.Email
                    };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }

        public async Task<string?> GetPassByIdAsync(Guid id)
        {
            try
            {
                var wantedUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

                if (wantedUser is not null)
                    return wantedUser.Password;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }

        public async Task<string?> UpdateAsync(UsersDataDto user, Guid id)
        {
            var wantedUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (user is null)
            {
                Console.WriteLine("no user input data");
                
                return "no user input data";
            }
            else
            {
                if (wantedUser is not null)
                {
                    StringBuilder changesDescriptionBuilder = new();

                    var changesCount = 0;

                    if (user.Name is not null)
                    {
                        wantedUser.Name = user.Name;

                        changesDescriptionBuilder.Append($"новое имя - {user.Name}, ");

                        changesCount++;
                    }

                    if (user.Password is not null)
                    {
                        wantedUser.Password = user.Password;

                        changesDescriptionBuilder.Append($"новый пароль - {user.Password}, ");

                        changesCount++;
                    }

                    if (user.Email is not null)
                    {
                        wantedUser.Email = user.Email;

                        changesDescriptionBuilder.Append($"новая эл. почта - {user.Email}, ");

                        changesCount++;
                    }

                    if (changesCount > 0)
                    {
                        await _context.SaveChangesAsync();

                        var changeDescription = changesDescriptionBuilder.ToString()[..^2];

                        return $"Пользователь {wantedUser.Name} обновил полей: {changesCount}, а именно: {changeDescription}";
                    }
                    else
                        return "no new data";
                }
                else
                {
                    Console.WriteLine("no user input data");
                    return null;
                }
            }
        }
    }
}
