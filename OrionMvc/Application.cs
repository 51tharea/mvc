using System;
using System.Collections.Generic;
using System.Web;
using OrionMvc.Web;
using System.Text.RegularExpressions;

namespace OrionMvc
{
    public class Application
    {

        public Application()
        {
        }
        public IControllerFactory ControllerFactory
        {
            get;
            set;
        }

        static public Application Instance
        {
            get;
            set;
        }

        public IRouter Router
        {
            get;
            set;
        }

        public IView View
        {
            get;
            set;
        }


        public static Application GetInstance()
        {
            if (Instance == null)
            {
                Instance = new Application();
                Instance.Initialize();
            }
            return Instance;
        }

        #region private void Initialize()
        /// <summary>
        /// 
        /// </summary>
        public virtual void Initialize()
        {
            ControllerFactory = new ControllerFactory();
            Router = new Router();
            View = new View();
        }
        #endregion


    }
}
