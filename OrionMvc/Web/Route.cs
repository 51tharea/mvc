using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.ComponentModel;

namespace OrionMvc.Web
{
    public class Route :Dictionary<string, IRoute>, IRoute
    {
        #region public object Defaults
        /// <summary>
        /// Get/Sets the Defaults of the Route
        /// </summary>
        /// <value></value>
        public object Defaults
        {
            get;
            set;
        }
        #endregion

        #region public string Path
        /// <summary>
        /// Get/Sets the Path of the Route
        /// </summary>
        /// <value></value>
        public string Path
        {
            get;
            set;
        }
        #endregion

        #region public object Rule
        /// <summary>
        /// Get/Sets the _Rule of the Route
        /// </summary>
        /// <value></value>
        public object Rule
        {
            get;
            set;
        }
        #endregion

        public string Controller
        {
            get;
            set;
        }

        public string Action
        {
            get;
            set;
        }

        #region public Route(string path, object defaults)
        /// <summary>
        /// Initializes a new instance of the <b>Route</b> class.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="defaults"></param>
        public Route(string path, object defaults)
        {

            Path = path;
            Defaults = defaults;

        }
        #endregion

        #region public Route(string path, object defaults, object rule)
        /// <summary>
        /// Initializes a new instance of the <b>Route</b> class.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="defaults"></param>
        /// <param name="rule"></param>
        public Route(string path, object defaults, object rule)
        {

            Path = path;
            Defaults = defaults;
            Rule = rule;
        }
        #endregion

        #region public string GetPath()
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetPath()
        {
            return Path;
        }
        #endregion

        #region public object GetDefault()
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public object GetDefault()
        {
            return Defaults;
        }
        #endregion

        #region public string GetFormat()
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetFormat()
        {
            string Expression = Regex.Replace(Path, @"[.\\+*?[^\\]${}=!|]", @"\\\\$0");

            if (Convert.ToBoolean(Expression.LastIndexOf("(")) != false)
            {
                Expression = Expression.Replace("(", "(?:").Replace(")", ")?");

            }

            Expression = Regex.Replace(Expression, @":([\w]+)", new MatchEvaluator(this.Expression));

            Expression = "(^" + Expression + "$)";
            return Expression;
        }
        #endregion

        #region public string Expression(Match expression)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public string Expression(Match expression)
        {
            string parts = "";
            foreach (var rule in RulesParams(Rule))
            {

                if (rule.Key == expression.Groups[1].ToString())
                {
                    parts = string.Format(@"(?<{0}>{1})", expression.Groups[1], rule.Value);
                    return parts;
                }

            }
            return string.Format(@"(?<{0}>{1})", expression.Groups[1], @"([^\/]+)");
           // return "(?<" + expression.Groups[1] + ">" + @"([^\/]+)" + ")";
        }
        #endregion

        #region Dictionary<string, string> RulesParams(object values)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        Dictionary<string, string> RulesParams(object values)
        {
            Dictionary<string, string> param = new Dictionary<string, string>();
            if (values != null)
            {

                foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(values))
                {
                    object val = descriptor.GetValue(values);
                    param[descriptor.Name] = val.ToString();
                }
            }
            return param;
        }
        #endregion
        
    }
}
