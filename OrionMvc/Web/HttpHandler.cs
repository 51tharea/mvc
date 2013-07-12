using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.SessionState;

namespace OrionMvc.Web
{


    public class HttpHandler : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext con)
        {
            
            Application.Instance.Router.Dispatch(con);
        }

       
        public bool IsReusable
        {
            get { return false; }
        }
    }
}
