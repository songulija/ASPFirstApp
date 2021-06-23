using Core.Models;
using DataStore.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstAspNet.Controllers
{
    /// <summary>
    /// we need to inherit this class from ControllerBase, it contains everything
    /// you need to create ApiController. And we define this
    /// And define this class as ApiController. we can add route to controller.
    /// and [controller] will get name of our controller, in our case Tickets becouse
    /// it gets everything from name before word Controller
    /// it gets everything from name before word Controller. We can specify ApiVersion number
    /// </summary>
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        //create private var for BugsContext which represents Database
        private readonly BugsContext db;

        //in constructor we need to inject BugsContext
        //our DbContext in injected in Startup.cs, apply BugsContext that we get to our private var
        public ProjectsController(BugsContext db)
        {
            this.db = db;
        }


        /// MVC Controller can return all type of data. Even WebApi can return JSON, XML ....
        /// We need generic return type. IAction result can return all of that. So you can have 
        /// one return type that contains everything. When person makes http GET request to
        /// specified route
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            //return all data from projects table in our database, AS LIST
            return Ok(await db.Projects.ToListAsync());
        }
        /**
         * When making GET request with id. Id comes from route to parameter
         */
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            //Find method will find entity with that primary key value in Projects table
            var project = await db.Projects.FindAsync(id);
            if (project == null)
                return NotFound();

            return Ok(project); 
        }

        /// <summary>
        /// When making GET request to api/projects/{pid}/tickets?tid={tid}
        /// providing both pid 
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="tid"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/api/projects/{pid}/tickets")]
        public async Task<IActionResult> GetProjectTickets(int pid)
        {
            //In tickets table searching for all tickets where ProjectId
            //is equal to pid that we get from GET request (from route). Get as List
            var tickets = await db.Tickets.Where(t => t.ProjectId == pid).ToListAsync();
            if (tickets == null || tickets.Count <= 0)
                return NotFound();

            return Ok(tickets);

        }
        /**
         * When you make http POST request to api/projects
         * Ticket object will come from BODY of POST request.
         * It basically json object coming from POST that will convert into object
         * Usually we use this in PUT, POST, PATCH methods to provide data
         */
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Project project)
        {
            //with Add we add new project to dbContext, it will be marked as added
            //and then with SaveChanges we basically insert it to db
            db.Projects.Add(project);
            await db.SaveChangesAsync();

            //this CreatedAtAction will return 201 code which means created
            //providing action(function) name GetById providing that function id it needs
            //and last param is object
            return CreatedAtAction(
                nameof(GetById),
                new {id = project.ProjectId},
                project
                );
        }
        /**
         * When you make http PUT request to api/projects/{id}
         * Ticket object will come from BODY of PUT request. and provide id too
         * It basically json object coming from PUT that will convert into object
         * Usually we use this in PUT, POST, PATCH methods to provide data
         */
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Project project)
        {
            if (id != project.ProjectId) return BadRequest();

            //entry gets changeTracking for given entity. and we're telling that its state supposed to be modified
            db.Entry(project).State = EntityState.Modified;

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
         * When making DELETE http request to specified route
         * Id comes from route to parameter
         */
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            //you can implement like hide field to not actually delete
            var project = await db.Projects.FindAsync(id);
            if (project == null) 
                return NotFound();
            //remove from dbContext, it will mark as deleted, and SaveChanges
            //will generate actual delete statement to delete from DB
            db.Projects.Remove(project);
            await db.SaveChangesAsync();

            return Ok(project);
        }
          
    }
}
