using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Yurtap.Business.Abstract;
using Yurtap.Business.Concrete;
using Yurtap.Entity;
using Yurtap.Entity.Models;

namespace Yurtap.Web.API.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class YoklamaBaslikController : ControllerBase
    {
        private readonly IYoklamaBaslikBll _yoklamaBasliklBll;

        public YoklamaBaslikController(IYoklamaBaslikBll yoklamaBasliklBll)
        {
            _yoklamaBasliklBll = yoklamaBasliklBll;
        }

        [HttpGet("GetYoklamaBaslikListesi")]
        public ActionResult GetYoklamaBaslikListesi()
        {
            var yoklamaBaslik = _yoklamaBasliklBll.GetYoklamaBaslikListesi();
            if (yoklamaBaslik.Success)
            {
                return Ok(yoklamaBaslik);
            }
            return NotFound(yoklamaBaslik);
        }

        [HttpPost("AddYoklamaBaslik")]
        public ActionResult AddYoklamaBaslik([FromBody]YoklamaBaslikEntity yoklamaBaslikEntity)
        {
            var yoklamaBaslik = _yoklamaBasliklBll.AddYoklamaBaslik(yoklamaBaslikEntity);
            if (yoklamaBaslik.Success)
            {
                return Created("", yoklamaBaslik);
            }
            return BadRequest(yoklamaBaslik);
        }

        [HttpPut("UpdateYoklamaBaslik")]
        public ActionResult UpdateYoklamaBaslik([FromBody]YoklamaBaslikEntity yoklamaBaslikEntity)
        {
            var yoklamaBaslik = _yoklamaBasliklBll.UpdateYoklamaBaslik(yoklamaBaslikEntity);
            if (yoklamaBaslik.Success)
            {
                return Ok(yoklamaBaslik);
            }
            return BadRequest(yoklamaBaslik);
        }

        [HttpDelete("DeleteYoklamaBaslik")]
        public ActionResult DeleteYoklamaBaslik([FromBody]YoklamaBaslikEntity yoklamaBaslikEntity)
        {
           var yoklamaBaslik = _yoklamaBasliklBll.DeleteYoklamaBaslik(yoklamaBaslikEntity);
            if (yoklamaBaslik.Success)
            {
                return Ok(yoklamaBaslik);
            }
            return BadRequest(yoklamaBaslik);
        }
    }
}
