using Dapr;
using DparContainer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace MyBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PubSubController : ControllerBase
    {
        [Topic("messages-pub-sub", "messages")]
        [HttpPost("/messages")]
        public IActionResult Messages(CloudEvent<EventModel> cloudEvent)
        {
            return Ok(cloudEvent.Data);
        }
    }
}
