using Cibertec.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cibertec.Repositories.Northwind
{
    public interface IUserRepository: IRepository<Users>
    {
        Users ValidaterUser(string email, string password);
    }
}
