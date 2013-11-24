using OrionMvc.Web;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MvcTest
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            var App = OrionMvc.Application.GetInstance();
            AppInstance(App.Router);

           
            
        }
        private void AppInstance(IRouter router)
        {
            router.Connect(string.Empty, new
            {
                controller = "Home",
                action = "Index"
            });
        }
    }
}
