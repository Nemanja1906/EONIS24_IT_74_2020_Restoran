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
    [Route("api/proizvod")]
    [Produces("application/json", "application/xml")]
    public class ProizvodController : Controller
    {
        private readonly IProizvodRepository proizvodRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public ProizvodController(IProizvodRepository proizvodRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.proizvodRepository = proizvodRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

       
        [HttpGet]
        [HttpHead] 
        [ProducesResponseType(StatusCodes.Status200OK)] 
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<ProizvodDto>> GetProizvods()
        {
            var proizvods = proizvodRepository.GetProizvod();
            if (proizvods == null || proizvods.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<ProizvodDto>>(proizvods));
        }

        [HttpGet("{proizvodID}")]
        public ActionResult<ProizvodDto> GetProizvod(Guid proizvodID)
        {
            var proizvod = proizvodRepository.GetProizvodById(proizvodID);
            if (proizvod == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<ProizvodDto>(proizvod));

        }

     
        [HttpPut]
        public ActionResult<ProizvodDto> UpdateProizvod(ProizvodDto proizvod)
        {
           
            try
            {
                var oldProizvod = proizvodRepository.GetProizvodById(proizvod.ProizvodID);
                if (oldProizvod == null)
                {
                    return NotFound();
                }
                Proizvod mappedProizvod = mapper.Map<Proizvod>(proizvod);
                var updatedProizvod = proizvodRepository.UpdateProizvod(mappedProizvod);
                ProizvodDto updatedProizvodDto = mapper.Map<ProizvodDto>(updatedProizvod);
                return Ok(updatedProizvod);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error.");
            }


        }

    
        [HttpPost]
        public ActionResult<ProizvodDto> CreateProizvod([FromBody] ProizvodDto proizvod)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                Proizvod proizvodEntity = mapper.Map<Proizvod>(proizvod);
                Proizvod proizvodConfirmation = proizvodRepository.CreateProizvod(proizvodEntity);
                proizvodRepository.SaveChanges();
                string location = linkGenerator.GetPathByAction("GetProizvod", "Proizvod", new { proizvodID = proizvodConfirmation.ProizvodID });
                return Created(location, mapper.Map<ProizvodDto>(proizvodConfirmation));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create error");
            }
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{proizvodID}")]
        public IActionResult DeleteProizvod(Guid proizvodID)
        {
            try
            {
                var proizvod = proizvodRepository.GetProizvodById(proizvodID);
                if (proizvod == null)
                {
                    return NotFound();
                }
                proizvodRepository.DeleteProizvod(proizvodID);
                proizvodRepository.SaveChanges();
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete error");
            }
        }

        [HttpOptions]
        [AllowAnonymous] 
        public IActionResult GetProizvodOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
