using DataStore.EF;
using Microsoft.AspNetCore.Mvc;
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

        

    }
}
