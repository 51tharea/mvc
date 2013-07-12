using System;
using System.Collections.Generic;
using System.Web;

namespace OrionMvc.Web
{
    public class OrionHttpModule : IHttpModule
    {
        public void Dispose()
        {

        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += new EventHandler(BeginRequest);
            context.PreRequestHandlerExecute += new EventHandler(PreRequestHandlerExecute);
            context.EndRequest += new EventHandler(EndRequest);
            context.AuthorizeRequest += new EventHandler(AuthorizeRequest);
        }




        void BeginRequest(object sender, EventArgs e)
        {
            //We received a request, so we save the original URL here
            HttpContext context = ((HttpApplication)sender).Context;
            string path = context.Request.Url.PathAndQuery ?? "/";
            
        }

        void EndRequest(object sender, EventArgs e)
        {
            //We processed the request
        }

        void AuthorizeRequest(object sender, EventArgs e)
        {
            //We change uri for invoking correct handler
            HttpContext context = ((HttpApplication)sender).Context;

            if (context.Request.RawUrl.Contains(".bspx"))
            {
                string url = context.Request.RawUrl.Replace(".bspx", ".aspx");
                context.RewritePath(url);
            }
        }

        void PreRequestHandlerExecute(object sender, EventArgs e)
        {
            //We set back the original url on browser
            HttpContext context = ((HttpApplication)sender).Context;

            if (context.Items["originalUrl"] != null)
            {
                context.RewritePath((string)context.Items["originalUrl"]);
            }
        }

    }
}
