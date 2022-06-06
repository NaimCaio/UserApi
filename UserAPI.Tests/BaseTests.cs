using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserAPI.Domain.BaseServices;
using UserAPI.Domain.Interfaces;
using UserAPI.Domain.Models;
using UserAPI.Infra.EF;
using UserAPI.Infra.Repositories;
using UserAPI.Services;
using UserAPI.Services.Interfaces;

namespace UserAPI.Tests
{
    [TestClass]
    public abstract class BaseTests
    {
        protected UserAPIDBContext _dbContext;
        protected readonly ServiceCollection _services = new ServiceCollection();

        public virtual void SetupDBTestDependencies(string dbName = null)
        {
            _services.AddLogging(configure => configure.AddConsole());
            var escolaridades = new Escolaridade("Infantil,Fundamental,Medio,Superior");
            _services.AddSingleton(escolaridades);
            _services.AddScoped<IRepository<User>, Repository<User>>();
            _services.AddScoped<IUserService, UserService>();
            _services.AddScoped<IValidationService, ValidationService>();
            _services
                .AddDbContext<UserAPIDBContext>(options =>
                    options
                        .UseInMemoryDatabase(databaseName: dbName ?? Guid.NewGuid().ToString())
                );
        }
    }
}
