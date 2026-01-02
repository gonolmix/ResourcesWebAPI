using Company.Resources.Core.DTO;
using Company.Resources.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Resources.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ResourcesWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourceController(ResourceContext context) : ControllerBase
    {
        private readonly ResourceContext _context = context;
        // GET: api/<ResourceController>
        [HttpGet]
        [Produces("application/json")]
        public List<ResourceDto> Get()
        {
            IQueryable<ResourceDto> resources = _context.Resources.Include(r => r.Gost).Select(r =>
            new ResourceDto
            {
                ResourceId = r.ResourceId,
                Name = r.Name,
                GostId = r.GostId,
                GostName = r.Gost.Name,
                Characteristics = r.Characteristics,
                Unit = r.Unit
            });
            return [.. resources];
        }

        // GET api/<ResourceController>/5
        [HttpGet("{id}")]
        [Produces("application/json")]
        public IActionResult Get(int id)
        {
            var resource = _context.Resources.Include(r => r.Gost).FirstOrDefault(r => r.ResourceId == id);
            if (resource == null)
                return NotFound();
            var resourceDto = new ResourceDto
            {
                ResourceId = resource.ResourceId,
                Name = resource.Name,
                GostId = resource.GostId,
                GostName = resource.Gost.Name,
                Characteristics = resource.Characteristics,
                Unit = resource.Unit
            };
            return new ObjectResult(resourceDto);
        }

        // POST api/<ResourceController>
        [HttpPost]
        public IActionResult Post([FromBody] CreateResourceDto resourceDto)
        {
            if (resourceDto == null)
            {
                return BadRequest();
            }

            var resource = new Resource
            {
                Name = resourceDto.Name,
                GostId = resourceDto.GostId,
                Characteristics = resourceDto.Characteristics,
                Unit = resourceDto.Unit
            };

            _context.Resources.Add(resource);
            _context.SaveChanges();

            return Ok(resource);
        }

        // PUT api/<ResourceController>/5
        [HttpPut]
        public IActionResult Put([FromBody] UpdateResourceDto resourceDto)
        {
            if (resourceDto == null)
            {
                return BadRequest();
            }
            if (!_context.Resources.Any(r => r.ResourceId == resourceDto.Id))
            {
                return NotFound();
            }

            var resource = new Resource
            {
                ResourceId = resourceDto.Id,
                Name = resourceDto.Name,
                GostId = resourceDto.GostId,
                Characteristics = resourceDto.Characteristics,
                Unit = resourceDto.Unit
            };

            _context.Update(resource);
            _context.SaveChanges();

            return Ok(resource);
        }

        // DELETE api/<ResourceController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var resource = _context.Resources.FirstOrDefault(r => r.ResourceId == id);
            if (resource == null)
            {
                return NotFound();
            }

            _context.Resources.Remove(resource);
            _context.SaveChanges();

            return Ok(resource);
        }
    }
}
