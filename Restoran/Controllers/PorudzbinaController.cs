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
    [Route("api/porudzbina")]
    [Produces("application/json", "application/xml")]
    public class PorudzbinaController : Controller
    {
        private readonly IPorudzbinaRepository porudzbinaRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public PorudzbinaController(IPorudzbinaRepository porudzbinaRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.porudzbinaRepository = porudzbinaRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

       
        [HttpGet]
        [HttpHead] 
        [ProducesResponseType(StatusCodes.Status200OK)] 
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<PorudzbinaDto>> GetPorudzbinas()
        {
            var porudzbinas = porudzbinaRepository.GetPorudzbina();
            if (porudzbinas == null || porudzbinas.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<PorudzbinaDto>>(porudzbinas));
        }

        [HttpGet("{porudzbinaID}")]
        public ActionResult<PorudzbinaDto> GetPorudzbina(Guid porudzbinaID)
        {
            var porudzbina = porudzbinaRepository.GetPorudzbinaById(porudzbinaID);
            if (porudzbina == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<PorudzbinaDto>(porudzbina));

        }

        
        [HttpPut]
        public ActionResult<PorudzbinaDto> UpdatePorudzbina(PorudzbinaDto porudzbina)
        {
           
            try
            {
                var oldPorudzbina = porudzbinaRepository.GetPorudzbinaById(porudzbina.PorudzbinaID);
                if (oldPorudzbina == null)
                {
                    return NotFound();
                }
                Porudzbina mappedPorudzbina = mapper.Map<Porudzbina>(porudzbina);
                var updatedPorudzbina = porudzbinaRepository.UpdatePorudzbina(mappedPorudzbina);
                PorudzbinaDto updatedPorudzbinaDto = mapper.Map<PorudzbinaDto>(updatedPorudzbina);
                return Ok(updatedPorudzbina);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error.");
            }


        }

  
        [HttpPost]
        public ActionResult<PorudzbinaDto> CreatePorudzbina([FromBody] PorudzbinaDto porudzbina)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                Porudzbina porudzbinaEntity = mapper.Map<Porudzbina>(porudzbina);
                Porudzbina porudzbinaConfirmation = porudzbinaRepository.CreatePorudzbina(porudzbinaEntity);
                porudzbinaRepository.SaveChanges();
                string location = linkGenerator.GetPathByAction("GetPorudzbina", "Porudzbina", new { porudzbinaID = porudzbinaConfirmation.PorudzbinaID });
                return Created(location, mapper.Map<PorudzbinaDto>(porudzbinaConfirmation));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create error");
            }
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{porudzbinaID}")]
        public IActionResult DeletePorudzbina(Guid porudzbinaID)
        {
            try
            {
                var porudzbina = porudzbinaRepository.GetPorudzbinaById(porudzbinaID);
                if (porudzbina == null)
                {
                    return NotFound();
                }
                porudzbinaRepository.DeletePorudzbina(porudzbinaID);
                porudzbinaRepository.SaveChanges();
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete error");
            }
        }

        [HttpOptions]
        [AllowAnonymous] 
        public IActionResult GetPorudzbinaOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
