using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using MovieHunter.Data;
using MovieHunter.DependancyProvider;

using Ninject.Web;

namespace MovieHunter.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            ModuleFactory.Get<IMovieDbContext>();
        }
    }
}
