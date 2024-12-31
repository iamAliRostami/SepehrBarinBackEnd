﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Hiwapardaz.SepehrBarin.Domain.Features.Auth.Entities;

[Table(nameof(UserRole))]
public class UserRole
{
    public int Id { get; set; }

    public Guid UserId { get; set; }

    public int RoleId { get; set; }

    public DateTime ValidFrom { get; set; }

    public DateTime? ValidTo { get; set; }

    public string InsertLogInfo { get; set; } = null!;

    public string? RemoveLogInfo { get; set; }

    public virtual Role Role { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
