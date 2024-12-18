﻿using Microsoft.Extensions.DependencyInjection;
using Hiwapardaz.SepehrBarin.Common.Extensions;
using Hiwapardaz.SepehrBarin.Persistence.Contexts.UnitOfWork;
using Hiwapardaz.SepehrBarin.Persistence.DbSeeder.Contracts;

namespace Hiwapardaz.SepehrBarin.Persistence.DbSeeder.Implementations
{
    public class DataSeedersRunner : IDataSeedersRunner
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IUnitOfWork _uow;
        public DataSeedersRunner(IServiceProvider serviceProvider, IUnitOfWork uow)
        {
            _serviceProvider=serviceProvider;
            _serviceProvider.NotNull(nameof(serviceProvider));

            _uow=uow;
            _uow.NotNull(nameof(uow));
        }
        public void RunAllDataSeeders()
        {
            var seeders = _serviceProvider.GetServices<IDataSeeder>().ToList();
            foreach (var seeder in seeders.OrderBy(dataSeeder => dataSeeder.Order))
            {               
                seeder.SeedData();
            }
            _uow.SaveChanges();
        }
    }
}