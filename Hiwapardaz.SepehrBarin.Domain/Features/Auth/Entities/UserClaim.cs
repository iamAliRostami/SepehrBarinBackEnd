﻿using Hiwapardaz.SepehrBarin.Domain.BaseEntities;
using Hiwapardaz.SepehrBarin.Persistence.Constants.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hiwapardaz.SepehrBarin.Domain.Features.Auth.Entities;

[Table(nameof(UserClaim))]
public class UserClaim: IHashableEntity
{
    public int Id { get; set; }
    public Guid UserId { get; set; }
    public ClaimType ClaimTypeId { get; set; }
    public string ClaimValue { get; set; } = null!;
    public Guid InsertGroupId { get; set; }
    public Guid RemoveGroupId { get; set; }
    public DateTime ValidFrom { get; set; }
    public DateTime? ValidTo { get; set; }
    public string InsertLogInfo { get; set; } = null!;
    public string? RemoveLogInfo { get; set; }

    public virtual User User { get; set; } = null!;
}