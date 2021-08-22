using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using StarAhmet.CouchbaseDb;
using StarAhmet.DbModels;
using StarAhmet.Helpers;
using StarAhmet.RequestModels;
using StarAhmet.RequestModels.UserModels;
using StarAhmet.ResponseModels;


namespace StarAhmet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserTokenRepository _userTokenRepository;

        public UserController(IUserRepository userRepository, IUserTokenRepository userTokenRepository)
        {
            _userRepository = userRepository;
            _userTokenRepository = userTokenRepository;
        }
        [HttpPost("Register")]
        public ResponseBaseModel<UserRegisterResult> Register(RegisterUser registerUserData)
        {
            try
            {
                var t = _userRepository.Add(new Users()
                {
                    Email = registerUserData.Email,
                    Password = registerUserData.Password.ToMd5(),
                    RegisterIpAddress = HttpContext.Request.Host.Value,
                    RegisterDate = DateTime.Now
                });
                string token = Guid.NewGuid().ToString() + "-" + new Random().Next(1000, 9999);
                _userTokenRepository.Add(new UserTokens()
                {
                    IsExit = false,
                    CreateDate = DateTime.Now,
                    ExpireDate = DateTime.Now.AddMonths(1),
                    UserId = t.Id,
                    Token = token
                });

                return new ResponseBaseModel<UserRegisterResult>()
                {
                    ErrorCode = null,
                    Data = new UserRegisterResult() { Token = token }
                };

            }
            catch (Exception)
            {

                return new ResponseBaseModel<UserRegisterResult>()
                {
                    ErrorCode = "Kayıt Başarısız",
                    Data = new UserRegisterResult() { Token = null }
                };
            }

        }

        [HttpPost("Login")]
        public ResponseBaseModel<UserLoginResult> Login(LoginModel loginModel)
        {
            var result = _userRepository.UserLogin(loginModel.Email, loginModel.Password);
            if (result != null)
            {
                return new ResponseBaseModel<UserLoginResult>()
                {
                    ErrorCode = null,
                    Data = new UserLoginResult(){Token = result}
                };
            }
            else
            {
                return new ResponseBaseModel<UserLoginResult>()
                {
                    ErrorCode = "Giriş Başarısız",
                    Data = new UserLoginResult() { Token = null }
                };
            }

        }
    }
}
