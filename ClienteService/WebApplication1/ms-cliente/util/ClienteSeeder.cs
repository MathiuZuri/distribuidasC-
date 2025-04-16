using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ms_cliente.data; // Asegúrate de tener este namespace
using ms_cliente.entity;

namespace ms_cliente.util
{
    public class ClienteSeeder : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public ClienteSeeder(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ClienteDbContext>();

                // Verificamos si ya existen datos para no duplicar
                if (!context.Clientes.Any())
                {
                    var cliente1 = new Cliente(1,"Juan Pérez", "12345678A", "Calle Principal 123", new DateTime(1990, 1, 1));
                    var cliente2 = new Cliente(2, "María García", "98765432B", "Avenida Secundaria 456", new DateTime(1985, 5, 15));
                    var cliente3 = new Cliente(3, "Carlos López", "56789012C", "Plaza Central 789", new DateTime(2000, 12, 10));
                    // Agrega más clientes aquí

                    context.Clientes.AddRange(cliente1, cliente2, cliente3);
                    await context.SaveChangesAsync(cancellationToken);

                    Console.WriteLine("Datos de clientes insertados correctamente.");
                }
                else
                {
                    Console.WriteLine("Los clientes ya existen, no se insertaron datos.");
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}