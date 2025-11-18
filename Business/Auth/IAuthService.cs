using Entity;
using Business.DTOs;

namespace Business.Auth
{
    public interface IAuthService
    {
        Task<string?> LoginAsync(string username, string password);
        Task<bool> RegisterStaffAsync(StaffRegisterDto dto);
    }

}
