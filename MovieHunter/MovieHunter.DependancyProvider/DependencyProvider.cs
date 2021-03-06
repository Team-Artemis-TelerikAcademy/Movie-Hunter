﻿namespace MovieHunter.DependancyProvider
{
    using System;
    using Ninject;

    public static class ModuleFactory
    {
        private static IKernel appKernel;

        public static void Initialize(IKernel kernel)
        {
            appKernel = kernel;
        }

        public static T Get<T>()
        {
            return appKernel.Get<T>();
        }

        public static void Register(Type source, Type destination)
        {
            appKernel.Bind(source, destination);
        }

        public static void Register<TSource, RDestination>()
            where TSource : RDestination
        {
            appKernel.Bind<RDestination>().To<TSource>();
        }

        public static void Register<RDestination>(Func<RDestination> providerMethod)
        {
            appKernel.Bind<RDestination>().ToMethod(c => providerMethod());
        }
    }
}