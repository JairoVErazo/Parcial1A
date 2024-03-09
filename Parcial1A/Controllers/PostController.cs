using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Parcial1A.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Parcial1A.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly AutoresdbContext autoresdbContext;

        public PostController(AutoresdbContext autoresdbContext)
        {
            this.autoresdbContext = autoresdbContext;
        }

        // GET: api/<PostController>
        [HttpGet]
        public IActionResult Get()
        {
            List<Post> post = new List<Post>(from e in autoresdbContext.Posts select e).ToList();
            if (post.Count == 0)
            {
                return NotFound();
            }
            else
            {
                return Ok(post);
            }
        }

     
        
        
        // POST api/<PostController>
        [HttpPost]
        [Route("Add")]
        public IActionResult Post([FromBody] Post post)
        {
            try
            {
                autoresdbContext.Add(post);
                autoresdbContext.SaveChanges();
                return Ok(post);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


        // PUT api/<PostController>/5
        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult Put(int id, [FromBody] Post postModificar)
        {
            Post? postActual = (from e in autoresdbContext.Posts where e.Id == id select e).FirstOrDefault();
            if (postActual == null)
            {
                return NotFound();
            }


            postActual.Titulo = postModificar.Titulo;



            autoresdbContext.Entry(postActual).State = EntityState.Modified;
            autoresdbContext.SaveChanges();

            return Ok(postModificar);
        }

        // DELETE api/<PostController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var postAEliminar = autoresdbContext.Posts.Find(id);
            if (postAEliminar == null)
            {
                return NotFound();
            }

            autoresdbContext.Posts.Remove(postAEliminar);
            autoresdbContext.SaveChanges();
            return Ok(postAEliminar);
        }

        [HttpGet]
        [Route("autor/{nombreDelAutor}")]
        public ActionResult<Post> GetUltimosPostsPorAutor(string nombreDelAutor)
        {
            var ultimosPostsDelAutor = (from post in autoresdbContext.Posts
                                        join autor in autoresdbContext.Autores on post.AutorId equals autor.Id
                                        where autor.Nombre == nombreDelAutor
                                        orderby post.FechaPublicacion descending
                                        select post)
                                        .Take(20)
                                        .ToList();

            return Ok(ultimosPostsDelAutor);
        }

        [HttpGet]
        [Route("libro/{tituloDelLibro}")]
        public ActionResult<Post> GetPostsPorLibro(string tituloDelLibro)
        {
            var postsPorLibro = (from post in autoresdbContext.Posts
                                 join autorLibro in autoresdbContext.AutorLibros on post.AutorId equals autorLibro.AutorId
                                 join libro in autoresdbContext.Libros on autorLibro.LibroId equals libro.Id
                                 join autor in autoresdbContext.Autores on post.AutorId equals autor.Id
                                 where libro.Titulo == tituloDelLibro
                                 select new
                                 {
                                     Post = new
                                     {
                                         post.Id,
                                         post.Titulo,
                                         post.Contenido,
                                         post.FechaPublicacion,
                                         Autor = new { autor.Nombre } 
                                     }
                                 })
                             .ToList();


            return Ok(postsPorLibro);
        }


    }
}
