using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using OrionMvc;
using OrionMvc.Web;

namespace MvcTest
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

          
            //Router routers = new Router();
            var App = OrionMvc.Application.GetInstance();

            App.Router.Connect("", new
            {
                controller = "Home",
                action = "Index"
            }); ;

            //Router.Connect("", new
            //{
            //    controller = "Home",
            //    action = "Index"
            //});

            //Router.Connect("deneme", new { 
            //    controller = "Home",
            //    action = "Deneme"
            //});
            
            //App.Start();
        }

       
    }
}