using Microsoft.AspNetCore.Mvc;
using RegistroPrestamos.Data;
using RegistroPrestamos.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RegistroPrestamos.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrestamosController(PrestamosServices prestamosServices) : ControllerBase
    {
        // GET: api/<PrestamosController>
        [HttpGet]
        public async Task<List<Prestamo>> Get()
        {
            var prestamosResult = await prestamosServices.GetList(p => true);
            return prestamosResult.Value;
        }

        // GET api/<PrestamosController>/5
        [HttpGet("{prestamoId}")]
        public async Task<IActionResult?> Get(int prestamoId)
        {
            var prestamo = await prestamosServices.Buscar(prestamoId);
            return Ok(prestamo.Value);
        }

        // POST api/<PrestamosController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Prestamo prestamo)
        {
            var prestamoResult = await prestamosServices.Guardar(prestamo);
            return Created();
        }

        // PUT api/<PrestamosController>/5
        [HttpPut("{prestamoId}")]
        public async Task<IActionResult> Put(int prestamoId, [FromBody] Prestamo prestamo)
        {
            var prestamoResult = await prestamosServices.Modificar(prestamo);
            return Ok(prestamoResult);
        }

        // DELETE api/<PrestamosController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int prestamoId)
        {
            var eliminado = await prestamosServices.Eliminar(prestamoId);
            return NoContent();
        }
    }
}
