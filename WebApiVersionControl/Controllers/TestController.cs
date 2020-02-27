using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiVersionControl.Model;

namespace WebApiVersionControl.Controllers
{
    public class TestController : Controller
    {
        [Route("api/test/action")]
        public TestResult TestAction(TestArgu testArgu)
        {
            return new TestResult() { name = testArgu.price.ToString() };
        }
    }
}
