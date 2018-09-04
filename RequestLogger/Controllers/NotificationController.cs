using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RequestsLogger.Repository;

namespace RequestLogger.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly IRequestsRepository _requestsRepository;
        private readonly IHttpContextAccessor _accessor;

        public NotificationController(IRequestsRepository requestsRepository,
                                      IHttpContextAccessor accessor)
        {
            _requestsRepository = requestsRepository;
            _accessor = accessor;
        }

        [HttpGet]
        public IActionResult Ping()
        {
            return StatusCode((int)HttpStatusCode.Accepted);
        }

        [HttpPost]
        public async Task<IActionResult> ProcessNotificationAsync()
        {
            var clientIp = _accessor.HttpContext.Connection.RemoteIpAddress.ToString();

            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                var content = await reader.ReadToEndAsync();
                await _requestsRepository.InsertAsync(content, clientIp);
            }
            
            return StatusCode((int)HttpStatusCode.Accepted);
        }
    }
}
