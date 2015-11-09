using System.Linq;
using MovieHunter.Models;

namespace MovieHunter.Services.Contracts
{
    public interface ITrailersService
    {
        IQueryable<Trailer> GetAllTrailers();
        Trailer GetById(int id);
    }
}