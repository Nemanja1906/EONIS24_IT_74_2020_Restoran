using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restoran.Data;
using Restoran.Entities;
using AutoMapper;
using Restoran.Dto;
using Microsoft.AspNetCore.Authorization;

namespace Restoran.Controllers
{
    [ApiController]
    [Route("api/musterija")]
    [Produces("application/json", "application/xml")]
    public class MusterijaController : Controller
    {
        private readonly IMusterijaRepository musterijaRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public MusterijaController(IMusterijaRepository musterijaRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.musterijaRepository = musterijaRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

      
        [HttpGet]
        [HttpHead]  
        [ProducesResponseType(StatusCodes.Status200OK)] 
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<MusterijaDto>> GetMusterijas()
        {
            var musterijas = musterijaRepository.GetMusterija();
            if (musterijas == null || musterijas.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<MusterijaDto>>(musterijas));
        }

     
        [HttpGet("{musterijaID}")]
        public ActionResult<MusterijaDto> GetMusterija(Guid musterijaID)
        {
            var musterija = musterijaRepository.GetMusterijaById(musterijaID);
            if (musterija == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<MusterijaDto>(musterija));

        }

        
        [HttpPut]
        public ActionResult<MusterijaDto> UpdateMusterija(MusterijaDto musterija)
        {
           
            try
            {
                var oldMusterija = musterijaRepository.GetMusterijaById(musterija.MusterijaID);
                if (oldMusterija == null)
                {
                    return NotFound();
                }
                Musterija mappedMusterija = mapper.Map<Musterija>(musterija);
                var updatedMusterija = musterijaRepository.UpdateMusterija(mappedMusterija);
                MusterijaDto updatedMusterijaDto = mapper.Map<MusterijaDto>(updatedMusterija);
                return Ok(updatedMusterija);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error.");
            }


        }

      
        [HttpPost]
        public ActionResult<MusterijaDto> CreateMusterija([FromBody] MusterijaDto musterija)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                Musterija musterijaEntity = mapper.Map<Musterija>(musterija);
                Musterija musterijaConfirmation = musterijaRepository.CreateMusterija(musterijaEntity);
                musterijaRepository.SaveChanges();
                string location = linkGenerator.GetPathByAction("GetMusterija", "Musterija", new { musterijaID = musterijaConfirmation.MusterijaID });
                return Created(location, mapper.Map<MusterijaDto>(musterijaConfirmation));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create error");
            }
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{musterijaID}")]
        public IActionResult DeleteMusterija(Guid musterijaID)
        {
            try
            {
                var musterija = musterijaRepository.GetMusterijaById(musterijaID);
                if (musterija == null)
                {
                    return NotFound();
                }
                musterijaRepository.DeleteMusterija(musterijaID);
                musterijaRepository.SaveChanges();
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete error");
            }
        }

        [HttpOptions]
        [AllowAnonymous] 
        public IActionResult GetMusterijaOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
