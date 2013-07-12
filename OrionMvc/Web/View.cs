using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using OrionMvc;
using System.IO;
using System.Web.Compilation;
using System.Web;
namespace OrionMvc.Web
{
    public class View : Page, IView
    {


        public void Render(IController controller, string path)
        {
            //ViewData = new ViewData();

            ViewData = controller.ViewData;

            try
            {
                var html = String.Empty;
                

                string _path = string.Format("~/View/{0}/{1}.aspx", controller.Name, path);
                //var writer = new StringWriter();
                //controller.Context.Server.Execute(_path, controller.Context.Response.Output, true);
                //BuildManager.CreateInstanceFromVirtualPath(_path, typeof(System.Web.UI.Page));
               // controller.Context.RewritePath(_path,false);
                //var page = BuildManager.CreateInstanceFromVirtualPath(_path, typeof(Page)) as IHttpHandler;

                //PageParser.GetCompiledPageInstance(_path, controller.Context.Server.MapPath(_path), controller.Context);//.ProcessRequest(controller.Context);
                //html = writer.ToString();
                Context.Response.Clear();
                using (HtmlTextWriter htmlw = new HtmlTextWriter(Context.Response.Output))
                {

                   Context.Server.Execute(_path, htmlw, true);
                
                }
                Context.Response.End();
            
            }
            catch (System.Exception e)
            {
                controller.Context.Response.Write(e.StackTrace);
                controller.Context.Response.Write(e.ToString());
            }
        }


        public string GetPathView(IController controller, string path)
        {
            string _path = "";
            return _path;
        }


        public IViewData ViewData
        {
            get;

            set;

        }
    }
}
