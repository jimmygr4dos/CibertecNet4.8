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
    public class NorthwindSP<T> where T: class
    {
        private readonly string _connectionString;

        public NorthwindSP(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<T> GetDataSP(string procedure)
        {
            using(var connection = new SqlConnection(_connectionString))
            {
                var datos = connection.Query<T>(procedure, commandType: CommandType.StoredProcedure).ToList();
                return datos;
            }
        }

        public IEnumerable<T> GetDataSP(string procedure, object parametros)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var datos = connection.Query<T>(procedure, parametros, commandType: CommandType.StoredProcedure).ToList();
                return datos;
            }
        }
    }
}
