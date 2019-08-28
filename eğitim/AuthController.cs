using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Tabim.Core.Bll;
using Tabim.Core.Bll.Admin;
using Tabim.Core.Bll.Authentication;
using Tabim.Core.Common.Enum;
using Tabim.Core.Data.Model.Core;
using Tabim.Core.Data.Model.Ortak;
using Tabim.Core.Service.Helpers;
using Tabim.Core.Service.Model;

namespace Tabim.Core.Service.Controllers
{
    public class tokenn
    {
        public string token { get; set; }
    }
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class AuthController : BaseController
    {
        private readonly IConfiguration configuration = null;
        private readonly IUsers users = null;
        private readonly IApplicationAccounts application_accounts = null;
        private readonly IDomainIslemleri domainIslemleri = null;

        public AuthController(IConfiguration configuration, IUsers users, IApplicationAccounts _application_accounts, IDomainIslemleri domainIslemleri)
        {
            this.configuration = configuration;
            this.users = users;
            this.application_accounts = _application_accounts;
            this.domainIslemleri = domainIslemleri;
        }

        [HttpPost("loginUser")]
        public ActionResult LoginUser([FromBody]UserForLoginDto login)
        {
            var user = users.LoginUser(login.ApplicationType, login.UserName, login.Password);

            if (user == null)
            {
                return Unauthorized("Kullanıcı adı ya da şifre hatalı.");
            }

            if (user.Domains.Any())
            {
                bool isDomainApplication = domainIslemleri.IsDomainApplication(RowStatus.Active, user.Domains.First().Id, login.ApplicationType);
                if (!isDomainApplication)
                {
                    return NotFound("Uygulama kullanıcısı değilsiniz.");
                }
            }

            Domain domain = null;
            if (user.Domains.Count == 1)
            {
                domain = user.Domains.FirstOrDefault();
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration.GetSection("Settings:TokenSecurityKey").Value);

            var tokenDescriptor = new SecurityTokenDescriptor
            {

                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName),

                    new Claim("UserName",user.Name)
                }),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)

            };

            if (domain != null)
            {
                tokenDescriptor.Subject.AddClaim(new Claim("Domain", domain.Name));
                tokenDescriptor.Subject.AddClaim(new Claim("DomainIdentifier", domain.Id.ToString()));

                if (domain.Kurum != null)
                {
                    tokenDescriptor.Subject.AddClaim(new Claim("Kurum", (domain != null && domain.Kurum != null) ? domain.Kurum.Ad : ""));
                    tokenDescriptor.Subject.AddClaim(new Claim("KurumId", (domain != null && domain.Kurum != null) ? domain.Kurum.Id.ToString() : null));
                }
            }
            /*  if (User != null) { 

                  tokenDescriptor.Subject.AddClaim(User.FindFirst("AppId"));
                  tokenDescriptor.Subject.AddClaim(User.FindFirst("ConstanName"));
              }*/


            //UserRole tablosundaki rollerini ekle
            foreach (var item in user.Roles)
            {
                tokenDescriptor.Subject.AddClaim(new Claim(ClaimTypes.Role, item.Role));
            }
            //kursiyer, mesken sahibi, kiracı, oturan gibi hakları ekle
            //domainde olmasının yeterli olduğu yerler için policy yazılacak


            var token = tokenHandler.CreateToken(tokenDescriptor);


            var tokenString = tokenHandler.WriteToken(token);

            return Ok(tokenString);
        }

        [HttpPost("changeDomain")]
        [Authorize(Roles = "System")] //buraya profesyonel yönetici falan da eklenebilecek
        public ActionResult ChangeDomain(string domainName)
        {  //bu kullanıcının yetkili olduğu domainlerden birisi ise değişikliği yap
            User user = users.ChangeDomain(JwtUser.UserId, domainName);
            if (user == null)
            {
                return Unauthorized();
            }

            Domain domain = null;
            if (user.Domains.Count == 1)
            {
                domain = user.Domains.FirstOrDefault();
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration.GetSection("Settings:TokenSecurityKey").Value);

            var tokenDescriptor = new SecurityTokenDescriptor
            {

                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName),

                    new Claim("UserName",user.Name)
                }),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)

            };

            if (domain != null)
            {
                tokenDescriptor.Subject.AddClaim(new Claim("Domain", domain.Name));
                tokenDescriptor.Subject.AddClaim(new Claim("DomainIdentifier", domain.Id.ToString()));

                if (domain.Kurum != null)
                {
                    tokenDescriptor.Subject.AddClaim(new Claim("Kurum", (domain != null && domain.Kurum != null) ? domain.Kurum.Ad : ""));
                    tokenDescriptor.Subject.AddClaim(new Claim("KurumId", (domain != null && domain.Kurum != null) ? domain.Kurum.Id.ToString() : null));
                }
            }
            /*  if (User != null) { 

                  tokenDescriptor.Subject.AddClaim(User.FindFirst("AppId"));
                  tokenDescriptor.Subject.AddClaim(User.FindFirst("ConstanName"));
              }*/


            //UserRole tablosundaki rollerini ekle
            foreach (var item in user.Roles)
            {
                tokenDescriptor.Subject.AddClaim(new Claim(ClaimTypes.Role, item.Role));
            }
            //kursiyer, mesken sahibi, kiracı, oturan gibi hakları ekle
            //domainde olmasının yeterli olduğu yerler için policy yazılacak


            var token = tokenHandler.CreateToken(tokenDescriptor);


            var tokenString = tokenHandler.WriteToken(token);

            return Ok(tokenString);
        }

        [HttpPost("loginApp")]
        public ActionResult LoginApp([FromBody]AppForLoginDto login)
        {
            var app = application_accounts.LoginApplication(login.AppId, login.AppKey);

            if (app == null)
            {
                return Unauthorized();
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration.GetSection("Settings:TokenSecurityKey").Value);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("AppId", app.AppId),
                    new Claim("ConstanName", app.ConstantName),
                    new Claim("Domain",app.Domain.Name),
                    new Claim("DomainIdentifier",app.DomainId.ToString()),
                    new Claim("DefaultUserId",app.DefaultUserId.ToString()),
                    new Claim("SubeId",app.SubeId.HasValue ? app.SubeId.Value.ToString():string.Empty),
                    new Claim("KurumId",app.Domain.KurumId.ToString())
                }),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var tokenString = tokenHandler.WriteToken(token);

            return Ok(tokenString );
        }



        [HttpPost("setPass")]
        public ActionResult SetPass(string userName)
        {
            users.SetPass(userName);

            return Ok();
        }

        [HttpPost("login/application")]
        public ActionResult Login([FromBody]ApplicationAccountLoginModel login)

        {
            var application_account = application_accounts.LoginApplication(login.AppId, login.AppKey);

            if (application_account == null)
            {
                return NotFound("Could not verify appid and appkey");
            }

            var token = Jwt.JwtHelper.BuildToken(
                configuration["Settings:TokenSecurityKey"],
                configuration["Settings:TokenIssuer"],
                configuration["Settings:TokenAudience"]
            );

            var jwt_packet = new JwtPacket
            {
                Token = token,
                Username = String.Empty,
                AppId = application_account.AppId

            };

            return Ok(jwt_packet);
        }
        [HttpPost("register")]
        public ActionResult<View.Model.Core.UserView> Register(View.Model.Core.UserView userView)
        {
            User user = new User();
            user.UserName = userView.UserName;
            user.RowStatus = Common.Enum.RowStatus.Active;
            user.Email = userView.EmailConfirmation;
            user.EmailConfirmed = userView.EmailConfirmed;
            user.PhoneNumber = userView.PhoneNumber;
            user.PhoneNumberConfirmed = userView.PhoneNumberConfirmed;
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration.GetSection("Settings:TokenSecurityKey").Value);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };

            users.RegisterUser(user, userView.Sifre);

            return Ok(userView);
        }
        [HttpPost("changePassword")]
        [Authorize]
        public ActionResult<View.Model.Core.NewPasswordView> changePassword([FromBody]View.Model.Core.NewPasswordView newPasswordView)
        {
            if (newPasswordView == null)
            {
                return BadRequest("Eksik Veri Gönderdiniz.");
            }
            if (newPasswordView.confirmPassword != newPasswordView.password)
            {
                return Unauthorized("Girdiğiniz Şifreler Aynı Değil.");
            }
            var username = JwtUser.Username;
            var userexists = users.UserExists(username);
            var match = users.PasswordMatch(username, newPasswordView.oldPass);
            if (!userexists)
            {
                return Unauthorized("Böyle Bir Kullanıcı Yok.");
            }
            if (!match)
            {
                return Unauthorized("Eski Şifreniz Yanlış.");
            }
            users.ChangePassword(username, newPasswordView.password);
            return Ok("Şifreniz Güncellendi.");
        }
        [HttpPost("registerUser")]
        public ActionResult<View.Model.Core.UserView> RegisterUser(string userView)
        {
          
            //STRİNG KULLANMADAN GETİR NORMALDE GELİYOR
            View.Model.Core.UserView userViewModel = JsonConvert.DeserializeObject<View.Model.Core.UserView>(userView);
            User user = new User();
            if (userViewModel.UserType == false)
            {
                //user.UserName = userViewModel.Name;
                user.Name = "";//required olduğu için boş basılır
                userViewModel.UserName = userViewModel.Email;
                user.UserName = userViewModel.Email;
            }
            else
            {
                user.Name = userViewModel.Name;
                userViewModel.UserName = userViewModel.Name;
                user.UserName = userViewModel.Name;
            }
            if (userViewModel.ConfirmationType == false)
            {
                //telefon
            }
            else {
                //eposta
            }
            user.ConfirmedType = (userViewModel.ConfirmationType == true) ? ConfirmationType.Eposta : ConfirmationType.Telefon;
            user.RowStatus = Common.Enum.RowStatus.Active;
            user.Email = userViewModel.EmailConfirmation;
            user.EmailConfirmed = userViewModel.EmailConfirmed;
            user.PhoneNumber = userViewModel.PhoneNumber;
            user.PhoneNumberConfirmed = userViewModel.PhoneNumberConfirmed;
            user.OperationDate = DateTime.Now;
            user.Id = userViewModel.Id;
            userViewModel.Id = users.RegisterUser(user, userViewModel.Sifre).Id;
            var domainUserModel = users.getDomainUser(userViewModel.DomainId, userViewModel.Id);
            if (domainUserModel == null)
            {
                DomainUser domainUser = new DomainUser();
                domainUser.DomainId = userViewModel.DomainId;
                domainUser.OperationDate = DateTime.Now;
                domainUser.UserId = userViewModel.Id;
                domainUser.RowStatus = Common.Enum.RowStatus.Active;
                domainUser.Id = (domainUserModel == null) ? 0 : domainUserModel.Id;
                users.postDomainUser(domainUser);
            }
            return Ok(userViewModel);
        }

        [HttpPost("login")]
        public ActionResult Login(string loginUser)
        {

            Tabim.Core.View.Model.Core.LoginView login = JsonConvert.DeserializeObject<View.Model.Core.LoginView>(loginUser);
            //var user = users.LoginUser(login.KullaniciAdi, login.Sifre);
            User user = null;

            if (user == null)
            {
                return Unauthorized();
            }

            Domain domain = null;
            Kurum kurum = null;
            if (user.Domains.Count == 1)
            {
                domain = user.Domains.FirstOrDefault();
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration.GetSection("Settings:TokenSecurityKey").Value);

            var tokenDescriptor = new SecurityTokenDescriptor
            {

                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim("Domain",domain.Name),
                    new Claim("UserName",user.Name),
                    new Claim("Kurum",(domain!=null && domain.Kurum!=null)?domain.Kurum.Ad:""),
                    new Claim("KurumId",(domain!=null && domain.Kurum!=null)?domain.Kurum.Id.ToString():string.Empty),
                    new Claim("DomainIdentifier",domain.Id.ToString())
                }),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)

            };

            /*  if (User != null) { 

                  tokenDescriptor.Subject.AddClaim(User.FindFirst("AppId"));
                  tokenDescriptor.Subject.AddClaim(User.FindFirst("ConstanName"));
              }*/


            //UserRole tablosundaki rollerini ekle
            foreach (var item in user.Roles)
            {
                tokenDescriptor.Subject.AddClaim(new Claim(ClaimTypes.Role, item.Role));
            }
            //kursiyer, mesken sahibi, kiracı, oturan gibi hakları ekle
            //domainde olmasının yeterli olduğu yerler için policy yazılacak


            var token = tokenHandler.CreateToken(tokenDescriptor);


            var tokenString = tokenHandler.WriteToken(token);

            return Ok(user);
        }

        [HttpGet("getUser")]
        public User GetUser(string userName)
        {
            return users.GetUserByUserName(userName);
        }

        [HttpGet("getUserByToken")]
        public User getUserByToken(string token)
        {
            return users.getUserByToken(token);
        }
        [HttpPost("postUserClaims")]
        public ActionResult<UserClaims> postUserClaims(string userClaims)
        {
            UserClaims model = JsonConvert.DeserializeObject<UserClaims>(userClaims);
            users.postUserClaims(model);
            return Ok(userClaims);
        }
        [HttpGet("GetUserEmailComfirm")]
        public ActionResult<User> getUserEmailComfirm(int userId)
        {

            return users.UserEmailComfirm(userId);

        }
        [HttpGet("PhoneConfirm")]
        public ActionResult<User> PhoneConfirm(int userId)
        {

            return users.PhoneConfirm(userId);

        }
        [HttpGet("GetUserInfo")]
        public ActionResult<View.Model.Core.UserInfoView> GetUserInfo()
        {
            var userId = JwtUser.UserId;
            var userinfo = users.UserById(userId);
            View.Model.Core.UserInfoView info = new View.Model.Core.UserInfoView();
            info.username = userinfo.UserName;
            info.name = userinfo.Name;
            info.email = userinfo.Email;
            info.cep = userinfo.PhoneNumber;
            info.tcKimlik = userinfo.NationalNumber;
            return info;
        }

    }
}
