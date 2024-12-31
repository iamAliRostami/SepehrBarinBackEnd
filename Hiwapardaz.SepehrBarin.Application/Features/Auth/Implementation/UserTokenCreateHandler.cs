﻿using AutoMapper;
using Hiwapardaz.SepehrBarin.Application.Features.Auth.Contracts;
using Hiwapardaz.SepehrBarin.Common.Extensions;
using Hiwapardaz.SepehrBarin.Domain.Features.Auth.Dto.Queries;
using Hiwapardaz.SepehrBarin.Domain.Features.Auth.Entities;
using Hiwapardaz.SeprhrBarin.Persistence.Features.Auth.Contracts;
using Microsoft.Extensions.Options;

namespace Hiwapardaz.SepehrBarin.Application.Features.Auth.Implementation
{
    public class UserTokenCreateHandler : IUserTokenCreateHandler
    {
        private readonly ITokenStoreCommandService _tokeStoreService;
        private readonly IMapper _mapper;
        private readonly IOptionsSnapshot<BearerTokenOptions> _configuration;
        public UserTokenCreateHandler(
            ITokenStoreCommandService tokenStoreService,
            IMapper mapper,
            IOptionsSnapshot<BearerTokenOptions> configuration)
        {
            _tokeStoreService = tokenStoreService;
            _tokeStoreService.NotNull(nameof(tokenStoreService));

            _mapper = mapper;
            _mapper.NotNull(nameof(mapper));

            _configuration = configuration;
            _configuration.NotNull(nameof(configuration));
        }
        public async Task Handle(JwtTokenData jwtTokenData, string? refreshTokenSourceSerial, CancellationToken cancellationToken)
        {
            UserToken userToken = await CreateUserToken(jwtTokenData, refreshTokenSourceSerial);
            await _tokeStoreService.RemoveTokensWithSameRefreshTokenSource(jwtTokenData.UserId);
            await _tokeStoreService.Add(userToken);
        }
        private async Task<UserToken> CreateUserToken(JwtTokenData jwtTokenData, string? refreshTokenSourceSerial)
        {
            var now = DateTime.Now;
            var userToken = new UserToken
            {
                UserId = jwtTokenData.UserId,
                // Refresh token handles should be treated as secrets and should be stored hashed
                RefreshTokenIdHash = await SecurityOperations.GetSha256Hash(jwtTokenData.RefreshTokenSerial),
                RefreshTokenIdHashSource =
                    string.IsNullOrWhiteSpace(refreshTokenSourceSerial) ?
                    null :
                    await SecurityOperations.GetSha256Hash(refreshTokenSourceSerial),
                AccessTokenHash = await SecurityOperations.GetSha256Hash(jwtTokenData.AccessToken),
                RefreshTokenExpiresDateTime = now.AddMinutes(_configuration.Value.RefreshTokenExpirationMinutes),
                AccessTokenExpiresDateTime = now.AddMinutes(_configuration.Value.AccessTokenExpirationMinutes)
            };
            return userToken;
        }

    }
}
