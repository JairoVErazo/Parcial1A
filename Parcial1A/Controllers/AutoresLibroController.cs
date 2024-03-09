using Microsoft.AspNetCore.Mvc;
using Parcial1A.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Parcial1A.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutoresLibroController : ControllerBase
    {
        private readonly AutoresdbContext _autoresdbContext;

        public AutoresLibroController(AutoresdbContext autoresdbContext)
        {
            _autoresdbContext = autoresdbContext;
        }
        // GET: api/<AutoresLibroController>
        [HttpGet]
        public IActionResult Get()
        {
            List<AutorLibro> autorlibro = new List<AutorLibro>(from e in _autoresdbContext.AutorLibros select e).ToList();
            if (autorlibro.Count == 0)
            {
                return NotFound();
            }
            else
            {
                return Ok(autorlibro);
            }
        }
        // GET api/<AutoresLibroController>/5
        [HttpGet("{id}")]
        public IActionResult Getbyid(int id)
        {
            List<AutorLibro> publicaciones = _autoresdbContext.AutorLibros.Where(u => u.AutorId == id).ToList();

            if (publicaciones.Count == 0)
            {
                return NotFound();
            }

            return Ok(publicaciones);
        }

        // POST api/<AutoresLibroController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<AutoresLibroController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AutoresLibroController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
