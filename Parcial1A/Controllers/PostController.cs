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
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<PostController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<PostController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<PostController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PostController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
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
