using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OrionMvc.Web
{
    public class ViewData:Dictionary<string, object>,IViewData
    {
        public ViewData()
        {
        }

        new public object this[string key]
        {
            get
            {
                if (this.ContainsKey(key))
                    return base[key];

                throw new Exception(key);
            }
            set
            {
                base[key] = value;
            }
        }
    }
}
