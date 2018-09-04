using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.AspNetCore.Mvc;
using RequestsLogger.Repository;

namespace RequestLogger.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly IRequestsRepository _requestsRepository;

        public NotificationController(IRequestsRepository requestsRepository)
        {
            _requestsRepository = requestsRepository;
        }

        [HttpGet]
        public IActionResult Ping()
        {
            return StatusCode((int)HttpStatusCode.Accepted);
        }

        [HttpPost]
        public async Task<IActionResult> ProcessNotificationAsync()
        {
            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                var content = await reader.ReadToEndAsync();
                await _requestsRepository.InsertAsync(content);
            }
            
            return StatusCode((int)HttpStatusCode.Accepted);
        }
    }
}
