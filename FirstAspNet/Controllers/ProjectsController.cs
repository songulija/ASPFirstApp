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
        /// MVC Controller can return all type of data. Even WebApi can return JSON, XML ....
        /// We need generic return type. IAction result can return all of that. So you can have 
        /// one return type that contains everything. When person makes http GET request to
        /// specified route
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Getting all projects");
        }
        /**
         * When making GET request with id. Id comes from route to parameter
         */
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok($"Getting Project with #{id}"); 
        }

        /// <summary>
        /// When making GET request to api/projects/{pid}/tickets?tid={tid}
        /// providing both pid and tid
        /// In FUNCTION we can also specify from what source it comes from, like tid comes from QUERY.
        /// Then it has to come from that source
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="tid"></param>
        /// <returns></returns>
        public IActionResult GetProjectTicket(int pid, [FromQuery] int tid)
        {
            if(tid == 0)
            {
                return Ok($"Reading all tickets belong to project ${pid}");
            }
            else
            {
                return Ok($"Reading project #{pid}, ticket #{tid}");
            }
        }
        /**
         * When you make http POST request to api/projects
         * Ticket object will come from BODY of POST request.
         * It basically json object coming from POST that will convert into object
         * Usually we use this in PUT, POST, PATCH methods to provide data
         */
        [HttpPost]
        public IActionResult Post()
        {
            return Ok($"Creating project");
        }
        /**
         * When you make http PUT request to api/projects
         * Ticket object will come from BODY of PUT request.
         * It basically json object coming from PUT that will convert into object
         * Usually we use this in PUT, POST, PATCH methods to provide data
         */
        [HttpPost]
        public IActionResult Put()
        {
            return Ok($"Updating project");
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
