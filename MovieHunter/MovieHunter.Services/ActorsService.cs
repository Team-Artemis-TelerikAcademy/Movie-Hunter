using System;
using System.Linq;
using System.Web.Http;
using MovieHunter.Models;
using MovieHunter.Services.Contracts;

namespace MovieHunter.Services
{
    public class ActorsService : IActorsService
    {
        private IRepository<Actor> actors;

        public ActorsService(IRepository<Actor> actorsRepo)
        {
            this.actors = actorsRepo;
        }


        public IQueryable<Actor> GetAllActors()
        {
            return this.actors.All().OrderBy(a => a.FullName);
        }

        public Actor GetActorById(int id)
        {
            return this.actors.Find(id);
        }

        public IQueryable<Actor> GetActorByName(string name)
        {
            return
                this.actors.All()
                    .Where(
                        a =>
                            a.FirstName.ToLower() == name.ToLower() || a.LastName.ToLower() == name.ToLower() ||
                            a.FullName == name.ToLower());
        }
    }
}