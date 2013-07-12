
namespace OrionMvc.Web
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Web;

    public interface IView
    {
        void Render(IController controller, string path);
        //string Render(IController controller, string path);
       // void Render(HttpContext context, IController controller, string path);

        IViewData ViewData
        {
            set;
            get;
        }
    }
}
