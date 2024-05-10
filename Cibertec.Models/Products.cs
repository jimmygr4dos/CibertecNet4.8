using Dapper.Contrib.Extensions;
using KeyAttributeDapper = Dapper.Contrib.Extensions.KeyAttribute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cibertec.Models
{
    public class Products
    {
        [KeyAttributeDapper]
        [Required(ErrorMessage = "El campo es obligatorio")]
        public int ProductID { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        public string ProductName { get; set; }

        public int SupplierID { get; set; }
        public int CategoryID { get; set; }
        public string QuantityPerUnit { get; set; }
        public double UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
        public int UnitsOnOrder { get; set; }
        public int ReorderLevel { get; set; }
        public int Discontinued { get; set; }
    }
}
