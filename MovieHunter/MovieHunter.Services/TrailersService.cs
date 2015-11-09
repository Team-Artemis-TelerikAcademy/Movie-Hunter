using System.Linq;
using MovieHunter.Models;
using MovieHunter.Services.Contracts;

namespace MovieHunter.Services
{
    public class TrailersService : ITrailersService
    {
        private IRepository<Trailer> trailers;

        public TrailersService(IRepository<Trailer> trailersRepo)
        {
            this.trailers = trailersRepo;
        }

        public IQueryable<Trailer> GetAllTrailers()
        {
            return this.trailers.All().OrderByDescending(trailer => trailer.ReleaseDate);
        }

        public Trailer GetById(int id)
        {
            var trailer = this.trailers.Find(id);
            return trailer;
        }
    }
}