using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using MovieHunter.Models;
using MovieHunter.Services.Contracts;
using FLExtensions.Common;

namespace MovieHunter.ControllerTests.Mocks
{
    public class Repositories
    {
        private static readonly Random rng = new Random();

        public static string[] descriptions = new string[]
        {
            "Pesho pishe veb",
            "A az testvam servera v 3 prez noshtta",
            "Ivan se prevrushta v plesen i stava izvesten",
            "Senzaciq: nqkoi uspqva da si hostne app-a v azure ot pyrviq pyt",
            "testtesttest"
        };

        public static string[] titles = new string[] 
        {
            "Otborna rabota",
            "Otborna rabota 2",
            "Nz",
            "Otborna rabota 3",
            "Forumen jivot: Kritika kym Kyci"
        };

        public static string[] urls = new string[]
        {
            "http://testtesttest.com",
            "http://kircho.dragoev.votes",
            "http://telerigacademy.com",
            "http://pastebin.com/123",
            "http://somelink.net"
        };

        public static string[] genres = new string[] 
        {
            "Vampire romance",
            "Academy stuffs",
            "Seriozna rabota",
            "sfsdfsdf",
            "sfsfdsfsdf"
        };

        public static string[] names = new string[]
        {
            "Pesho",
            "Bay Ivan",
            "Mariya",
            "Bobkluka",
            "Magi"
        };

        public static IRepository<Movie> Movies
        {
            get
            {
                var mockUserRepo = new Mock<IRepository<Movie>>();

                

                mockUserRepo.Setup(x => x.All()).Returns(() =>
                {
                    var result = new List<Movie>();

                    for (int i = 0; i < 5; i++)
                    {
                        result.Add(new Movie()
                        {
                            Description = descriptions[i],
                            Id = i + 1,
                            ImageUrl = urls[i],
                            ReleaseDate = DateTime.Now.AddDays(i % 2 == 0 ? 20 : -20),
                            Title = titles[i],
                            
                        });

                        result[i].Genres.Add(new Genre()
                        {
                            Name = genres[i]
                        });
                    }

                    return result.AsQueryable();
                });

                mockUserRepo
                    .Setup(x => x.Find(It.IsAny<int>()))
                    .Returns<int>(id =>
                    {
                        return mockUserRepo.Object.All().FirstOrDefault(y => y.Id == id);
                    });

                return mockUserRepo.Object;
            }
        }

        public static IRepository<User> Users
        {
            get
            {
                var mockUsers = new Mock<IRepository<User>>();

                var result = new List<User>();

                for (int i = 0; i < 5; i++)
                {
                    result.Add(new User()
                    {
                        Email = rng.NextString(5,6) + "@gmail.com",
                        UserName = names[i],
                        Id = (i + 1).ToString()
                    });
                }

                mockUsers.Setup(x => x.All()).Returns(() => result.AsQueryable());

                return mockUsers.Object;
            }
        }

        public static IRepository<Actor> Actors
        {
            get
            {
                throw new NotImplementedException("Actors mock not implemented");
            }
        }
    }
}
