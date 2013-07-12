using System;

namespace OrionMvc.Web
{
    public interface IControllerFactory
    {
        IController CreateController(System.Web.HttpContext context, RouteMeta meta);
    }
}
