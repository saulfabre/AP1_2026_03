using Microsoft.AspNetCore.Mvc;
using RegistroPrestamos.Data;
using RegistroPrestamos.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RegistroPrestamos.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibrosController(LibrosServices librosService) : ControllerBase
    {
        // GET: api/<LibrosController>
        [HttpGet]
        public async Task <List<Libro>> Get()
        {
            var librosResult = await librosService.GetList(l => true);
            return librosResult.Value;
        }

        // GET api/<LibrosController>/5
        [HttpGet("{libroId}")]
        public async Task<IActionResult?> Get(int libroId)
        {
            var libro = await librosService.Buscar(libroId);
            return Ok(libro.Value);
        }

        // POST api/<LibrosController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Libro libro)
        {
            var libroResult = await librosService.Guardar(libro);
            return Created();
        }

        // PUT api/<LibrosController>/5
        [HttpPut("{libroId}")]
        public async Task<IActionResult> Put(int libroId, [FromBody] Libro libro)
        {
            var libroResult = await librosService.Modificar(libro);
            return Ok(libro);
        }

        // DELETE api/<LibrosController>/5
        [HttpDelete("{libroId}")]
        public async Task<IActionResult> Delete(int libroId)
        {
            var eliminado = await librosService.Eliminar(libroId);
            return NoContent();
        }
    }
}
