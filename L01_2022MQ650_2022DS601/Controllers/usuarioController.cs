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

        [HttpGet]
        [Route("GetByNombreApellido")]
        public IActionResult GetByNombreApellido(string? nombre, string? apellido)
        {
            var usuariosFiltrados = _usuarioC.usuarios
                .Where(u => (nombre == null || u.Nombre.Contains(nombre)) &&
                            (apellido == null || u.Apellido.Contains(apellido)))
                .ToList();

            if (usuariosFiltrados.Count == 0)
                return NotFound();

            return Ok(usuariosFiltrados);
        }

        [HttpGet]
        [Route("GetByRol/{rolId}")]
        public IActionResult GetByRol(int rolId)
        {
            var usuariosPorRol = _usuarioC.usuarios
                .Where(u => u.RolId == rolId)
                .ToList();

            if (usuariosPorRol.Count == 0)
                return NotFound();

            return Ok(usuariosPorRol);
        }

        [HttpGet]
        [Route("GetTopUsuariosComentarios/{topN}")]
        public IActionResult GetTopUsuariosComentarios(int topN)
        {
            var topUsuarios = _usuarioC.usuarios
                .Join(_usuarioC.comentarios,
                      u => u.UsuarioId,
                      c => c.UsuarioId,
                      (u, c) => new { u.UsuarioId, u.NombreUsuario })
                .GroupBy(x => new { x.UsuarioId, x.NombreUsuario })
                .Select(g => new { UsuarioId = g.Key.UsuarioId, NombreUsuario = g.Key.NombreUsuario, TotalComentarios = g.Count() })
                .OrderByDescending(u => u.TotalComentarios)
                .Take(topN)
                .ToList();

            if (topUsuarios.Count == 0)
                return NotFound();

            return Ok(topUsuarios);
        }


    }
}
