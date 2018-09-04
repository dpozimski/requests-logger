using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
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
        public IActionResult Ping(HttpRequestMessage request)
        {
            return StatusCode((int)HttpStatusCode.Accepted);
        }

        [HttpPost]
        public async Task<IActionResult> ProcessNotificationAsync(HttpRequestMessage request)
        {
            var content = await request.Content.ReadAsStringAsync();

            await _requestsRepository.InsertAsync(content);

            return StatusCode((int)HttpStatusCode.Accepted);
        }
    }
}
