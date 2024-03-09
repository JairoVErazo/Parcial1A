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
        [Route("/{autorId}/{libroId}")]
        public ActionResult<AutorLibro> ObtenerOrdenAutorLibro(int autorId, int libroId)
        {
            var ordenAutorLibro = _autoresdbContext.AutorLibros
                .Where(al => al.AutorId == autorId && al.LibroId == libroId)
                .Select(al => al.Orden)
                .FirstOrDefault();

            if (ordenAutorLibro == null)
            {
                return NotFound();
            }

            return Ok(ordenAutorLibro);
        }


        // POST api/<AutoresLibroController>
        [HttpPost]
        public ActionResult<AutorLibro> AsignarOrdenAutorLibro([FromBody] AutorLibro nuevoOrden)
        {
            var autorLibroExistente = _autoresdbContext.AutorLibros
                .FirstOrDefault(al => al.AutorId == nuevoOrden.AutorId && al.LibroId == nuevoOrden.LibroId);

            if (autorLibroExistente == null)
            {
                return NotFound();
            }

            autorLibroExistente.Orden = nuevoOrden.Orden;

            _autoresdbContext.SaveChanges();

            return Ok(autorLibroExistente);
        }

        // PUT api/<AutoresLibroController>/5
        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult Put(int id, [FromBody] AutorLibro autorliModificar)
        {
            AutorLibro? autorliActual = (from e in _autoresdbContext.AutorLibros where e.AutorId == id select e).FirstOrDefault();
            if (autorliActual == null)
            {
                return NotFound();
            }

            autorliActual.LibroId = autorliModificar.   LibroId;
            autorliActual.Orden = autorliModificar.Orden;


            _autoresdbContext.Entry(autorliActual).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _autoresdbContext.SaveChanges();

            return Ok(autorliModificar);
        }
        // DELETE api/<AutoresLibroController>/5
        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult Delete(int id)
        {
            AutorLibro? autorLibro = (from e in _autoresdbContext.AutorLibros where e.AutorId == id select e).FirstOrDefault();
            if (autorLibro == null)
            {
                return NotFound();
            }

            _autoresdbContext.AutorLibros.Attach(autorLibro);
            _autoresdbContext.AutorLibros.Remove(autorLibro);
            _autoresdbContext.SaveChanges();
            return Ok(id);
        }
    }
}
