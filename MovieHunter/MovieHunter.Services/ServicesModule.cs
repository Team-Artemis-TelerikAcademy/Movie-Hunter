namespace MovieHunter.Services
{
    using System;
    using MovieHunter.Common.Contracts;
    using Contracts;
    using Models;
    using Ninject;
    using MovieHunter.Data;
    using System.Data.Entity;

    public class ServicesModule : IModule
    {
        public void RegisterBindings(IKernel kernel)
        {
            kernel.Bind(typeof(IRepository<>)).To(typeof(EfRepository<>));

            kernel.Bind<IActorsService>().ToMethod(c => new ActorsService(kernel.Get<IRepository<Actor>>()));
            kernel.Bind<IGenresService>().ToMethod(c => new GenresService(kernel.Get<IRepository<Genre>>()));
            kernel.Bind<IMoviesService>().ToMethod(c => new MoviesService(kernel.Get<IRepository<Movie>>()));
            kernel.Bind<ITrailersService>().ToMethod(c => new TrailersService(kernel.Get<IRepository<Trailer>>()));
        }
    }
}