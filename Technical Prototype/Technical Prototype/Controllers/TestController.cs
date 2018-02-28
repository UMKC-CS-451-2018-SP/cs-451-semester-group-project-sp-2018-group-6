using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Technical_Prototype.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public string Index()
        {
            return "This is my <b>default</b> action...";
        }

        // 
        // GET: /Test/Welcome/ 

        public string Welcome(string name, int numTimes = 1)
        {
            return HttpUtility.HtmlEncode("Hello " + name + ", NumTimes is: " + numTimes);
        }
    }
}