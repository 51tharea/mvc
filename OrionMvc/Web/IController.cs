using System;
using System.Web;

namespace OrionMvc.Web
{
    public interface IController
    {

        //string Execute(HttpContext context, RouteMeta routeMeta);
        void Execute(HttpContext context, RouteMeta routeData);

        //void Render(string View);
        void Render(HttpContext context,string View);

        HttpContext Context
        {
            get;
            set;
        }

        IViewData ViewData
        {
            get;
            set;
        }
        string Name
        {
            get;
            set;
        }

        HttpRequest Request { get; set; }
    }
}
