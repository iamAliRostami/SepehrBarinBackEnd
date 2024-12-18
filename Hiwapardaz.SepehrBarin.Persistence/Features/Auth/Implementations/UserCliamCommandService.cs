﻿using Aban360.Common.Extensions;
using Hiwapardaz.SepehrBarin.Domain.Features.Auth.Entities;
using Hiwapardaz.SepehrBarin.Persistence.Contexts.UnitOfWork;
using Hiwapardaz.SeprhrBarin.Persistence.Features.Auth.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hiwapardaz.SeprhrBarin.Persistence.Features.Auth.Implementations
{
    public class UserCliamCommandService : IUserClaimCommandService
    {
        private readonly IUnitOfWork _uow;
        private readonly DbSet<UserClaim> _userClaims;

        public UserCliamCommandService(IUnitOfWork uow)
        {
            _uow = uow;
            _uow.NotNull(nameof(_uow));

            _userClaims = _uow.Set<UserClaim>();
            _userClaims.NotNull(nameof(_userClaims));
        }
        public async Task Add(ICollection<UserClaim> userClaims)
        {
            await _userClaims.AddRangeAsync(userClaims);
        }

        public void Remove(ICollection<UserClaim> userClaims, string logInfo)
        {
            Guid operationGropuId = Guid.NewGuid();
            userClaims.ForEach(userClaim => Remove(userClaim, logInfo, operationGropuId));
        }
        private void Remove(UserClaim userClaim, string logInfo, Guid operationGropuId)
        {
            userClaim.ValidTo = DateTime.Now; ;
            userClaim.RemoveLogInfo = logInfo;
            userClaim.RemoveGroupId = operationGropuId;
        }
    }
}
