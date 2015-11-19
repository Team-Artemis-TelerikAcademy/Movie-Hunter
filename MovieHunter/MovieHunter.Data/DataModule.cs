namespace MovieHunter.Data
{
    using System.Data.Entity;
    using Common.Contracts;
    using Ninject;

    public class DataModule : IModule
    {
        public void RegisterBindings(IKernel kernel)
        {
            kernel.Bind<DbContext>().To<MovieDbContext>();
            kernel.Bind<IMovieDbContext>().To<MovieDbContext>();
        }
    }
}