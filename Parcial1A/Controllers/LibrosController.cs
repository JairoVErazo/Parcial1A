using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Parcial1A.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Parcial1A.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibrosController : ControllerBase
    {
        private readonly AutoresdbContext autoresdbContext;

        public LibrosController( AutoresdbContext autoresdbContext)
        {
            this.autoresdbContext = autoresdbContext;
        }
        // GET: api/<LibrosController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<LibrosController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<LibrosController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<LibrosController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<LibrosController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [Route("autor/{nombreDelAutor}")]
        public ActionResult<Libro> BuscarLibrosPorAutor(string nombreDelAutor)
        {
            var librosPorAutor = (from autor in autoresdbContext.Autores
                                  join autorLibro in autoresdbContext.AutorLibros on autor.Id equals autorLibro.AutorId
                                  join libro in autoresdbContext.Libros on autorLibro.LibroId equals libro.Id
                                  where autor.Nombre == nombreDelAutor
                                  select libro)
                                  .Distinct()
                                  .ToList();

            return Ok(librosPorAutor);
        }
    }
}
