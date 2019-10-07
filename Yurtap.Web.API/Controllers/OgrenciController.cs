using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Yurtap.Business.Abstract;
using Yurtap.Core.Models.User;
using Yurtap.Core.Web.Mvc;
using Yurtap.Entity;
using Yurtap.Entity.Models;

namespace Yurtap.Web.API.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OgrenciController : BaseControllor
    {
        private IOgrenciBll _ogrenciBll;
        private IKisiBll _kisiBll;

        public OgrenciController(IOgrenciBll ogrenciBll, IKisiBll kisiBll)
        {
            _ogrenciBll = ogrenciBll;
            _kisiBll = kisiBll;
        }

        [HttpGet("ad")]
        public string ad()
        {
            return "serkan çelik";
        }

        [HttpGet("GetOgrenciListesi")]
        public ActionResult GetOgrenciListesi()
        {
            var ogrenciListesi = _ogrenciBll.GetOgrenciListesi();
            if (ogrenciListesi.Success)
            {
                return Ok(ogrenciListesi);
            }
            return NotFound(ogrenciListesi);
        }

        [HttpGet("GetOgrenciByKisiId")]
        public ActionResult GetOgrenciById(int kisiId)
        {
            var ogrenci = _ogrenciBll.GetOgrenciByKisiId(kisiId);
            if (ogrenci.Success)
            {
                return Ok(ogrenci);
            }
            return NotFound(ogrenci);
        }

        [HttpPost("AddOgrenci")]
        public ActionResult AddOgrenci([FromBody]OgrenciModel ogrenciModel)
        {
            var ogrenci = _ogrenciBll.AddOgrenci(ogrenciModel);
            if (ogrenci.Success)
            {
                return Created("", ogrenci);
            }
            return BadRequest(ogrenci);
        }

        [HttpPut("UpdateOgrenci")]
        public ActionResult UpdateOgrenci([FromBody]OgrenciModel ogrenciModel)
        {
            var ogrenci = _ogrenciBll.UpdateOgrenci(ogrenciModel);
            if (ogrenci.Success)
            {
                return Ok(ogrenci);
            }
            return BadRequest(ogrenci);
        }

        [HttpDelete("DeleteOgrenci")]
        public ActionResult Delete([FromBody]OgrenciModel ogrenciModel)
        {
            var ogrenci = _ogrenciBll.DeleteOgrenci(ogrenciModel);
            if (ogrenci.Success)
            {
                return Ok(ogrenci);
            }
            return BadRequest(ogrenci);
        }
    }
}
