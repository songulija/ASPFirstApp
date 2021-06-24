using DataStore.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstAspNet.Controllers.V2
{
    [ApiVersion("2.0")]
    [ApiController]
    [Route("api/projects")]
    public class ProjectsV2Controller : ControllerBase
    {
        private readonly BugsContext db;

        public ProjectsV2Controller(BugsContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await db.Projects.ToListAsync());
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var project = await db.Projects.FindAsync(id);
            if (project == null)
                return NotFound();

            return Ok(project);
        }

        [HttpGet]
        [Route("/api/projects/{pid}/tickets")]
        public async Task<IActionResult> GetProjectTickets(int pId)
        {
            var tickets = await db.Tickets.Where(t => t.ProjectId == pId).ToListAsync();
            if (tickets == null || tickets.Count <= 0)
                return NotFound();

            return Ok(tickets);
        }



    }
}
