﻿using System.Security.Claims;

namespace Hiwapardaz.SepehrBarin.Domain.Features.Auth.Dto.Queries
{
    public class JwtTokenData
    {
        public Guid UserId { get; set; }
        public string AccessToken { get; set; } = default!;
        public string RefreshToken { get; set; } = default!;
        public string RefreshTokenSerial { get; set; } = default!;
        public IEnumerable<Claim>? Claims { get; set; }
    }
}
