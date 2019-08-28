using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Yurtap.Business.Abstract;
using Yurtap.Entity;
using Yurtap.Entity.Models;

namespace Yurtap.Web.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PersonelController : ControllerBase
    {
        private IPersonelBll _personelBll;
        private IKisiBll _kisiBll;

        public PersonelController(IPersonelBll personelBll, IKisiBll kisiBll)
        {
            _personelBll = personelBll;
            _kisiBll = kisiBll;
        }

        [HttpGet("GetPersonelListesi")]
        public ActionResult GetPersonelListesi()
        {
            List<PersonelModel> personel;
            try
            {
                personel = _personelBll.GetPersonelListesi();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(personel);
        }

        [HttpGet("GetPersonelByKisiId")]
        public ActionResult GetPersonelByKisiId(ushort kisiId)
        {
            PersonelModel ogrenci;
            try
            {
                ogrenci = _personelBll.GetPersonelByKisiId(kisiId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(ogrenci);
        }

        [HttpPost("AddPersonel")]
        public ActionResult AddPersonel([FromBody]PersonelModel personelModel)
        {
            PersonelModel personel;         
            try
            {
                personel = _personelBll.AddPersonel(personelModel);   
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(personel);
        }

        [HttpPut("UpdatePersonel")]
        public ActionResult UpdatePersonel([FromBody]PersonelModel personelModel)
        {
            PersonelModel personel;
            try
            {
                personel = _personelBll.UpdatePersonel(personelModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(personel);
        }

        [HttpDelete("DeletePersonel")]
        public ActionResult DeletePersonel([FromBody]PersonelModel personelModel)
        {
            bool personel;
            try
            {
                personel = _personelBll.DeletePersonel(personelModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(personel);
        }
    }
}
