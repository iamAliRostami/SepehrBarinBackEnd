﻿namespace Hiwapardaz.SepehrBarin.Domain.Features.Auth.Entities;

public class InvalidLoginReason
{
    public short Id { get; set; }

    public string Title { get; set; } = null!;

    public virtual ICollection<UserLogin> UserLogins { get; set; } = new List<UserLogin>();
}
