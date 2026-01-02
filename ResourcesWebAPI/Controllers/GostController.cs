using Company.Resources.Core.DTO;
using Company.Resources.Infrastructure.Data;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Resources.Models;

namespace ResourcesWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GostController (ResourceContext context) : ControllerBase
    {
        private readonly ResourceContext _context = context;

        [HttpGet]
        [Produces("application/json")]
        public List<Gost> Get()
        {
            IQueryable<Gost> gosts = _context.Gosts;
            return [.. gosts];
        }

        // GET api/<GostController>/5
        [HttpGet("{id}")]
        [Produces("application/json")]
        public IActionResult Get(int id)
        {
            var gost = _context.Gosts.FirstOrDefault(g => g.GostId == id);
            if (gost == null)
                return NotFound();
            return new ObjectResult(gost);
        }

        // POST api/<GostController>
        [HttpPost]
        public IActionResult Post([FromBody] Gost gost)
        {
            if (gost == null) 
            { 
                return BadRequest();
            }

            _context.Gosts.Add(gost);
            _context.SaveChanges();

            return Ok(gost);
        }

        // PUT api/<GostController>/5
        [HttpPut]
        public IActionResult Put([FromBody] Gost gost)
        {
            if (gost == null)
            {
                return BadRequest();
            }
            if (!_context.Gosts.Any(g => g.GostId == gost.GostId))
            {
                return NotFound();
            }
            _context.Update(gost);
            _context.SaveChanges();

            return Ok(gost);
        }

        // DELETE api/<GostController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var gost = _context.Gosts.FirstOrDefault(g => g.GostId == id);
            if (gost == null)
            {
                return NotFound();
            }

            _context.Gosts.Remove(gost);
            _context.SaveChanges();

            return Ok(gost);
        }
    }
}
