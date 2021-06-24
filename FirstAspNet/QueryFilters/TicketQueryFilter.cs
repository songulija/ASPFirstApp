using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstAspNet.QueryFilters
{
    public class TicketQueryFilter
    {
        //using ? so user doesnt have to provide id
        public int? Id { get; set; }
        public string TitleOrDescription { get; set; }
    }
}
