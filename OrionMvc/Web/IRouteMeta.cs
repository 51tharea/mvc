using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OrionMvc.Web
{
    public interface IRouteMeta:IDictionary<string, object>
    {
        IRoute Route
        {
            get;
            set;
        }

        IRouter Router
        {
            get;
            set;
        }

        string Controller
        {
            get;
            set;
        }

        string Action
        {
            get;
            set;
        }

        string Rule
        {
            get;
            set;
        }
    }
}
