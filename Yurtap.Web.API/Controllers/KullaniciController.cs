﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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

        public KullaniciController(IKullaniciBll kullaniciBll, IConfiguration configuration, IKullaniciRolBll kullaniciRolBll)
        {
            _kullaniciBll = kullaniciBll;
            _configuration = configuration;
            _kullaniciRolBll = kullaniciRolBll;
        }

        [HttpPost("Giris")]
        public IActionResult Giris([FromBody] KullaniciModel kullaniciModel)
        {
            var kullanici = _kullaniciBll.GetKullaniciBilgileri(kullaniciModel.Ad, kullaniciModel.Sifre);
            if (kullanici == null)
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
                    new Claim("id",kullanici.KisiId.ToString()),
                    new Claim("name",kullanici.Ad),
                    new Claim("password",kullanici.Sifre),
                    new Claim("fullName",kullanici.AdSoyad),
                     new Claim("roles",JsonConvert.SerializeObject(kullanici.Roller)),
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

            return Ok(tokenString);
        }

        [HttpGet("GetKullaniciListesi")]
        public ActionResult GetKullaniciListesi()
        {
            List<KullaniciModel> kullaniciModel;
            try
            {
                kullaniciModel = _kullaniciBll.GetKullaniciListesi();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(kullaniciModel);
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

        [HttpPost("AddKullanici")]
        public ActionResult AddKullanici([FromBody]KullaniciModel kullaniciModel)
        {
            KullaniciEntity kullaniciEntity;
            try
            {
                kullaniciEntity = _kullaniciBll.AddKullanici(kullaniciModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(kullaniciEntity);
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
        public ActionResult DeleteKullanici([FromBody]KullaniciEntity kullaniciEntity)
        {
            bool kullanici;
            try
            {
                kullanici = _kullaniciBll.DeleteKullanici(kullaniciEntity);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(kullanici);
        }
    }
}