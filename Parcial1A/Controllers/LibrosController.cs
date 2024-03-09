using Microsoft.AspNetCore.Mvc;
using Parcial1A.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Parcial1A.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibrosController : ControllerBase
    {
        // GET: api/<LibrosController>
        private readonly AutoresdbContext _librosContext;

        public LibrosController(AutoresdbContext librosContext)
        {
            _librosContext = librosContext;
        }
        [HttpGet]
        public IActionResult Get()
        {
            List<Libro> libros = new List<Libro>(from e in _librosContext.Libros select e).ToList();
            if (libros.Count == 0)
            {
                return NotFound();
            }
            else
            {
                return Ok(libros);
            }
        }

       
        // POST api/<publicacionesController>
        [HttpPost]
        [Route("Add")]
        public IActionResult Post([FromBody] Libro libros)
        {
            try
            {
                _librosContext.Add(libros);
                _librosContext.SaveChanges();
                return Ok(libros);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        // PUT api/<publicacionesController>/5
        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult Put(int id, [FromBody] Libro librosModificar)
        {
            Libro? librosActual = (from e in _librosContext.Libros where e.Id == id select e).FirstOrDefault();
            if (librosActual == null)
            {
                return NotFound();
            }


            librosActual.Titulo = librosModificar.Titulo;
            


            _librosContext.Entry(librosActual).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _librosContext.SaveChanges();

            return Ok(librosModificar);
        }

        // DELETE api/<publicacionesController>/5
        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult Delete(int id)
        {
            Libro? libros = (from e in _librosContext.Libros where e.Id == id select e).FirstOrDefault();
            if (libros == null)
            {
                return NotFound();
            }

            _librosContext.Libros.Attach(libros);
            _librosContext.Libros.Remove(libros);
            _librosContext.SaveChanges();
            return Ok(id);
        }
    }
}
