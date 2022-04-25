using Microsoft.AspNetCore.Mvc;
using SamuraiApp.Data.Interface;
using SamuraiApp.Domain;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SamuraiApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SamuraisController : ControllerBase
    {
        private readonly ISamurai _samurais;

        public SamuraisController(ISamurai samurais)
        {
            _samurais = samurais;
        }
        // GET: api/<SamuraisController>
        [HttpGet]
        public async Task<IEnumerable<Samurai>> Get()
        {
            var results = await _samurais.GetAll();
            return results;
        }

        // GET api/<SamuraisController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Samurai>> GetById(int id)
        {
            var result = await _samurais.GetById(id);
            if(result == null)
                return NotFound();
            else
                return Ok(result);
        }

        // POST api/<SamuraisController>
        [HttpPost]
        public async Task<ActionResult<Samurai>> Post([FromBody] Samurai samurai)
        {
            try
            {
                var result = await _samurais.Insert(samurai);
                return CreatedAtAction("GetById", new {id=samurai.Id}, samurai);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<SamuraisController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Samurai>> Put(int id, Samurai samurai)
        {
            try
            {
                var result = _samurais.Update(id, samurai);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<SamuraisController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
