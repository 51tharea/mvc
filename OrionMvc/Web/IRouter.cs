using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Text.RegularExpressions;

namespace OrionMvc.Web
{
    public interface IRouter:IDictionary<string,object>
    {

        //static Route Connect(string path, object _default);
        //static Route Connect(string path, object _default, object rule);

        void Connect(string path, object _default);
        void Connect(string path, object _default, object rule);
        void Dispatch(HttpContext context);

        void Match(string url);

        RouteMeta Meta
        {
            set;
            get;
        }



       
       
    }
}
