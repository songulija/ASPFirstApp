using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstAspNet.Models
{
    public class Ticket
    {
        public int TicketId { get; set; }
        public int ProjectId { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
    }
}
