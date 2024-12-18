﻿//using Aban360.Common.Categories.UseragentLog;
//using Aban360.Common.Extensions;
//using Hiwapardaz.SepehrBarin.Domain.Constants;
//using Hiwapardaz.SepehrBarin.Domain.Features.Auth.Entities;
//using Hiwapardaz.SepehrBarin.Persistence.Contexts.UnitOfWork;
//using Hiwapardaz.SepehrBarin.Persistence.DbSeeder.Contracts;
//using Microsoft.EntityFrameworkCore;

//namespace Hiwapardaz.SepehrBarin.Persistence.DbSeeder.Implementations
//{
//    public class RoleSeeder: IDataSeeder
//    {
//        public int Order { get; set; } = 5;
//        private readonly IUnitOfWork _uow;
//        private readonly DbSet<Role> _roles;
//        public RoleSeeder(IUnitOfWork uow)
//        {
//            _uow= uow;
//            _uow.NotNull(nameof(uow));

//            _roles=_uow.Set<Role>();
//            _roles.NotNull(nameof(_roles));
//        }

//        public void SeedData()
//        {
//            if (!_roles.Any())
//            {
//                var admin = new Role()
//                {
//                    Hash = string.Empty,
//                    InsertLogInfo = LogInfoJson.Get(),
//                    IsRemovable = false,
//                    SensitiveInfo = false,
//                    Title = "راهبر سامانه",
//                    Name = BaseRoles.Admin,
//                    ValidFrom = DateTime.Now
//                };
//                var ai = new Role()
//                {
//                    Hash = string.Empty,
//                    InsertLogInfo = LogInfoJson.Get(),
//                    IsRemovable = false,
//                    SensitiveInfo = false,
//                    Title = "دستیار هوشمند",
//                    Name = BaseRoles.Ai,
//                    ValidFrom = DateTime.Now
//                };
//                var programmer = new Role()
//                {
//                    Hash = string.Empty,
//                    InsertLogInfo = LogInfoJson.Get(),
//                    IsRemovable = false,
//                    SensitiveInfo = false,
//                    ValidFrom = DateTime.Now,
//                    Name = BaseRoles.Programmer,
//                    Title = "برنامه نویس"
//                };
//                var roles= new Role[] {admin, ai, programmer};
//                _roles.AddRange(roles);
//                _uow.SaveChanges();
//            }
//        }
//    }
//}
