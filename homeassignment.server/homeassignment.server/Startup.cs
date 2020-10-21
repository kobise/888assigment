using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Client.Abstractions;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using homeassignment.server.GeaphQL;
using homeassignment.server.Redis;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Serilog;
using StackExchange.Redis;
using Microsoft.AspNetCore.Cors;

namespace homeassignment.server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {


            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.Console()
                .CreateLogger();
            services.AddSingleton(Log.Logger);
            services.AddSingleton<IGraphQLClient>(new GraphQLHttpClient(Configuration["GraphQLURL"], new NewtonsoftJsonSerializer()));
            services.AddScoped<IGraphQLRepository, GraphQLRepository>();
            services.AddSingleton<IDatabase>( ConnectionMultiplexer.Connect(Configuration["RedisConnectionString"]).GetDatabase());
            services.AddScoped<ICacheContinentRepository, RedisRepository>();
            services.AddScoped<IContinentRepository, ContinentRepository>();
            services.AddScoped<IContinentService, ContinentService>();

            services.AddCors();

            services.AddSwaggerGen();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors(
                   options => options.AllowAnyOrigin().AllowAnyMethod()
               );

            //add swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }


            app.UseHttpsRedirection();
            app.UseMvc();


        }
    }
}
