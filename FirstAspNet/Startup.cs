using DataStore.EF;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc.Versioning;
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
            services.AddApiVersioning(options =>
            {
                //we will use http header to do versioning. that means version number
                //is not in url or query string. they will have same url
                options.AssumeDefaultVersionWhenUnspecified = true;
                //default version 1.0. But we can change VERSION IN CONTROLLERS by adding [ApiVersion("2.0") for ex
                options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
                //specify what header is that is used to comunicate the version number
                //usually http header start with Xhttps://www.udemy.com/staticx/udemy/js/webpack/icon-play.ac3f32ecb72a0c3f674fa5a3f3062a56.svg
                //when you call api you need to use this as header name
                options.ApiVersionReader = new HeaderApiVersionReader("X-API-Version");
            });

            
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
