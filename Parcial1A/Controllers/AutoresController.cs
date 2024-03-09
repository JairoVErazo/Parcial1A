using Microsoft.AspNetCore.Mvc;
using Parcial1A.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Parcial1A.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutoresController : ControllerBase
    {
        // GET: api/<AutoresController>
        private readonly AutoresdbContext _autoresContext;

        public AutoresController(AutoresdbContext autoresContext)
        {
            _autoresContext = autoresContext;
        }
        [HttpGet]
        public IActionResult Get()
        {
            List<Autore> autores = new List<Autore>(from e in _autoresContext.Autores select e).ToList();
            if (autores.Count == 0)
            {
                return NotFound();
            }
            else
            {
                return Ok(autores);
            }
        }

        // GET api/<AutoresController>/5
        [HttpGet("{id}")]
        public IActionResult Getbyid(int id)
        {
            List<Autore> autores = _autoresContext.Autores.Where(u => u.Id == id).ToList();

            if (autores.Count == 0)
            {
                return NotFound();
            }

            return Ok(autores);
        }

        // POST api/<AutoresController>
        [HttpPost]
        [Route("Add")]
        public IActionResult Post([FromBody] Autore publicaciones)
        {
            try
            {
                _autoresContext.Add(publicaciones);
                _autoresContext.SaveChanges();
                return Ok(publicaciones);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        // PUT api/<AutoresController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AutoresController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
