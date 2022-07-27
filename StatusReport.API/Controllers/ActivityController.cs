using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StatusReport.API.Models;
using StatusReport.API.Services;

namespace StatusReport.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        private readonly ICosmosDbService<Activity> _cosmosDbService;
        public ActivityController(ICosmosDbService<Activity> cosmosDbService)
        {
            _cosmosDbService = cosmosDbService ?? throw new ArgumentNullException(nameof(cosmosDbService));
        }
        // GET api/items
        [HttpGet]
        public async Task<IActionResult> GetAllActivity()
        {
            return Ok(await _cosmosDbService.GetMultipleAsync("SELECT * FROM c"));
        }
        // GET api/items/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await _cosmosDbService.GetAsync(id));
        }
        // POST api/items
        [HttpPost]
        public async Task<IActionResult> AddNewActivity([FromBody] Activity item)
        {
            item.id = Guid.NewGuid().ToString();
            await _cosmosDbService.AddAsync(item);
            return CreatedAtAction(nameof(Get), new { id = item.id }, item);
        }
        // PUT api/items/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] Activity item)
        {
            await _cosmosDbService.UpdateAsync(item.id, item);
            return NoContent();
        }
        // DELETE api/items/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _cosmosDbService.DeleteAsync(id);
            return NoContent();
        }
    }
}
