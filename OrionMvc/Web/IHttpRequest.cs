using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using AspHttpContext = System.Web.HttpContext;
namespace OrionMvc.Web
{
    public interface IHttpRequest
    {
        string FullPath
        {
            get;
            set;
        }

        string Path
        {
            get;
            set;
        }

        //ParamsCollection Params
        //{
        //    get;
        //    set;
        //}

        //String Wants
        //{
        //    get;
        //    set;
        //}

        //IHttpRequestHeaders Headers
        //{
        //    get;
        //    set;
        //}

        void Init(HttpContext mvcContext, AspHttpContext aspContext);
    }
}
