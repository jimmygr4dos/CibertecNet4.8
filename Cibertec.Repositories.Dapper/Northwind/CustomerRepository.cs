using Cibertec.Models;
using Cibertec.Repositories.Northwind;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cibertec.Repositories.Dapper.Northwind
{
    public class CustomerRepository : Repository<Customers>, ICustomerRepository
    {
        public CustomerRepository(string connectionString) : base(connectionString)
        {
        }

        public int Count()
        {
            using(var connection = new SqlConnection(_connectionString))
            {
                return connection.ExecuteScalar<int>("Select count(1) from dbo.Customers");
            }
        }

        public IEnumerable<Customers> PagedList(int startRow, int endRow)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                if (startRow >= endRow) return new List<Customers>();

                var parameters = new DynamicParameters();
                parameters.Add("@startRow", startRow);
                parameters.Add("@endRow", endRow);
                return connection.Query<Customers>("dbo.sp_customer_pagedlist", parameters, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
