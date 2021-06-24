using App.Repository.ApiClient;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Repository
{
    public class ProjectRepository
    {
        private readonly IWebApiExecuter webApiExecuter;

        //saying that this projectRepository is expecting a object to implement this IWebApiExecuter executer
        public ProjectRepository(IWebApiExecuter webApiExecuter)
        {
            this.webApiExecuter = webApiExecuter;
        }


        public async Task<IEnumerable<Project>> GetAsync()
        {
            //invoking GET method to this route
            return await webApiExecuter.InvokeGet<IEnumerable<Project>>("api/projects");
        }

        //get Project id
        public async Task<Project> GetByIdAsync(int id)
        {
            //invoking GET method to this route. providing id
            return await webApiExecuter.InvokeGet<Project>($"api/projects/{id}");
        }
        //get project Tickets
        public async Task<IEnumerable<Ticket>> GetProjectTicketsAsync(int projectId)
        {
            //invoking GET method to this route. providing projectId
            return await webApiExecuter.InvokeGet<IEnumerable<Ticket>>($"api/projects/{projectId}/tickets");
        }
        //create new project
        public async Task<int> CreateAsync(Project project)
        {
            //invoking POST method to this route. Providing Project object
            project = await webApiExecuter.InvokePost("api/projects", project);
            return project.ProjectId;
        }
        //update project
        public async Task UpdateAsync(Project project)
        {
            //invoking PUT method to this route. Providing Project object
            await webApiExecuter.InvokePut($"api/projects/{project.ProjectId}", project);
        }
        //delete project
        public async Task DeleteAsync(int id)
        {
            //invoking Delete method to this route. Providing id
            await webApiExecuter.InvokeDelete($"api/projects/{id}");
        }
    }
}
