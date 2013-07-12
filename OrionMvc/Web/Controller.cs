using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Reflection;
using System.Linq;
using System.Text.RegularExpressions;

namespace OrionMvc.Web
{
    public class Controller : IController//,Dictionary<string,object>
    {
        string _responseContent = null;
        string _name = null;
        Dictionary<string, MethodInfo> _actions;
        Dictionary<string, string> _actionLayouts;


        public Controller()
        {
            ViewData = new ViewData();
            _actions = GetActionsList();
            _actionLayouts = new Dictionary<string, string>();
            
        }

        public HttpContext Context
        {
            get;
            set;
        }

        public IViewData ViewData
        {
            get;
            set;
        }


        public HttpRequest Request
        {
            get;
            set;
        }

        public object this[string key]
        {
            get
            {

                return ViewData[key];
            }
            set
            {

                ViewData[key] = value;
            }
        }

        public void Render(HttpContext context,string View)
        {
            Application.Instance.View.Render(this, View);

        }

        //public void Execute(HttpContext context, RouteMeta routeData)
        //{
        //    String actionName = routeData.Action;
        //    MethodInfo action = FindAction(actionName);

        //    if (actionName == null || action == null)
        //        return null;

        //    actionName = action.Name;

        //    InvokeAction(action);


        //    string html = "";

        //    foreach (KeyValuePair<string, object> listOfparams in this.ViewData)
        //    {
        //        html += "Params" + listOfparams.Key + "----" + "value" + listOfparams.Value;
        //    }
        //    Render(context,actionName);
        //    return html.ToString() ?? "";
        //}


        public void Execute(HttpContext context, RouteMeta routeData)
        {
            String actionName = routeData.Action;
            MethodInfo action = FindAction(actionName);

            //if (actionName == null || action == null)
            //    return null;

            actionName = action.Name;

            InvokeAction(action);


            string html = "";

            foreach (KeyValuePair<string, object> listOfparams in this.ViewData)
            {
                html += "Params" + listOfparams.Key + "----" + "value" + listOfparams.Value;
            }
            Render(context, actionName);
           
            //return html.ToString() ?? "";
        }

        public MethodInfo FindAction(string name)
        {
            name = name.ToLower();
            return _actions.ContainsKey(name) ? _actions[name] : null;
        }

       
        public bool InvokeAction(MethodInfo action)
        {
            action.Invoke(this, new object[] { });
            return true;
        }

   
        public Dictionary<string, MethodInfo> GetActionsList()
        {
            BindingFlags flags = BindingFlags.Default | BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy;

            MethodInfo[] allMethods = GetType().GetMethods(flags);
            var baseMethods = from method in typeof(Controller).GetMethods(flags) select method.Name;
            var actionMethods = from method in allMethods
                                where baseMethods.Contains(method.Name) == false && method.MemberType == MemberTypes.Method
                                select method;

            return actionMethods.ToDictionary(m => m.Name.ToLower());
        }

        public string Name
        {
            get
            {
                if (_name == null)
                {
                    _name = this.GetType().Name;
                    //_name = Regex.Replace(_name, string.Format("{0}$", Application.Instance.), "");
                }

                return _name;
            }
            set
            {
                _name = value;
            }
        }




    }
}
