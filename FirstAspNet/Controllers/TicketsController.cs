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
    public class TicketsController : ControllerBase
    {
        /// <summary>
        /// MVC Controller can return all type of data. Even WebApi can return JSON, XML ....
        /// We need generic return type. IAction result can return all of that. So you can have 
        /// one return type that contains everything. When person makes http GET request to
        /// specified route
        /// </summary>
        [HttpGet]
        public IActionResult Get()
        {
            //http response has different status code. like 200 which is means its ok
            return Ok("Reading all the tickets");
        }

        /**
         * When you make http GET request to api/tickets/ route 
         * In function we have to define that id
         */
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById(int id)
        {
            //http response has different status code. like 200 which is means its ok
            return Ok($"Reading ticket #{id}.");
        }

        /**
         * When you make http POST request to api/tickets/
         */
        [HttpPost]
        public IActionResult Post()
        {
            //http response has different status code. like 200 which is means its ok
            return Ok("Creating a new ticket.");
        }

        /**
         * When you make http PUT request to api/tickets
         */
        [HttpPut]
        public IActionResult Put()
        {
            //http response has different status code. like 200 which is means its ok
            return Ok("Updating a new ticket.");
        }



        /**
         * When you make http DELETE request to api/tickets/.. route and provide id
         * In function we have to define that id
         */
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            //http response has different status code. like 200 which is means its ok
            return Ok($"Deleting ticket #{id}.");
        }
    }
}
