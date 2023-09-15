using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Helpers.Errors;

public class ApiResponse
{
    public int StatusCode { get; set; }
    public string Message { get; set; }

    public ApiResponse(int statusCode, string message = null)
    {
        StatusCode = statusCode;
        Message = message ?? GetDefaultMessage(statusCode);
    }

    private string GetDefaultMessage(int statusCode)
    {
        return statusCode switch
        {
            400 => "Has realizado una petición incorrecta.",
            401 => "Usuario no autorizado.",
            404 => "El recurso que has intentado solicitar no existe.",
            405 => "Este método HTTP no está permitido en el servidor.",
            500 => "Error en el servidor. No eres tú, soy yo. Comunícate con el administrador XD.",
            _ => throw new NotImplementedException()
        };
    }
}
