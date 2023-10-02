using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IAuthService
    {
        Task<bool> ValidateUserAsync(SignInDto signInDto);
        string CreateToken();
        Task SignUpAsync(SignUpDto signUpDto);
    }
}
