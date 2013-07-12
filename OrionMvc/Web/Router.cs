using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Reflection;

namespace OrionMvc.Web
{
    public class Router : Dictionary<string, object>, IRouter
    {
        public static Dictionary<string, IRoute> RouterData = new Dictionary<string, IRoute>();
       
        public  RouteMeta Meta
        {
            get;

            set;
        }

        IRoute _route
        {
            set;
            get;
        }

        private static List<IRoute> _RouterCollection = new List<IRoute>();
        public static List<IRoute> RouterCollection
        {
            get { return _RouterCollection; }
            set { _RouterCollection = value; }
        }


        public void Dispatch(HttpContext context)
        {
            var App = Application.Instance;
            var Path = context.Request.Path;
            App.Router.Match(Path.TrimEnd('/'));
            
            var routeMeta = App.Router.Meta;
            //string actionResult = null;
            var _context = routeMeta.Route;
            string actionResult = null;

            IController controller = App.ControllerFactory.CreateController(context, routeMeta);
            controller.Execute(context, routeMeta);
            //controller.Render(context, routeMeta);
            //context.Response.Write(actionResult);
        }



        //public static Route Connect(string path, object _default)
        //{
        //    Route router = new Route(path, _default);
        //    AddPath(router);
        //    return router;
        //}

        //public static Route Connect(string path, object _default, object rule)
        //{
        //    Route router = new Route(path, _default, rule);
        //    AddPath(router);
        //    return router;
        //}

        public void Connect(string path, object _default)
        {
            Route router = new Route(path, _default);
            AddPath(router);
         
        }

        public void Connect(string path, object _default, object rule)
        {
            Route router = new Route(path, _default, rule);
            AddPath(router);
        }

        private static void AddPath(Route router)
        {
            RouterCollection.Add(router);
        }


        public RouteMeta ToDefault(object values)
        {
            if (values != null)
            {

                foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(values))
                {
                    object val = descriptor.GetValue(values);
                    Meta[descriptor.Name] = val.ToString();

                }
            }
            return Meta;
        }


        public void Match(string url)
        {
            Meta = new RouteMeta
            {
                Route = _route
            };
            Match ignoreRoute = Regex.Match(url, "(.*/)?favicon.ico(/.*)?");
            if (!ignoreRoute.Success)
            {
                foreach (var route in RouterCollection)
                {
                    Regex expression = new Regex(route.GetFormat());


                    if (expression.IsMatch(url))
                    {
                        Meta = ToDefault(route.GetDefault());
                        Meta.Route = route;
                        Meta.Router = this;

                        var Matches = expression.Match(url);

                        if (Matches.Success)
                        {
                            foreach (string key in expression.GetGroupNames())
                            {
                                int isInt;
                                if (Int32.TryParse(key, out isInt) == false)
                                {
                                    if (Matches.Groups[key].ToString() != "")
                                    {
                                        Meta[key] = Matches.Groups[key].ToString();
                                    }
                                }
                            }

                        }

                        break;
                    }
                }
            }

        }



       
    }
}

/* foreach (KeyValuePair<string, IRoute> route in RouterData)
 {
     Regex expression = new Regex(route.Value.GetFormat());


     if (expression.IsMatch(url))
     {
         Meta = ToDefault(route.Value.GetDefault());
         //RouterTable = (Dictionary<string, string>)toDefault(route.Value.GetDefault());
         Meta.Route = route.Value;

         var Matches = expression.Match(url);

         if (Matches != null)
         {
             foreach (string key in expression.GetGroupNames())
             {
                 int isInt;
                 if (Int32.TryParse(key, out isInt) == false)
                 {
                     if (Matches.Groups[key].ToString() != "")
                     {
                         Meta[key] = Matches.Groups[key].ToString();
                     }
                 }
             }

         }
                   
         break;
     }
 }*/