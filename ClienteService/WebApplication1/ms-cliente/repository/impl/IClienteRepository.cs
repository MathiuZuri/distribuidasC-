using System.Collections.Generic;
using ms_cliente.entity;

namespace ms_cliente.repository
{
    public interface IClienteRepository
    {
        List<Cliente> Listar();
        Cliente Guardar(Cliente cliente);
        Cliente BuscarPorId(int id);
        Cliente Actualizar(Cliente cliente);
        void Eliminar(int id);
    }
}