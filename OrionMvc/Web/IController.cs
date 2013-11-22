using System;
using System.Web;

namespace OrionMvc.Web
{
    public interface IController
    {
        void Execute(HttpContext context, RouteMeta routeData);

        void Render(HttpContext context, string View);

        //string Render(HttpContext context, string View);

        void Execute(HttpContextBase context, RouteMeta routeData);

        void Render(HttpContextBase context, string View);

        HttpContext Context
        {
            get;
            set;
        }

        ViewData ViewData
        {
            get;
            set;
        }
        new dynamic ViewBag
        {
            set;
            get;
        }
        
        string Name
        {
            get;
            set;
        }

        HttpRequest Request { get; set; }
    }
}
