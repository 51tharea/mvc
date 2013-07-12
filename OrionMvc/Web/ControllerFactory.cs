using System;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using System.Web;
using System.Text;
using System.Linq;

namespace OrionMvc.Web
{
    public class ControllerFactory : IControllerFactory
    {
        //private static Dictionary<string, Type> types;
        private static readonly object GeneralLock = new object();
        private static Type types = null;

        public IController CreateController(HttpContext context, RouteMeta meta)
        {

            string ControllerNmae = meta.Controller;


            if (meta.Controller == null)
                return null;

            Type controllerType = GetController(ControllerNmae);  // GetController(meta.Controller);


            IController result = (IController)Activator.CreateInstance(controllerType);
            
            result.Context = context;
            result.Request = context.Request;

            return result;
        }

        protected internal virtual Type GetController(string controller)
        {
            if (types == null)
            {
                lock (GeneralLock)
                {
                    if (types == null)
                    {

                        types = AppDomain.CurrentDomain.GetAssemblies()
                                  .SelectMany(a => a.GetTypes())
                                  .FirstOrDefault(t => t.Name == controller);
                    }
                }
            }
            return types;
        }




        
    }
}

   //public virtual Type _GetController(string typeName)
   //     {
   //         if (types == null)
   //         {
   //             lock (GeneralLock)
   //             {
   //                 if (types == null)
   //                 {
   //                     //types = new Dictionary<string, Type>();
   //                     var appAssemblies = AppDomain.CurrentDomain.GetAssemblies();
   //                     foreach (var appAssembly in appAssemblies)
   //                     {
   //                         foreach (var type in appAssembly.GetTypes())
   //                             if (!types.(type.Name))
   //                                 types.Add(type.Name, type);
   //                     }
   //                 }
   //             }
   //         }

   //         return types[typeName];
   //     }

/*public virtual Type GetController(string controller)
{
  var assembly = new List<Assembly> (AppDomain.CurrentDomain.GetAssemblies());

  foreach (var asm in assembly)
  {
                
  }
  Type objectType = (from asm in assembly
                     from type in asm.GetTypes()
                     where type.IsClass && type.Name == controller
                     select type).FirstOrDefault();
  return objectType;
}*/

/*public virtual Type GetControllerType(string name)
{
    var sb = new StringBuilder();

    sb.Append(Application.Instance.Settings.Types.ControllerNamespace ?? "");

    if (sb.Length > 0)
        sb.Append(".");

    sb.Append(name);
    sb.Append(Application.Instance.Settings.Types.ControllerSuffix);

    var interfaceName = typeof(IController).Name;

    return _typeFinder.Find(
        sb.ToString(),
        t => t.GetInterface(interfaceName) != null
        );
}
 
 
 public object CreateInstance(string typeName) {
   var type = AppDomain.CurrentDomain.GetAssemblies()
              .SelectMany(a => a.GetTypes())
              .FirstOrDefault(t => t.FullName == typeName);

   return type.CreateInstance();
}
 */