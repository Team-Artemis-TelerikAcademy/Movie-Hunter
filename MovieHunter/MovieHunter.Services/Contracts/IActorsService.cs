using System.Linq;
using MovieHunter.Models;

namespace MovieHunter.Services.Contracts
{
    public interface IActorsService
    {
        IQueryable<Actor> GetAll();
        Actor GetActorById(int id);
        IQueryable<Actor> GetActorByName(string name);
    }
}