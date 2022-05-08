using KYSliotek.Books;
using KYSliotek.Domain.Book;
using KYSliotek.Framework;
using KYSliotek.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Raven.Client.Documents;

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
            services.AddScoped<BooksApplicationService>();

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
