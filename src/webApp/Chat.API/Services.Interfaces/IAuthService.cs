using Shared.DataTransferObjects;

namespace Services.Interfaces
{
    public interface IAuthService
    {
        Task<bool> ValidateUserAsync(SignInDto signInDto);
        string CreateToken();
        Task SignUpAsync(SignUpDto signUpDto);
    }
}
