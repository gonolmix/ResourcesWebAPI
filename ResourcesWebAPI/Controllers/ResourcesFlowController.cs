using Company.Resources.Core.DTO;
using Company.Resources.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Resources.Models;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ResourcesWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourcesFlowController(ResourceContext context) : ControllerBase
    {
        private readonly ResourceContext _context = context;
        // GET: api/<ResourceController>
        [HttpGet]
        [Produces("application/json")]
        public List<ResourceFlowDto> Get()
        {
            IQueryable<ResourceFlowDto> resourcesFlows = _context.ResourcesFlows.Include(rf => rf.Resource).Select(rf =>
            new ResourceFlowDto
            {
                ResourceFlowId = rf.ResourceFlowId,
                ResourceId = rf.ResourceId,
                Quarter = rf.Quarter,
                DeliveredQty = rf.DeliveredQty,
                Year = rf.Year,
                UsedQty = rf.UsedQty,
                ResourceName = rf.Resource.Name
            });
            return [.. resourcesFlows];
        }

        // GET api/<ResourceController>/5
        [HttpGet("{id}")]
        [Produces("application/json")]
        public IActionResult Get(int id)
        {
            var resourceFlow = _context.ResourcesFlows.Include(rf => rf.Resource).FirstOrDefault(rf => rf.ResourceFlowId == id);
            if (resourceFlow == null)
                return NotFound();
            var resourceFlowDto = new ResourceFlowDto
            {
                ResourceFlowId = resourceFlow.ResourceFlowId,
                ResourceId = resourceFlow.ResourceId,
                Quarter = resourceFlow.Quarter,
                DeliveredQty = resourceFlow.DeliveredQty,
                Year = resourceFlow.Year,
                UsedQty = resourceFlow.UsedQty,
                ResourceName = resourceFlow.Resource.Name
            };
            return new ObjectResult(resourceFlowDto);
        }

        // GET api/<ResourceController>/5
        [HttpGet("flows-by-resources")]
        [Produces("application/json")]
        public List<ResourceFlowDto> GetResourceFlowsByResourceName(string resourceName)
        {
            IQueryable<ResourceFlowDto> resourcesFlows = _context.ResourcesFlows.Include(rf => rf.Resource).Select(rf =>
            new ResourceFlowDto
            {
                ResourceFlowId = rf.ResourceFlowId,
                ResourceId = rf.ResourceId,
                Quarter = rf.Quarter,
                DeliveredQty = rf.DeliveredQty,
                Year = rf.Year,
                UsedQty = rf.UsedQty,
                ResourceName = rf.Resource.Name
            }).Where(rf => rf.ResourceName.ToLower().Contains(resourceName.ToLower()));
            return [.. resourcesFlows];
        }

        [HttpGet("resources")]
        [Produces("application/json")]
        public IEnumerable<Resource> GetResources()
        {
            return _context.Resources.ToList();
        }

        // POST api/<ResourceController>
        [HttpPost]
        public IActionResult Post([FromBody] CreateResourceFlowDto resourceFlowDto)
        {
            if (resourceFlowDto == null)
            {
                return BadRequest();
            }

            var resourceFlow = new ResourceFlow
            {
                ResourceId = resourceFlowDto.ResourceId,
                Quarter = resourceFlowDto.Quarter,
                DeliveredQty = resourceFlowDto.DeliveredQty,
                Year = resourceFlowDto.Year,
                UsedQty = resourceFlowDto.UsedQty
            };

            _context.ResourcesFlows.Add(resourceFlow);
            _context.SaveChanges();

            return Ok(resourceFlow);
        }

        // PUT api/<ResourceController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateResourceFlowDto resourceFlowDto)
        {
            if (resourceFlowDto == null)
                return BadRequest("DTO is null");

            if (resourceFlowDto.Id != id)
                return BadRequest("ID in URL does not match ID in body");

            if (!_context.ResourcesFlows.Any(rf => rf.ResourceFlowId == id))
                return NotFound();

            var resourceFlow = _context.ResourcesFlows.First(rf => rf.ResourceFlowId == id);

            resourceFlow.ResourceId = resourceFlowDto.ResourceId;
            resourceFlow.Year = resourceFlowDto.Year;
            resourceFlow.Quarter = resourceFlowDto.Quarter;
            resourceFlow.DeliveredQty = resourceFlowDto.DeliveredQty;
            resourceFlow.UsedQty = resourceFlowDto.UsedQty;

            _context.SaveChanges();

            return Ok(resourceFlow);
        }

        // DELETE api/<ResourceController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var resourceFlow = _context.ResourcesFlows.FirstOrDefault(rf => rf.ResourceFlowId == id);
            if (resourceFlow == null)
            {
                return NotFound();
            }

            _context.ResourcesFlows.Remove(resourceFlow);
            _context.SaveChanges();

            return Ok(resourceFlow);
        }
    }
}
