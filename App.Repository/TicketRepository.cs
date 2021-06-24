using App.Repository.ApiClient;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Repository
{
    public class TicketRepository
    {
        private readonly IWebApiExecuter webApiExecuter;

        public TicketRepository(IWebApiExecuter webApiExecuter)
        {
            this.webApiExecuter = webApiExecuter;
        }
        //getting all tickets
        public async Task<IEnumerable<Ticket>> GetAsync(string filter = null)
        {
            string uri = "api/tickets?api-version=2.0";
            if (!string.IsNullOrWhiteSpace(filter))
                uri += $"&titleordescription={filter.Trim()}";

            return await webApiExecuter.InvokeGet<IEnumerable<Ticket>>(uri);
        }
        //get ticket by id
        public async Task<Ticket> GetByIdAsync(int id)
        {
            //invoking GET method to this route. Providing id
            return await webApiExecuter.InvokeGet<Ticket>($"api/tickets/{id}?api-version=2.0");
        }
        //create ticket
        public async Task<int> CreateAsync(Ticket ticket)
        {
            //invoking POST method to this route. Providing Ticket object
            ticket = await webApiExecuter.InvokePost("api/tickets?api-version=2.0", ticket);
            return ticket.TicketId.Value;
        }
        //update ticket
        public async Task UpdateAsync(Ticket ticket)
        {
            //invoking PUT method to this route. Ticket object
            await webApiExecuter.InvokePut($"api/tickets/{ticket.TicketId}?api-version=2.0", ticket);
        }
        //delete ticket
        public async Task DeleteAsync(int id)
        {
            //invoking Delete method to this route. Providing id
            await webApiExecuter.InvokeDelete($"api/tickets/{id}?api-version=2.0");
        }
    }
}
