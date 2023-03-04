using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Mvc;
using Dapr.Client;
using Dapr;

namespace MyBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PubSubController : ControllerBase
    {
        [Topic("messages-pub-sub", "messages")]
        [HttpPost("/messages")]
        public IActionResult Messages([FromServices] DaprClient  daprClient)
        {
            return Ok();
        }
    }
}
