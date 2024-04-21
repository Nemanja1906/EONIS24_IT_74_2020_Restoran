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
    [Route("api/zaposleni")]
    [Produces("application/json", "application/xml")]
    public class ZaposleniController : Controller
    {
        private readonly IZaposleniRepository zaposleniRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public ZaposleniController(IZaposleniRepository zaposleniRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.zaposleniRepository = zaposleniRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

       
        [HttpGet]
        [HttpHead] 
        [ProducesResponseType(StatusCodes.Status200OK)] 
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<ZaposleniDto>> GetZaposlenis()
        {
            var zaposlenis = zaposleniRepository.GetZaposleni();
            if (zaposlenis == null || zaposlenis.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<ZaposleniDto>>(zaposlenis));
        }

 
        [HttpGet("{zaposleniID}")]
        public ActionResult<ZaposleniDto> GetZaposleni(Guid zaposleniID)
        {
            var zaposleni = zaposleniRepository.GetZaposleniById(zaposleniID);
            if (zaposleni == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<ZaposleniDto>(zaposleni));

        }

      
        [HttpPut]
        public ActionResult<ZaposleniDto> UpdateZaposleni(ZaposleniDto zaposleni)
        {
           
            try
            {
                var oldZaposleni = zaposleniRepository.GetZaposleniById(zaposleni.ZaposleniID);
                if (oldZaposleni == null)
                {
                    return NotFound();
                }
                Zaposleni mappedZaposleni = mapper.Map<Zaposleni>(zaposleni);
                var updatedZaposleni = zaposleniRepository.UpdateZaposleni(mappedZaposleni);
                ZaposleniDto updatedZaposleniDto = mapper.Map<ZaposleniDto>(updatedZaposleni);
                return Ok(updatedZaposleni);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error.");
            }


        }

      
        [HttpPost]
        public ActionResult<ZaposleniDto> CreateZaposleni([FromBody] ZaposleniDto zaposleni)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                Zaposleni zaposleniEntity = mapper.Map<Zaposleni>(zaposleni);
                Zaposleni zaposleniConfirmation = zaposleniRepository.CreateZaposleni(zaposleniEntity);
                zaposleniRepository.SaveChanges();
                string location = linkGenerator.GetPathByAction("GetZaposleni", "Zaposleni", new { zaposleniID = zaposleniConfirmation.ZaposleniID });
                return Created(location, mapper.Map<ZaposleniDto>(zaposleniConfirmation));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create error");
            }
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{zaposleniID}")]
        public IActionResult DeleteZaposleni(Guid zaposleniID)
        {
            try
            {
                var zaposleni = zaposleniRepository.GetZaposleniById(zaposleniID);
                if (zaposleni == null)
                {
                    return NotFound();
                }
                zaposleniRepository.DeleteZaposleni(zaposleniID);
                zaposleniRepository.SaveChanges();
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete error");
            }
        }

        [HttpOptions]
        [AllowAnonymous] 
        public IActionResult GetZaposleniOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
