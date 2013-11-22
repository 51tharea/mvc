using System;
using System.Collections.Generic;
using System.Linq;
using OrionMvc.Web;

namespace MvcTest.Application.Home
{
    public class Home : Controller
    {
        public void Index()
        {

            ViewBag.Content = "TestHome";
            ViewBag["deneme"] = "dfgdfg";
            this["Title"] = " Bu bir Title";
        }
    }
}
