using MovieHunter.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieHunter.Models;

namespace MovieHunter.Services
{
    public class UsersService : IUsersService
    {
        private EfRepository<User> efRepository;

        public UsersService(EfRepository<User> efRepository)
        {
            this.efRepository = efRepository;
        }

        public User GetByName(string username)
        {
            throw new NotImplementedException();
        }
    }
}
