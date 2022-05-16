using EventStore.ClientAPI;
using KYSliotek.Books;
using KYSliotek.Domain.Book;
using KYSliotek.Domain.UserProfile;
using KYSliotek.Framework;
using KYSliotek.Infrastructure;
using KYSliotek.Projections;
using KYSliotek.UserProfile;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Raven.Client.Documents;
using System.Collections.Generic;

namespace KYSliotek
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        private Microsoft.AspNetCore.Hosting.IHostingEnvironment Environment { get; }

        public Startup(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment; 
        }       

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var esConnection = EventStoreConnection.Create( Configuration["eventStore:connectionString"],
                                                            ConnectionSettings.Create().KeepReconnecting(),
                                                            Environment.ApplicationName);
            var es_store = new EsAggregateStore(esConnection);
            services.AddSingleton(esConnection);
            services.AddSingleton<IAggregateStore>(es_store);
            

            //inMemory collection
            //var userDetails = new List<ReadModels.UserDetails>();
            //services.AddSingleton<IEnumerable<ReadModels.UserDetails>>(userDetails);

            //var projectionManager = new ProjectionManager(esConnection, new UserDetailsProjection(userDetails));
            //services.AddSingleton<IHostedService>(new EventStoreService(esConnection, projectionManager));
            services.AddSingleton<IHostedService, EventStoreService>();
            services.AddSingleton(new UserProfileApplicationServiceForEventStore(es_store));
            services.AddMvc();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", 
                    new OpenApiInfo { Title = "KYSliotek", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();  
            }
            app.UseRouting();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "KYSliotek v1");
                c.RoutePrefix = string.Empty;
            });

            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
//dependencies for ravenDb that were in ConfigureServices method
/*
 //RavenDb
            var store = new DocumentStore
            {
                Urls = new[] { "http://localhost:8080" },
                Database = "MiniBibliotek_Db",
                Conventions =
                {
                    FindIdentityProperty = x => x.Name == "DbId"
                }
            };
            store.Initialize();

            services.AddScoped(c => store.OpenAsyncSession());
            services.AddScoped<IUnitOfWork, RavenDbUnitOfWork>();
            services.AddScoped<IBooksRepository, BookRepository>();
            services.AddScoped<IUserProfileRepository, UserProfileRepository>();
            services.AddScoped<BooksApplicationService>();
            services.AddScoped(c => new UserProfileApplicationService(
               c.GetService<IUserProfileRepository>(),
               c.GetService<IUnitOfWork>()));
 */
