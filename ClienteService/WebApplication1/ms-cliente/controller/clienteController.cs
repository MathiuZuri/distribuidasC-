using Microsoft.AspNetCore.Mvc;
using ms_cliente.entity;
using ms_cliente.service; // Asegúrate de tener este namespace

namespace ms_cliente.controller
{
    [ApiController]
    [Route("api/clientes")] // Define la ruta base para este controlador
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _iclienteService;

        public ClienteController(IClienteService iclienteService)
        {
            _iclienteService = iclienteService;
        }

        [HttpGet]
        public IActionResult Listar()
        {
            var clientes = _iclienteService.Listar();
            return Ok(clientes);
        }

        [HttpPost]
        public IActionResult Guardar([FromBody] Cliente cliente)
        {
            var clienteGuardado = _iclienteService.Guardar(cliente);
            return Ok(clienteGuardado);
        }

        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            var cliente = _iclienteService.BuscarPorId(id);
            if (cliente == null)
            {
                return NotFound(); // Retorna 404 si el cliente no se encuentra
            }
            return Ok(cliente);
        }

        [HttpPut]
        public IActionResult Actualizar([FromBody] Cliente cliente)
        {
            var clienteActualizado = _iclienteService.Actualizar(cliente);
            return Ok(clienteActualizado);
        }

        [HttpDelete("{id}")]
        public IActionResult Eliminar(int id)
        {
            _iclienteService.Eliminar(id);
            var clientesActualizados = _iclienteService.Listar(); // Opcional: Retorna la lista actualizada
            return Ok(clientesActualizados);
        }
    }
}