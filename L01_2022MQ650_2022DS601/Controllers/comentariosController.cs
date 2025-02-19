using L01_2022MQ650_2022DS601.Modelos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PracticaApi.Modelos;

namespace L01_2022MQ650_2022DS601.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class comentariosController : ControllerBase
    {
        private readonly blogDBcontext _comentarioC;

        public comentariosController(blogDBcontext comentarios)
        {
            _comentarioC = comentarios;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<comentarios> listaComentario = (from c in _comentarioC.comentarios
                                                 select c).ToList();
            if (listaComentario.Count() == 0)
            {
                return NotFound();
            }
            return Ok(listaComentario);
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult Add([FromBody] comentarios comentario)
        {
            try
            {
                _comentarioC.comentarios.Add(comentario);
                _comentarioC.SaveChanges();
                return Ok(comentario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        [HttpPut]
        [Route("Modify/{id}")]
        public IActionResult Modify(int id, [FromBody] comentarios comentarioModificar)
        {
            comentarios? comentarioActual = (from c in _comentarioC.comentarios
                                             where c.ComentarioId == id
                                             select c).FirstOrDefault();

            if (comentarioActual == null)
            {
                return NotFound();
            }

            comentarioActual.Comentario = comentarioModificar.Comentario;

            _comentarioC.Entry(comentarioActual).State = EntityState.Modified;
            _comentarioC.SaveChanges();

            return Ok(comentarioActual);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult Delete(int id)
        {
            comentarios? comentario = (from c in _comentarioC.comentarios
                                       where c.ComentarioId == id
                                       select c).FirstOrDefault();
            if (comentario == null)
                return NotFound();
            _comentarioC.comentarios.Attach(comentario);
            _comentarioC.comentarios.Remove(comentario);
            _comentarioC.SaveChanges();
            return Ok(comentario);
        }

        //además de un método que permita retornar el listado de los comentarios filtradas por un usuario en específico.
        [HttpGet]
        [Route("GetByUser/{userId}")]
        public IActionResult GetByUser(int userId)
        {
            var comentariosUsuario = from c in _comentarioC.comentarios
                                     where c.UsuarioId == userId
                                     select c;

            if (!comentariosUsuario.Any())
            {
                return NotFound();
            }

            return Ok(comentariosUsuario);
        }
    }
}
