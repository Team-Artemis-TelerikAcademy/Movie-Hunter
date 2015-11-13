namespace MovieHunter.Common.Contracts
{
    using Ninject;

    public interface IModule
    {
        void RegisterBindings(IKernel kernel);
    }
}