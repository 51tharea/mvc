using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OrionMvc.Web;

namespace MvcTest.Application.Home
{
    public class Home:Controller
    {

        public void Index()
        {
            this["content"] = "Test Home Controller";
            
        }
    }
}