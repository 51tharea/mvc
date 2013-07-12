using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace OrionMvc.Web
{
    // public static Dictionary<string, Route> RouterData = new Dictionary<string, Route>();
    public interface IRoute:IDictionary<string,IRoute>
    {
        object Defaults
        {
            set;
            get;
        }

        object Rule
        {
            set;
            get;
        }

        string Path
        {
            set;
            get;
        }

        string Expression(Match expression);

        //Dictionary<string, string> RulesParams(object values);

        string GetFormat();

        string GetPath();

        object GetDefault();

    }
}
