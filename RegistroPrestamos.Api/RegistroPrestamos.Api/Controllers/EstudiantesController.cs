using Microsoft.AspNetCore.Mvc;
using RegistroPrestamos.Data;
using RegistroPrestamos.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RegistroPrestamos.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstudiantesController(EstudiantesServices estudiantesService) : ControllerBase
    {
        // GET: api/<EstudiantesController>
        [HttpGet]
        public async Task <List<Estudiante>> Get()
        {
            var estudiantesResult = await estudiantesService.GetList(l => true);
            return estudiantesResult.Value;
        }

        // GET api/<EstudiantesController>/5
        [HttpGet("{estudianteId}")]
        public async Task<IActionResult?> Get(int estudianteId)
        {
            var estudiante = await estudiantesService.Buscar(estudianteId);
            return Ok(estudiante.Value);
        }

        // POST api/<EstudiantesController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Estudiante estudiante)
        {
            var estudianteResult = await estudiantesService.Guardar(estudiante);
            return Created();
        }

        // PUT api/<EstudiantesController>/5
        [HttpPut("{estudianteId}")]
        public async Task<IActionResult> Put(int estudianteId, [FromBody] Estudiante estudiante)
        {
            var estudianteResult = await estudiantesService.Modificar(estudiante);
            return Ok(estudiante);
        }

        // DELETE api/<EstudiantesController>/5
        [HttpDelete("{estudianteId}")]
        public async Task<IActionResult> Delete(int estudianteId)
        {
            var eliminado = await estudiantesService.Eliminar(estudianteId);
            return NoContent();
        }
    }
}
