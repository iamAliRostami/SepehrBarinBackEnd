﻿using Hiwapardaz.SepehrBarin.Domain.BaseEntities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hiwapardaz.SepehrBarin.Domain.Features.Auth.Entities;

[Table(nameof(User))]
public class User: IHashableEntity
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = null!;
    public string DisplayName { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Mobile { get; set; } = null!;
    public bool MobileConfirmed { get; set; }
    public bool HasTwoStepVerification { get; set; }
    public int InvalidLoginAttemptCount { get; set; }
    public string? SerialNumber { get; set; }
    public DateTime? LatestLoginDateTime { get; set; }
    public DateTime? LockTimespan { get; set; }
    public Guid? PreviousId { get; set; }
    
    public virtual ICollection<User> InversePrevious { get; set; } = new List<User>();

    public virtual User? Previous { get; set; }

    public virtual ICollection<UserClaim> UserClaims { get; set; } = new List<UserClaim>();

    public virtual ICollection<UserLogin> UserLogins { get; set; } = new List<UserLogin>();

    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

    public virtual ICollection<UserToken> UserTokens { get; set; } = new List<UserToken>();
}