using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApiVersionControl.Controllers.v2
{
    [ApiController]
    public class ValuesController : Controller
    {
        [HttpPost]
        [HttpGet]
        [ApiVersion("2.0")]
        [Route("api/[controller]/[action]")]
        public string VersionTest()
        {
            return "Version2.0";
        }

        [HttpPost]
        [HttpGet]
        [ApiVersion("2.0")]
        [Route("api/v{version:apiVersion}/[controller]/[action]")]
        public string VersionTestTwo()
        {
            return "Version2.0";
        }
    }
}