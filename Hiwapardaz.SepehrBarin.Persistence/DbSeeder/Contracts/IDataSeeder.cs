﻿namespace Hiwapardaz.SepehrBarin.Persistence.DbSeeder.Contracts
{
    internal interface IDataSeeder
    {
        int Order { set; get; }
        void SeedData();
    }
}
