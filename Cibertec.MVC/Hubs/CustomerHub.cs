using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Cibertec.MVC.Hubs
{
    public class CustomerHub: Hub
    {
        static List<string> CustomersIDs = new List<string>();

        public void AddCustomerID(string id)
        {
            if(!CustomersIDs.Contains(id))
            {
                CustomersIDs.Add(id);
            }
            Clients.All.customerStatus(CustomersIDs);
        }

        public void RemoveCustomerID(string id)
        {
            if(CustomersIDs.Contains(id))
            {
                CustomersIDs.Remove(id);
            }
            Clients.All.customerStatus(CustomersIDs);
        }

        public override Task OnConnected()
        {
            return Clients.All.customerStatus(CustomersIDs);
        }

        public void Message(string message)
        {
            Clients.All.getMessage(message);
        }
    }
}