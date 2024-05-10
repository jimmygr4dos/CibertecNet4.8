using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cibertec.Models
{
    public class Customers
    {
		[ExplicitKey]
		[Required(ErrorMessage ="El campo es obligatorio")]
		public string CustomerID { get; set; }

		[Required(ErrorMessage = "El campo es obligatorio")]
		public string CompanyName { get; set; }

		[Display(Name="Contacto",Description="Nombre del contacto")]
		public string ContactName { get; set; }

		public string ContactTitle { get; set; }
		public string Address { get; set; }
		public string City { get; set; }
		public string Region { get; set; }
		public string PostalCode { get; set; }
		public string Country { get; set; }
		public string Phone { get; set; }
		public string Fax { get; set; }

    }
}
