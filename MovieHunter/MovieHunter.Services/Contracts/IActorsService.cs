using System.Linq;
using MovieHunter.Models;

namespace MovieHunter.Services.Contracts
{
    public interface IActorsService
    {
        IQueryable<Actor> GetAllActors();
        Actor GetActorById(int id);
        IQueryable<Actor> GetActorByName(string name);
    }
}