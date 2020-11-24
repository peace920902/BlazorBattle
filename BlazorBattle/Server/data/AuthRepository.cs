using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorBattle.Shared;
using Microsoft.EntityFrameworkCore;

namespace BlazorBattle.Server.data
{
    public class AuthRepository:IAuthRepository
    {
        private readonly DataContext _dataContext;

        public AuthRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<ServiceResponse<int>> Register(User user, string password)
        {
            if (await UserExist(user.Email))
            {
                return new ServiceResponse<int>
                {
                    Success = false,
                    Message = "User already exists"
                };
            }

            CreatePasswordHash(password, out var passwordHash, out var passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            await _dataContext.Users.AddAsync(user);
            await _dataContext.SaveChangesAsync();
            return new ServiceResponse<int>()
            {
                Data = user.Id,
                Message = "Registration successful!"
            };
        }

        public async Task<ServiceResponse<string>> Login(string email, string password)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> UserExist(string email)
        {
            return await _dataContext.Users.AnyAsync(x => x.Email.Equals(email, StringComparison.CurrentCultureIgnoreCase));
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new System.Security.Cryptography.HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }
    }
}