using Core.Models;
using DataStore.EF;
using Microsoft.AspNetCore.Mvc;
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
    /// </summary>
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
        public IActionResult Get()
        {
            //return all data from projects table in our database, AS LIST
            return Ok(db.Projects.ToList());
        }
        /**
         * When making GET request with id. Id comes from route to parameter
         */
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            //first need to find project with that id. if cant find it return error
            //Find will find entity with given primary key value
            var project = db.Projects.Find(id);
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
        public IActionResult GetProjectTickets(int pid)
        {
            //In tickets table searching for all tickets where ProjectId
            //is equal to pid that we get from GET request (from route). Get as List
            var tickets = db.Tickets.Where(t => t.ProjectId == pid).ToList();
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
        public IActionResult Post([FromBody]Project project)
        {
            return Ok(project);
        }
        /**
         * When you make http PUT request to api/projects
         * Ticket object will come from BODY of PUT request.
         * It basically json object coming from PUT that will convert into object
         * Usually we use this in PUT, POST, PATCH methods to provide data
         */
        [HttpPost]
        public IActionResult Put([FromBody] Project project)
        {
            return Ok(project);
        }

        /**
         * When making DELETE http request to specified route
         * Id comes from route to parameter
         */
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok($"Deleted project #{id}");
        }
          
    }
}
