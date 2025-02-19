using L01_2022MQ650_2022DS601.Modelos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PracticaApi.Modelos;

namespace L01_2022MQ650_2022DS601.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class usuarioController : ControllerBase
    {

        private readonly blogDBcontext _usuarioC;

        public usuarioController(blogDBcontext usuarios)
        {
            _usuarioC = usuarios;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<usuarios> listaUsuario = (from u in _usuarioC.usuarios
                                                 select u).ToList();
            if (listaUsuario.Count() == 0)
            {
                return NotFound();
            }
            return Ok(listaUsuario);
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult Add([FromBody] usuarios usuario)
        {
            try
            {
                _usuarioC.usuarios.Add(usuario);
                _usuarioC.SaveChanges();
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        [HttpPut]
        [Route("Modify/{id}")]
        public IActionResult Modify(int id, [FromBody] usuarios usuarioModificar)
        {
            usuarios? usuarioActual = (from u in _usuarioC.usuarios
                                             where u.UsuarioId == id
                                             select u).FirstOrDefault();

            if (usuarioActual == null)
            {
                return NotFound();
            }

            usuarioActual.NombreUsuario = usuarioModificar.NombreUsuario;
            usuarioActual.Nombre = usuarioModificar.Nombre;
            usuarioActual.Apellido = usuarioModificar.Apellido;
            usuarioActual.Clave = usuarioModificar.Clave;

            _usuarioC.Entry(usuarioActual).State = EntityState.Modified;
            _usuarioC.SaveChanges();

            return Ok(usuarioActual);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult Delete(int id)
        {
            usuarios? usuario = (from u in _usuarioC.usuarios
                                       where u.UsuarioId == id
                                       select u).FirstOrDefault();
            if (usuario == null)
                return NotFound();
            _usuarioC.usuarios.Attach(usuario);
            _usuarioC.usuarios.Remove(usuario);
            _usuarioC.SaveChanges();
            return Ok(usuario);
        }

    }
}
