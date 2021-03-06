[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(MovieHunter.Api.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(MovieHunter.Api.App_Start.NinjectWebCommon), "Stop")]

namespace MovieHunter.Api.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    //using DependancyProvider;
    using Ninject;
    using Ninject.Web.Common;
    using Data;
    using System.Collections.Generic;
    using Common.Contracts;
    using Services;
    using Services.Contracts;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        public static Action<IKernel> DbBindings = kernel => 
        {
            new DataModule().RegisterBindings(kernel);
        };

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            DbBindings(kernel);

            kernel.Bind(typeof(IRepository<>)).To(typeof(EfRepository<>));

            // custom bindings here
            var appModules = new List<IModule>()
            {
                // new DataModule(),
                new ServicesModule()
            };

            foreach (var module in appModules)
            {
                module.RegisterBindings(kernel);
            }

            // initialize object factory with the kernel
            //ModuleFactory.Initialize(kernel);
        }        
    }
}
