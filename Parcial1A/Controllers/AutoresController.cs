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

      
       
        // POST api/<AutoresController>
        [HttpPost]
        [Route("Add")]
        public IActionResult Post([FromBody] Autore autores)
        {
            try
            {
                _autoresContext.Add(autores);
                _autoresContext.SaveChanges();
                return Ok(autores);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        // PUT api/<AutoresController>/5
        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult Put(int id, [FromBody] Autore autoresModificar)
        {
            Autore? autoresActual = (from e in _autoresContext.Autores where e.Id == id select e).FirstOrDefault();
            if (autoresActual == null)
            {
                return NotFound();
            }


            autoresActual.Nombre = autoresModificar.Nombre;



            _autoresContext.Entry(autoresActual).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _autoresContext.SaveChanges();

            return Ok(autoresModificar);
        }

        // DELETE api/<AutoresController>/5
        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult Delete(int id)
        {
            Autore? autores = (from e in _autoresContext.Autores where e.Id == id select e).FirstOrDefault();
            if (autores == null)
            {
                return NotFound();
            }

            _autoresContext.Autores.Attach(autores);
            _autoresContext.Autores.Remove(autores);
            _autoresContext.SaveChanges();
            return Ok(id);
        }
    }
}
