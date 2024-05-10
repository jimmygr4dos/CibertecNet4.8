using Cibertec.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cibertec.Repositories.Northwind
{
    public interface ICustomerRepository: IRepository<Customers>
    {
        IEnumerable<Customers> PagedList(int startRow, int endRow);
        int Count();
    }
}
