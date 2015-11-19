﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using MovieHunter.Data;
using MovieHunter.Models;

[assembly: OwinStartup(typeof(MovieHunter.Api.Startup))]

namespace MovieHunter.Api
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {



            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            ConfigureAuth(app);
        }
    }
}
