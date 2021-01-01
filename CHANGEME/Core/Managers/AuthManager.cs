using Infrastructure.Config;
using Core.Database.Entities;
using Core.Database.Repositories;
using Infrastructure.Base;
using Core.Dtos;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Infrastructure.Exceptions;

namespace Core.Managers
{
    public class AuthManager : BaseManager
    {
        private UsersRepository usersRepository { get; set; }
        private AccountsRepository accountsRepository { get; set; }

        public AuthManager(
            UsersRepository usersRepository,
            AccountsRepository accountsRepository
        )
        {
            this.usersRepository = usersRepository;
            this.accountsRepository = accountsRepository;
        }

        public AuthStatusDto Login(AuthLoginDto payload)
        {
            var entity = accountsRepository.FindByEmail(payload.Email);

            if (entity == null)
                return AuthenticationFailure("Username or password is wrong");

            if (!(HashPassword(payload.Password, Convert.FromBase64String(entity.PasswordSalt)) == entity.PasswordHash))
                return AuthenticationFailure("Username or password is wrong");

            var token = GenerateJwtToken(entity.Id);

            return AuthenticationSucceeded("Successfully authenticated", token);
        }

        public AuthStatusDto Register(AuthRegisterDto payload)
        {
            try
            {
                BreakIfAccountExists(payload.Email);

                var newAccount = CreateAccount(payload);

                var token = GenerateJwtToken(newAccount.Id);
                return AuthenticationSucceeded("Successfully registered", token);
            }
            catch (AccountExistingException)
            {
                return AuthenticationFailure("Account using this Email is already registered");
            }
        }

        public Account RegisterAndReturnAccount(AuthRegisterDto payload)
        {
            BreakIfAccountExists(payload.Email);

            var newAccount = CreateAccount(payload);

            return newAccount;
        }

        private Account CreateAccount(AuthRegisterDto payload)
        {
            var newAccount = CreateAccountEntity(payload);
            accountsRepository.Insert(newAccount);

            return newAccount;
        }

        private void BreakIfAccountExists(string email)
        {
            var entity = accountsRepository.FindByEmail(email);

            if (entity != null)
                throw new AccountExistingException($"Duplicate email exception: {email}");
        }

        private Account CreateAccountEntity(AuthRegisterDto payload)
        {
            var salt = GenerateSalt();
            var newAccount = new Account
            {
                CompanyId = payload.CompanyId,
                FirstName = payload.FirstName,
                LastName = payload.LastName,
                Email = payload.Email,

                PasswordSalt = Convert.ToBase64String(salt),
                PasswordHash = HashPassword(payload.Password, salt),

                User = new User
                {
                    CompanyId = payload.CompanyId,
                    FirstName = payload.FirstName,
                    LastName = payload.LastName,
                    Email = payload.Email,

                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };

            return newAccount;
        }

        private AuthStatusDto AuthenticationFailure(string message)
        {
            return new AuthStatusDto
            {
                IsSuccessful = false,
                Message = message
            };
        }

        private AuthStatusDto AuthenticationSucceeded(string message, string token)
        {
            return new AuthStatusDto
            {
                IsSuccessful = true,
                Message = message,
                Token = token
            };
        }

        private string HashPassword(string password, byte[] salt)
        {
            var bytes = KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8);

            return Convert.ToBase64String(bytes);
        }

        private byte[] GenerateSalt()
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            return salt;
        }

        private string GenerateJwtToken(int accountId)
        {
            var basicUserClaims = new List<Claim>()
            {
                new Claim("AccountId", accountId.ToString())
            };

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppConfig.AuthSettings.JwtKey));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                AppConfig.AuthSettings.JwtIssuer,
                AppConfig.AuthSettings.JwtIssuer,
                claims: basicUserClaims,
                expires: DateTime.Now.AddDays(5),
                signingCredentials: signinCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}