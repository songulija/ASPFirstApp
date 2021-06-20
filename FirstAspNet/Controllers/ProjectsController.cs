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
        /**
         * When making POST request 
         */
        [HttpPost]
        public IActionResult Post()
        {
            return Ok($"Creating project");
        }
        /**
         * When making PUT request 
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
