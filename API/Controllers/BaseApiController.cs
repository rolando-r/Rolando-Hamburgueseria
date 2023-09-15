using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiController]
// Ruta para manejar los controladores
[Route("api/hamburguesas/[controller]")]
public class BaseApiController : ControllerBase
{

}
