﻿using Hiwapardaz.SepehrBarin.Persistence.Extensions;
using Hiwapardaz.SepehrBarin.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Hiwapardaz.SepehrBarin.Persistence.Contexts.Implementation
{
    public partial class SepehrBarinDbContext : BaseDbContext
    {
        public SepehrBarinDbContext()
        {
        }

        public SepehrBarinDbContext(DbContextOptions<SepehrBarinDbContext> options)
            : base(options)
        {
        }       
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //var connectionString = MigrationRunner.GetConnectionInfo().Item1;
                //optionsBuilder.UseSqlServer(connectionString,
                //        serverDbContextOptionsBuilder =>
                //        {
                //            var minutes = (int)TimeSpan.FromMinutes(3).TotalSeconds;
                //            serverDbContextOptionsBuilder.CommandTimeout(minutes);
                //            //serverDbContextOptionsBuilder.EnableRetryOnFailure();
                //        });
                //optionsBuilder.AddInterceptors(new PersianYeKeCommandInterceptor());
                //optionsBuilder.AddInterceptors(new RowLevelAuthenticitySaveChangeInterceptor());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}