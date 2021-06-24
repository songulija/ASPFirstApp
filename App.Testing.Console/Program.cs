using App.Repository;
using App.Repository.ApiClient;
using Core.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

HttpClient httpClient = new();
IWebApiExecuter apiExecuter = new WebApiExecuter("http://localhost:41936", httpClient);

Console.WriteLine("////////////////////");
Console.WriteLine("Reading projects...");
await GetProjects();


Console.WriteLine("////////////////////");
Console.WriteLine("Reading project tickets...");
await GetProjectTickets(1);

Console.WriteLine("////////////////////");
Console.WriteLine("Create a project...");
var pId = await CreateProject();
await GetProjects();

Console.WriteLine("////////////////////");
Console.WriteLine("Update a project...");
var project = await GetProject(pId);
await UpdateProject(project);
await GetProjects();

Console.WriteLine("////////////////////");
Console.WriteLine("Delete a project...");
await DeleteProject(pId);
await GetProjects();

//get all projects
async Task GetProjects()
{
    //get all projects with repository help
    ProjectRepository repository = new(apiExecuter);
    var projects = await repository.GetAsync();
    foreach (var project in projects)
    {
        Console.WriteLine($"Project: {project.Name}");
    }
}


async Task<Project> GetProject(int id)
{
    //get project by id with Repository help
    ProjectRepository repository = new(apiExecuter);
    return await repository.GetByIdAsync(id);
}

async Task GetProjectTickets(int id)
{
    //get project tickets with Repository help
    var project = await GetProject(id);
    Console.WriteLine($"Project: {project.Name}");

    ProjectRepository repository = new(apiExecuter);
    var tickets = await repository.GetProjectTicketsAsync(id);
    foreach (var ticket in tickets)
    {
        Console.WriteLine($"    Ticket: {ticket.Title}");
    }
}

async Task<int> CreateProject()
{
    //create new project with repository help
    var project = new Project { Name = "Another Project" };
    ProjectRepository repository = new(apiExecuter);
    return await repository.CreateAsync(project);
}

async Task UpdateProject(Project project)
{
    //Update new project with repository help
    ProjectRepository repository = new(apiExecuter);
    project.Name += " updated";
    await repository.UpdateAsync(project);
}

async Task DeleteProject(int id)
{
    //Delete new project with repository help
    ProjectRepository repository = new(apiExecuter);
    await repository.DeleteAsync(id);
}
