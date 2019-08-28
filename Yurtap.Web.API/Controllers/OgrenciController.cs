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
            List<OgrenciModel> ogrenci;
            try
            {
                ogrenci = _ogrenciBll.GetOgrenciListesi();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(ogrenci);
        }

        [HttpGet("GetOgrenciByKisiId")]
        public ActionResult GetOgrenciById(ushort kisiId)
        {
            OgrenciModel ogrenci;
            try
            {
                ogrenci = _ogrenciBll.GetOgrenciByKisiId(kisiId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(ogrenci);
        }

        [HttpPost("AddOgrenci")]
        public ActionResult AddOgrenci([FromBody]OgrenciModel ogrenciModel)
        {
            OgrenciModel ogrenci;         
            try
            {
                ogrenci = _ogrenciBll.AddOgrenci(ogrenciModel);   
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(ogrenci);
        }

        [HttpPut("UpdateOgrenci")]
        public ActionResult UpdateOgrenci([FromBody]OgrenciModel ogrenciModel)
        {
            OgrenciModel ogrenci;
            try
            {
                ogrenci = _ogrenciBll.UpdateOgrenci(ogrenciModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(ogrenci);
        }

        [HttpDelete("DeleteOgrenci")]
        public ActionResult Delete([FromBody]OgrenciModel ogrenciModel)
        {
            bool ogrenci;
            try
            {
                ogrenci = _ogrenciBll.DeleteOgrenci(ogrenciModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(ogrenci);
        }
    }
}
