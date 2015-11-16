using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieHunter.Services.Contracts
{
    public interface IUsersService
    {
        object GetByName(string username);
    }
}
