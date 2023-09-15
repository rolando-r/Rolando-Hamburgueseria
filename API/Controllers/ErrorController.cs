using API.Helpers.Errors;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("errors/{code}")]
public class ErrorsController:BaseApiController
{
    public IActionResult Error(int code)
    {
        return new ObjectResult(new ApiResponse(code));
    }
}