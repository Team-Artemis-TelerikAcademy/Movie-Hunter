namespace MovieHunter.Services
{
    using Common.Contracts;
    using Contracts;
    using Models;
    using Ninject;

    public class ServicesModule : IModule
    {
        public void RegisterBindings(IKernel kernel)
        {
            

            kernel.Bind<IActorsService>().ToMethod(c => new ActorsService(kernel.Get<IRepository<Actor>>()));
            kernel.Bind<IGenresService>().ToMethod(c => new GenresService(kernel.Get<IRepository<Genre>>()));
            kernel.Bind<IMoviesService>().ToMethod(c => new MoviesService(kernel.Get<IRepository<Movie>>()));
            kernel.Bind<ITrailersService>().ToMethod(c => new TrailersService(kernel.Get<IRepository<Trailer>>()));
            kernel.Bind<IUsersService>().ToMethod(c => new UsersService(kernel.Get<IRepository<User>>()));
            // TODO: implement binding using reflection
        }
    }
}