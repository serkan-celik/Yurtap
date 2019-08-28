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
    [Authorize]
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
            List<YoklamaBaslikModel> yoklamaBaslik;
            try
            {
                yoklamaBaslik = _yoklamaBasliklBll.GetYoklamaBaslikListesi();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(yoklamaBaslik);
        }

        [HttpPost("AddYoklamaBaslik")]
        public ActionResult AddYoklamaBaslik([FromBody]YoklamaBaslikEntity yoklamaBaslikEntity)
        {
            YoklamaBaslikEntity yoklamaBaslik;         
            try
            {
                yoklamaBaslik = _yoklamaBasliklBll.AddYoklamaBaslik(yoklamaBaslikEntity);   
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(yoklamaBaslik);
        }

        [HttpPut("UpdateYoklamaBaslik")]
        public ActionResult UpdateYoklamaBaslik([FromBody]YoklamaBaslikEntity yoklamaBaslikEntity)
        {
            YoklamaBaslikEntity yoklamaBaslik;
            try
            {
                yoklamaBaslik = _yoklamaBasliklBll.UpdateYoklamaBaslik(yoklamaBaslikEntity);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(yoklamaBaslik);
        }

        [HttpDelete("DeleteYoklamaBaslik")]
        public ActionResult DeleteYoklamaBaslik([FromBody]YoklamaBaslikEntity yoklamaBaslikEntity)
        {
            bool yoklamaBaslik;
            try
            {
                yoklamaBaslik = _yoklamaBasliklBll.DeleteYoklamaBaslik(yoklamaBaslikEntity);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(yoklamaBaslik);
        }
    }
}
