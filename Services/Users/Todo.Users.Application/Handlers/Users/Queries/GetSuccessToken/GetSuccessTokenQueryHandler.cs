using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Todo.Users.Core.Database;
using Todo.Users.Core.Exceptions;
using Todo.Users.Core.Helpers.Hasher;
using Todo.Users.Core.Models;
using Todo.Users.Core.Options;

namespace Todo.Users.Application.Handlers.Users.Queries.GetSuccessToken
{
    public class GetSuccessTokenQueryHandler : IRequestHandler<GetSuccessTokenQuery, string>
    {
        private readonly ILogger<GetSuccessTokenQueryHandler> logger;

        private readonly IDatabaseContext database;

        private readonly AuthOptions auth;

        public GetSuccessTokenQueryHandler(IDatabaseContext database, IOptions<AuthOptions> auth, ILogger<GetSuccessTokenQueryHandler> logger) =>
            (this.database, this.auth, this.logger) = (database, auth.Value, logger);

        public async Task<string> Handle(GetSuccessTokenQuery request, CancellationToken cancellationToken)
        {
            User user = await database.Users.FirstOrDefaultAsync(x => x.Email == request.Email);
            ClaimsIdentity identity = GetIdentity(user);

            if (identity == null)
            {
                throw new Exception(ExceptionStrings.WrongEmailOrPassword);
            }

            string hash = PasswordHasher.Hash(request.Password, user.PasswordSalt);

            if (hash != user.PasswordHash)
            {
                throw new Exception(ExceptionStrings.WrongEmailOrPassword);
            }

            DateTime now = DateTime.UtcNow;
            JwtSecurityToken jwt = new JwtSecurityToken(
                    issuer: auth.Issuer,
                    audience: auth.Audience,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(auth.Lifetime)),
                    signingCredentials: new SigningCredentials(auth.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            string token = new JwtSecurityTokenHandler().WriteToken(jwt);

            logger.LogInformation("The user has successfully entered");

            return token;
        }

        private ClaimsIdentity GetIdentity(User user)
        {
            if (user == null)
            {
                return null;
            }

            List<Claim> claims = new List<Claim> 
            { 
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Id.ToString()), 
            };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(
                claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType
            );

            return claimsIdentity;
        }
    }
}
