using IdentityService.Context.ContextTable;
using IdentityService.Helper;
using IdentityService.Models;
using IdentityService.Result;
using IdentityService.SeedWork;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityService.Services
{
    public class AdminAuthService : IAdminAuthService
    {
        private IJwtService _tokenHelper;
        private IRepository _repository;
        public AdminAuthService(IJwtService tokenHelper, IRepository repository)
        {
            _tokenHelper = tokenHelper;
            _repository = repository;
        }
        public async Task<IDataResult<AccessToken>> Register(UserForRegisterDto userForRegisterDto)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(userForRegisterDto.Password, out passwordHash, out passwordSalt);
            var user = new AdminUser
            {
                Email = userForRegisterDto.Email,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };

            _repository.AdminUser.Add(user);
            await _repository.SaveEntitiesAsync();

            var result = (await CreateAccessToken(user)).Data;

            return new SuccessDataResult<AccessToken>(result, "Kayıt oldu");
        }
        public async Task<IDataResult<AccessToken>> Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = _repository.AdminUser.AsQueryable().FirstOrDefault(x=>x.Email==userForLoginDto.Email);

            if (userToCheck == null)
            {
                return new ErrorDataResult<AccessToken>("Kullanıcı bulunamadı");
            }

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                return new ErrorDataResult<AccessToken>("Parola hatası");
            }

            var result = (await CreateAccessToken(userToCheck)).Data;

            return new SuccessDataResult<AccessToken>(result, "Başarılı giriş");
        }

        private async Task<IResult> UserExists(string email)
        {
            var userToCheck = _repository.AdminUser.AsQueryable().FirstOrDefault(x => x.Email == email);
            if (userToCheck != null)
            {
                return new ErrorResult("Kullanıcı mevcut");
            }
            return new SuccessResult();
        }

        private async Task<IDataResult<AccessToken>> CreateAccessToken(AdminUser user)
        {
            var accessToken = _tokenHelper.CreateToken(user);

            return new SuccessDataResult<AccessToken>(accessToken, "Token oluşturuldu");
        }


    }
}
