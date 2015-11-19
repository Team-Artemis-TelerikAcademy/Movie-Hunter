using MovieHunter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieHunter.Services.Contracts
{
    public interface IUsersService
    {
        User GetByName(string username);
    }
}
