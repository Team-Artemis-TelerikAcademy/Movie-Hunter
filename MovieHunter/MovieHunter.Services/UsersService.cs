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
        private IRepository<User> usersRepository;

        public UsersService(IRepository<User> usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        public User GetByName(string username)
        {
            return this.usersRepository.All().FirstOrDefault(user => user.UserName.Equals(username, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
