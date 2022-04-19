using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AretoExercise.Application.Interfaces;
using AretoExercise.Application.Services;
using AretoExercise.Auth;
using AretoExercise.Data;
using AretoExercise.Data.Interfaces;
using AretoExercise.Data.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AretoExercise
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
            services.AddControllers();
            services.AddAuthentication("BasicAuthentication")
              .AddScheme<AuthenticationSchemeOptions, ApiAuthentication>("BasicAuthentication", null);
            services.AddDbContext<AretoDBContext>(item => item.UseSqlServer(Configuration.GetConnectionString("connectionString")));

            services.AddScoped<ITransactionsRepository, TransactionsRepository>();
            services.AddScoped<IUsersRepository, UsersRepository>();
            
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IStripeService, StripeService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
