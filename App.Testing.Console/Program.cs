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

async Task TestTickets()
{
    Console.WriteLine("////////////////////");
    Console.WriteLine("Reading all tickets...");
    await GetTickets();

    Console.WriteLine("////////////////////");
    Console.WriteLine("Reading contains 1...");
    await GetTickets("1");

    Console.WriteLine("////////////////////");
    Console.WriteLine("Create a ticket...");
    var tId = await CreateTicket();
    await GetTickets();

    Console.WriteLine("////////////////////");
    Console.WriteLine("Update a ticket...");
    var ticket = await GetTicketById(tId);
    await UpdateTicket(ticket);
    await GetTickets();

    Console.WriteLine("////////////////////");
    Console.WriteLine("Delete a ticket...");
    await DeleteTicket(tId);
    await GetTickets();
}

async Task GetTickets(string filter = null)
{
    //GET all tickets with repository help
    TicketRepository ticketRepository = new(apiExecuter);
    var tickets = await ticketRepository.GetAsync(filter);
    foreach (var ticket in tickets)
    {
        Console.WriteLine($"Ticket: {ticket.Title}");
    }
}

async Task<Ticket> GetTicketById(int id)
{
    //GET ticket by id with repository help
    TicketRepository ticketRepository = new(apiExecuter);
    var ticket = await ticketRepository.GetByIdAsync(id);
    return ticket;
}

async Task<int> CreateTicket()
{
    //Create new ticket with repository help
    TicketRepository ticketRepository = new(apiExecuter);
    return await ticketRepository.CreateAsync(new Ticket
    {
        ProjectId = 2,
        Title = "This a very difficult.",
        Description = "Something is wrong on the server."
    });
}

async Task UpdateTicket(Ticket ticket)
{
    //Update ticket with repository help
    TicketRepository ticketRepository = new(apiExecuter);
    ticket.Title += " Updated";
    await ticketRepository.UpdateAsync(ticket);
}

async Task DeleteTicket(int id)
{
    //Delete ticket with repository help
    TicketRepository ticketRepository = new(apiExecuter);
    await ticketRepository.DeleteAsync(id);
}

