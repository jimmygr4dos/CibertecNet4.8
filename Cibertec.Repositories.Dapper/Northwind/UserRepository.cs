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
    public class UserRepository : Repository<Users>, IUserRepository
    {
        public UserRepository(string connectionString) : base(connectionString)
        {
        }

        public Users ValidaterUser(string email, string password)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Email", email);
                parameters.Add("@Password", password);
                return connection.QueryFirstOrDefault<Users>("dbo.ValidateUser", parameters, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
