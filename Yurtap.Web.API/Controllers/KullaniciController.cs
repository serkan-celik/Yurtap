using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Yurtap.Business.Abstract;
using Yurtap.Core.Web.Mvc;
using Yurtap.Entity;
using Yurtap.Entity.Models;

namespace Yurtap.Web.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class KullaniciController : BaseControllor
    {
        private readonly IKullaniciBll _kullaniciBll;
        private readonly IKullaniciRolBll _kullaniciRolBll;
        private IConfiguration _configuration;
        IKisiBll _kisiBll;

        public KullaniciController(IKullaniciBll kullaniciBll, IConfiguration configuration, IKullaniciRolBll kullaniciRolBll, IKisiBll kisiBll)
        {
            _kullaniciBll = kullaniciBll;
            _configuration = configuration;
            _kullaniciRolBll = kullaniciRolBll;
            _kisiBll = kisiBll;
        }

        [HttpPost("Giris")]
        public IActionResult Giris([FromBody] KullaniciModel kullaniciModel)
        {
            var kullanici = _kullaniciBll.GetKullaniciBilgileri(kullaniciModel.Ad, kullaniciModel.Sifre);
            if (!kullanici.Success)
            {
                return Unauthorized();
            }

            //tokenı üretecek handler
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("TokenSettings:TokenKey").Value);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                     //tokena veri gönderme
                    new Claim("id",kullanici.Result.KisiId.ToString()),
                    new Claim("name",kullanici.Result.Ad),
                    new Claim("password",kullanici.Result.Sifre),
                    new Claim("fullName",kullanici.Result.AdSoyad),
                     new Claim("roles",JsonConvert.SerializeObject(kullanici.Result.Roller)),
                    //new Claim(ClaimTypes.Role,"Personel"),
                    //new Claim(ClaimTypes.Role,"Ogrenci")
                }),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };
            //foreach (var item in new string[] { "Personel", "Ogrenci" })
            //{
            //    tokenDescriptor.Subject.AddClaim(new Claim(ClaimTypes.Role, item));
            //}
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            kullanici.Result.Token = tokenString;

            return Ok(kullanici);
        }

        [HttpGet("GetKullaniciRolleriListesi")]
        public ActionResult GetKullaniciRolleriListesi()
        {
            List<KullaniciRolListeModel> kullaniciModel;
            try
            {
                kullaniciModel = _kullaniciRolBll.GetKullaniciRolleriListesi();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(kullaniciModel);
        }

        [HttpGet("GetKisiListesi")]
        public ActionResult GetKisiListesi()
        {
            var kisiler = _kisiBll.GetKisiListesi();
            return Ok(kisiler);
        }

        [HttpGet("GetKullaniciRolleriById")]
        public ActionResult GetKullaniciRolleriById(int kisiId)
        {
            List<KullaniciRolModel> kullaniciRolModel;
            try
            {
                kullaniciRolModel = _kullaniciRolBll.GetKullaniciRolleriById(kisiId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(kullaniciRolModel);
        }

        [HttpPost("AddKullaniciRol")]
        public ActionResult AddKullaniciRol([FromBody]KullaniciRolEntity kullaniciRolEntity)
        {
            KullaniciRolEntity kullaniciRol;
            try
            {
                kullaniciRol = _kullaniciRolBll.AddKullaniciRol(kullaniciRolEntity);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(kullaniciRol);
        }

        [HttpDelete("DeleteKullaniciRol")]
        public ActionResult DeleteKullanici([FromBody]KullaniciRolEntity kullaniciRolEntity)
        {
            bool kullaniciRol;
            try
            {
                kullaniciRol = _kullaniciRolBll.DeleteKullaniciRol(kullaniciRolEntity);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(kullaniciRol);
        }

        [HttpPut("UpdateKullaniciRol")]
        public ActionResult UpdateKullaniciRol([FromBody]KullaniciRolEntity kullaniciRolEntity)
        {
            KullaniciRolEntity kullaniciRol;
            try
            {
                kullaniciRol = _kullaniciRolBll.UpdateKullaniciRol(kullaniciRolEntity);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(kullaniciRol);
        }

        [HttpGet("get")]
        public string get()
        {
            return "sekooooooooooo";
        }

        [HttpPut("UpdateKullanici")]
        public ActionResult UpdateKullanici([FromBody]KullaniciModel kullaniciModel)
        {
            KullaniciEntity kullaniciEntity;
            try
            {
                kullaniciEntity = _kullaniciBll.UpdateKullanici(kullaniciModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(kullaniciEntity);
        }

        [HttpDelete("DeleteKullanici")]
        public ActionResult DeleteKullanici([FromBody]KullaniciRolListeModel kullaniciRolListeModel)
        {
            bool kullanici;
            try
            {
                kullanici = _kullaniciBll.DeleteKullanici(kullaniciRolListeModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(kullanici);
        }
    }
}