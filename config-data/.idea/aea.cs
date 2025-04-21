namespace DefaultNamespace;

public class aea
{
    {
        "Logging": {
            "LogLevel": {
                "Default": "Information",
                "Microsoft.AspNetCore": "Warning"
            }
        },
        "AllowedHosts": "*",
        "ConnectionStrings": {
            "DefaultConnection": "Server=localhost;Port=3306;Database=ms_cliente_db;Uid=root;Pwd=;"
        },
        "Swagger": {
            "Enabled": true,
            "RoutePrefix": "swagger"
        },
        "ServiceSettings": {
            "ServiceName": "ms-cliente-service"
        }
    }

}