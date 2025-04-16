using Microsoft.EntityFrameworkCore;
using ms_cliente.entity; // Asegúrate de tener este namespace

namespace ms_cliente.data
{
    public class ClienteDbContext : DbContext
    {
        public ClienteDbContext(DbContextOptions<ClienteDbContext> options) : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
    }
}