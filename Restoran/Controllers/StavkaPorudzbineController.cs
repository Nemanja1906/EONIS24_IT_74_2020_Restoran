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
    [Route("api/stavkaPorudzbine")]
    [Produces("application/json", "application/xml")]
    public class StavkaPorudzbineController : Controller
    {
        private readonly IStavkaPorudzbineRepository stavkaPorudzbineRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public StavkaPorudzbineController(IStavkaPorudzbineRepository stavkaPorudzbineRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.stavkaPorudzbineRepository = stavkaPorudzbineRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

      
        [HttpGet]
        [HttpHead] 
        [ProducesResponseType(StatusCodes.Status200OK)] 
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<StavkaPorudzbineDto>> GetStavkaPorudzbines()
        {
            var stavkaPorudzbines = stavkaPorudzbineRepository.GetStavkaPorudzbine();
            if (stavkaPorudzbines == null || stavkaPorudzbines.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<StavkaPorudzbineDto>>(stavkaPorudzbines));
        }

      
        [HttpGet("{stavkaPorudzbineID}")]
        public ActionResult<StavkaPorudzbineDto> GetStavkaPorudzbine(Guid porudzbinaID)
        {
            var stavkaPorudzbine = stavkaPorudzbineRepository.GetStavkaPorudzbineById(porudzbinaID);
            if (stavkaPorudzbine == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<StavkaPorudzbineDto>(stavkaPorudzbine));

        }

      
        [HttpPut]
        public ActionResult<StavkaPorudzbineDto> UpdateStavkaPorudzbine(StavkaPorudzbineDto stavkaPorudzbine)
        {
           
            try
            {
                var oldStavkaPorudzbine = stavkaPorudzbineRepository.GetStavkaPorudzbineById(stavkaPorudzbine.PorudzbinaID);
                if (oldStavkaPorudzbine == null)
                {
                    return NotFound();
                }
                StavkaPorudzbine mappedStavkaPorudzbine = mapper.Map<StavkaPorudzbine>(stavkaPorudzbine);
                var updatedStavkaPorudzbine = stavkaPorudzbineRepository.UpdateStavkaPorudzbine(mappedStavkaPorudzbine);
                StavkaPorudzbineDto updatedStavkaPorudzbineDto = mapper.Map<StavkaPorudzbineDto>(updatedStavkaPorudzbine);
                return Ok(updatedStavkaPorudzbine);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error.");
            }


        }

      
        [HttpPost]
        public ActionResult<StavkaPorudzbineDto> CreateStavkaPorudzbine([FromBody] StavkaPorudzbineDto stavkaPorudzbine)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                StavkaPorudzbine stavkaPorudzbineEntity = mapper.Map<StavkaPorudzbine>(stavkaPorudzbine);
                StavkaPorudzbine stavkaPorudzbineConfirmation = stavkaPorudzbineRepository.CreateStavkaPorudzbine(stavkaPorudzbineEntity);
                stavkaPorudzbineRepository.SaveChanges();
                string location = linkGenerator.GetPathByAction("GetStavkaPorudzbine", "StavkaPorudzbine", new { porudzbinaID = stavkaPorudzbineConfirmation.PorudzbinaID });
                return Created(location, mapper.Map<StavkaPorudzbineDto>(stavkaPorudzbineConfirmation));
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
        public IActionResult DeleteStavkaPorudzbine(Guid porudzbinaID)
        {
            try
            {
                var stavkaPorudzbine = stavkaPorudzbineRepository.GetStavkaPorudzbineById(porudzbinaID);
                if (stavkaPorudzbine == null)
                {
                    return NotFound();
                }
                stavkaPorudzbineRepository.DeleteStavkaPorudzbine(porudzbinaID);
                stavkaPorudzbineRepository.SaveChanges();
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete error");
            }
        }

        [HttpOptions]
        [AllowAnonymous] 
        public IActionResult GetStavkaPorudzbineOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
