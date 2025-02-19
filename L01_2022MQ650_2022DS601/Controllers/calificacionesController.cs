using L01_2022MQ650_2022DS601.Modelos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PracticaApi.Modelos;

namespace L01_2022MQ650_2022DS601.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class calificacionesController : ControllerBase
    {
        private readonly blogDBcontext _blogDBcontexto;

        public calificacionesController(blogDBcontext blogDBcontexto)
        {
            _blogDBcontexto = blogDBcontexto;
        }



        [HttpGet]
        [Route("GetAll")]

        public IActionResult Get()
        {
            List<calificaciones> listadoEquipo = (from c in _blogDBcontexto.calificaciones select c).ToList();

            if (listadoEquipo.Count == 0)
            {
                return NotFound();
            }

            return Ok(listadoEquipo);
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult GuardarEquipo([FromBody] calificaciones calificacion)
        {
            try
            {
                _blogDBcontexto.calificaciones.Add(calificacion);
                _blogDBcontexto.SaveChanges();
                return Ok(calificacion);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult ActualizarEquipo(int id, [FromBody] calificaciones calificacionModificar)
        {
            // Obtener el registro original de la base de datos
            calificaciones? calificacionActual = (from c in _blogDBcontexto.calificaciones
                                                  where c.CalificacionId == id
                                                  select c).FirstOrDefault();

            // Verificamos que exista el registro según su ID
            if (calificacionActual == null)
            {
                return NotFound();
            }

            // Si se encuentra el registro, se alteran los campos modificables
            calificacionActual.Calificacion = calificacionModificar.Calificacion;


            // Se marca el registro como modificado en el contexto y se envía la modificación a la base de datos
            _blogDBcontexto.Entry(calificacionActual).State = EntityState.Modified;
            _blogDBcontexto.SaveChanges();

            return Ok(calificacionModificar);
        }

        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult EliminarEquipo(int id)
        {
            // Obtener el registro original de la base de datos
            calificaciones? equipo = (from c in _blogDBcontexto.calificaciones
                                      where c.CalificacionId == id
                                      select c).FirstOrDefault();

            // Verificamos que exista el registro según su ID
            if (equipo == null)
            {
                return NotFound();
            }

            // Ejecutamos la acción de eliminar el registro
            _blogDBcontexto.calificaciones.Remove(equipo);
            _blogDBcontexto.SaveChanges();

            return Ok(equipo);
        }


        [HttpGet]
        [Route("GetByPublicacion/{publicacionId}")]
        public IActionResult GetByPublicacion(int publicacionId)
        {
            var calificaciones = _blogDBcontexto.calificaciones.Where(c => c.PublicacionId == publicacionId).ToList();

            if (calificaciones.Count == 0)
            {
                return NotFound();
            }

            return Ok(calificaciones);
        }

    }
}
