using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Compilation;
using System.Web.UI;

namespace OrionMvc.Web
{
    public class View : Page, IView
    {
        private static dynamic viewBag;

        public View() { }

        public View(string viewname=null)
        {


        }

        public string Render(HttpContext context, IController controller, string path)
        {
            viewBag = controller.ViewBag;

            string _path = string.Format("~/View/{0}/{1}.aspx", controller.Name, path);

            var objContentPage = BuildManager.CreateInstanceFromVirtualPath(_path.ToString(), typeof(Page)) as View;

            StringWriter sw = new StringWriter();

            objContentPage.AppRelativeVirtualPath = _path.ToString();

            context.Server.Execute(objContentPage, sw, false);

            return sw.GetStringBuilder().ToString();

        }

        public object PartialRender(HttpContext context, IController controller, string path)
        {
            viewBag = controller.ViewBag;

            string _path = string.Format("~/View/{0}/{1}.aspx", controller.Name, path);

            var objContentPage = BuildManager.CreateInstanceFromVirtualPath(_path.ToString(), typeof(Page)) as View;

            StringWriter sw = new StringWriter();

            objContentPage.AppRelativeVirtualPath = _path.ToString();

            context.Server.Execute(objContentPage, sw, false);


            sw.GetStringBuilder().ToString();

             return viewBag;
        }



        public string GetPathView(IController controller, string path)
        {
            var _path = string.Empty;
            return _path;
        }


        public static dynamic ViewBag
        {

            get { return viewBag; }
        }
    }
}
