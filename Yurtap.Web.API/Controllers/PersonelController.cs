using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Yurtap.Business.Abstract;
using Yurtap.Core.Business.Models;
using Yurtap.Entity;
using Yurtap.Entity.Models;

namespace Yurtap.Web.API.Controllers
{
    //[Authorize]
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
            var personelListesi = _personelBll.GetPersonelListesi();
            if (personelListesi.Success)
            {
                return Ok(personelListesi);
            }
            return NotFound(personelListesi);
        }

        [HttpGet("GetPersonelByKisiId")]
        public ActionResult GetPersonelByKisiId(int kisiId)
        {
            var ogrenci = _personelBll.GetPersonelByKisiId(kisiId);
            if (ogrenci.Success)
            {
                return Ok(ogrenci);
            }
            return NotFound(ogrenci);
        }

        [HttpPost("AddPersonel")]
        public ActionResult AddPersonel([FromBody]PersonelModel personelModel)
        {
            var personel = _personelBll.AddPersonel(personelModel);
            if (personel.Success)
            {
                return Created("",personel);
            }
            return BadRequest(personel);
        }

        [HttpPut("UpdatePersonel")]
        public ActionResult UpdatePersonel([FromBody]PersonelModel personelModel)
        {
            var personel = _personelBll.UpdatePersonel(personelModel);
            if (personel.Success)
            {
                return Ok(personel);
            }
            return BadRequest(personel);
        }

        [HttpDelete("DeletePersonel")]
        public ActionResult DeletePersonel([FromBody]PersonelModel personelModel)
        {
            var personel = _personelBll.DeletePersonel(personelModel);
            if (personel.Success)
            {
                return Ok(personel);
            }
            return BadRequest(personel);
        }
    }
}
