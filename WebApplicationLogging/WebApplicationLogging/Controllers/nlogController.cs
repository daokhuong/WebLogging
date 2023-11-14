﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplicationLogging.Loggings;

namespace WebApplicationLogging.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class nlogController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        public nlogController(ILoggerManager logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public IEnumerable<string> Get()
        {
            _logger.LogInfo("Here is info message from the controller.");
            _logger.LogDebug("Here is debug message from the controller.");
            _logger.LogWarn("Here is warn message from the controller.");
            _logger.LogError("Here is error message from the controller.");
            return new string[] { "value1", "value2" };
        }
    }
}