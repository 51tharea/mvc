namespace OrionMvc.Web
{
    using System;
    using System.Collections.Generic;

    public interface IView
    {
        void  Render(IController controller, string path);

        //string Render(IController controller, string path);

        //IViewData ViewData { set; get; }
    }
}
