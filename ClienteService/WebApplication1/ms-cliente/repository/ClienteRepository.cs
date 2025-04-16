using System.Collections.Generic;
using System.Linq;
using ms_cliente.entity;
using ms_cliente.data; // Asegúrate de tener este namespace

namespace ms_cliente.repository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly ClienteDbContext _context;

        public ClienteRepository(ClienteDbContext context)
        {
            _context = context;
        }

        public List<Cliente> Listar()
        {
            return _context.Clientes.ToList();
        }

        public Cliente Guardar(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            _context.SaveChanges();
            return cliente;
        }

        public Cliente BuscarPorId(int id)
        {
            return _context.Clientes.Find(id);
        }

        public Cliente Actualizar(Cliente cliente)
        {
            _context.Clientes.Update(cliente);
            _context.SaveChanges();
            return cliente;
        }

        public void Eliminar(int id)
        {
            var cliente = _context.Clientes.Find(id);
            if (cliente != null)
            {
                _context.Clientes.Remove(cliente);
                _context.SaveChanges();
            }
        }
    }
}