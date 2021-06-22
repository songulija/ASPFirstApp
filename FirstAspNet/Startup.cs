using DataStore.EF;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstAspNet
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;

        //constructor. To assign 
        public Startup(IWebHostEnvironment env)
        {
            this._env = env;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        //this configures added dependencies and configures behaviour of middleware
        public void ConfigureServices(IServiceCollection services)
        {
            //if environment is IsDevelopment use inMemory database
            if (_env.IsDevelopment())
            {
                //we will use in Memory database(LOCAL).
                //Adding DbContext witch we build.And we are configuring options
                //and options will be to use InMemoryDatabase. And give NAME to Database
                //these options will be passed to BugsContext class constructor options
                services.AddDbContext<BugsContext>(options =>
                {
                    options.UseInMemoryDatabase("Bugs");
                });
            }
            

            //adds services for controllers. basically we configure our added middleware
            //in  Configure endpoints.MapControllers. We dont configure more leavy empty by default
            services.AddControllers();

            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //it contains all the middleware you want to add to pipeline
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, BugsContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                //if IsDevelopment create in-memory database with initial data. becouse
                //when creating in-memory db you have to have initial data
                //ensure that database isDeleted and then created
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }

            app.UseRouting();
            //basically this is saying that i have these endpoints
            //so endpoint middleware configures endpoints
            app.UseEndpoints(endpoints =>
            {
                //adds endpoints for controller action without specifying any routes
                //its as middleware
                endpoints.MapControllers();
            });
        }
    }
}
