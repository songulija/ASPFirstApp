using Core.Models;
using DataStore.EF;
using FirstAspNet.Filters.V2;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstAspNet.Controllers.V2
{

    /// <summary>
    /// we need to inherit this class from ControllerBase, it contains everything
    /// you need to create ApiController. And we define this
    /// And define this class as ApiController. we can add route to controller.
    /// and [controller] will get name of our controller, in our case Tickets becouse
    /// it gets everything from name before word Controller
    /// it gets everything from name before word Controller. We can specify ApiVersion number
    /// </summary>
    [ApiVersion("2.0")]
    [ApiController]
    [Route("api/tickets")]
    public class TicketsV2Controller : ControllerBase
    {

        //create private var for BugsContext which represents Database
        private readonly BugsContext db;

        //in constructor we need to inject BugsContext
        //our DbContext in injected in Startup.cs, apply BugsContext that we get to our private var
        public TicketsV2Controller(BugsContext db)
        {
            this.db = db;
        }

        /// <summary>
        /// MVC Controller can return all type of data. Even WebApi can return JSON, XML ....
        /// We need generic return type. IAction result can return all of that. So you can have 
        /// one return type that contains everything. When person makes http GET request to
        /// specified route
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            //http response has different status code. like 200 which is means its ok
            return Ok(await db.Tickets.ToListAsync());
        }

        /**
         * When you make http GET request to api/tickets/ route 
         * In function we have to define that id
         */
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            //Find method will find entity with that primary key value in Tickets table
            var ticket = await db.Tickets.FindAsync(id);
            if (ticket == null)
                return NotFound();
            return Ok(ticket);
        }

        /**
         * When you make http POST request to api/tickets/
         * And we have filter for our V2 POST and PUT request. It makes sure
         * we have added Description to ticket
         */
        [HttpPost]
        [Ticket_EnsureDescriptionPresentActionFilter]
        public async Task<IActionResult> Post([FromBody]Ticket ticket)
        {
            //with Add we add new project to dbContext, it will be marked as added
            //and then with SaveChanges we basically insert it to db
            db.Tickets.Add(ticket);
            //wait until it saves changes then render rest of code
            await db.SaveChangesAsync();

            //this CreatedAtAction will return 201 code which means created
            //providing action(function) name GetById providing that function id it needs
            //and last param is object
            return CreatedAtAction(
                nameof(GetById),
                new { id = ticket.TicketId },
                ticket
                );
        }

        /**
         * When you make http PUT request to api/tickets
         * And we have filter for our V2 POST and PUT request. It makes sure
         * we have added Description to ticket
         */
        [HttpPut("{id}")]
        [Ticket_EnsureDescriptionPresentActionFilter]
        public async Task<IActionResult> Put(int id, [FromBody]Ticket ticket)
        {
            if (id != ticket.TicketId) return BadRequest();

            //entry gets changeTracking for given entity. and we're telling that its state supposed to be modified
            db.Entry(ticket).State = EntityState.Modified;

            //try if saveChanges fails then. 
            try
            {
                await db.SaveChangesAsync();
            }
            catch
            {
                if (await db.Projects.FindAsync(id) == null)
                    return NotFound();
                throw;
            }

            return NoContent();
        }



        /**
         * When you make http DELETE request to api/tickets/.. route and provide id
         * In function we have to define that id
         */
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            //you can implement like hide field to not actually delete
            var ticket = await db.Tickets.FindAsync(id);
            if (ticket == null)
                return NotFound();
            //remove from dbContext, it will mark as deleted, and SaveChanges
            //will generate actual delete statement to delete from DB
            db.Tickets.Remove(ticket);
            await db.SaveChangesAsync();

            return Ok(ticket);
        }
    }
}
