using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApiVersionControl.Controllers.v1
{
    [ApiController]
    public class ValuesController : Controller
    {
        [HttpPost]
        [HttpGet]
        [ApiVersion("1.0")]
        [Route("api/[controller]/[action]")]
        public string VersionTest()
        {
            return "Version1.0";
        }

        [HttpPost]
        [HttpGet]
        [ApiVersion("1.0")]
        [Route("api/v{version:apiVersion}/[controller]/[action]")]
        public string VersionTestTwo()
        {
            return "Version1.0";
        }

        
    }
}