using Core.Database.Entities;
using Core.Database.Repositories;
using Core.Models;
using Infrastructure.Base;
using Infrastructure.Exceptions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Core.Managers
{
    public class DataSeedingManager : BaseManager
    {
        AuthManager authManager;
        DataSeedingRepository mainRepository;

        public DataSeedingManager(
            AuthManager authManager,
            DataSeedingRepository mainRepository,

            ILogger<DataSeedingManager> logger
        ) : base(logger)
        {
            this.authManager = authManager;
            this.mainRepository = mainRepository;
        }

        public Company SeedNewCompany()
        {
            var company = mainRepository.SeedCompany();
            var accounts = SeedAccounts(company.Id, 10);

            return company;
        }

        public List<Account> SeedAccounts(int companyId, int accountsNumber)
        {
            var newAccounts = new List<Account>();

            for (int i = 0; i < accountsNumber; i++)
            {
                try
                {
                    var randomSeed = GenerateRandomString(10);
                    var generatedEmail = $"test_{randomSeed}@email.com";
                    var registerAccountDto = new AuthRegisterDto
                    {
                        CompanyId = companyId,
                        FirstName = "Test First Name",
                        LastName = "Test Last Name",
                        Password = "pass",
                        Email = generatedEmail,
                    };

                    var newAccount = authManager.RegisterAndReturnAccount(registerAccountDto);
                    newAccounts.Add(newAccount);
                }
                catch (AccountExistingException ex)
                {
                    logger.LogError(ex, $"Account email generation collision");
                }
            }

            return newAccounts;
        }

        private string GenerateRandomString(int stringSize)
        {
            var alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[stringSize];
            var unixTimestampSeed = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            var random = new Random(unixTimestampSeed);

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = alphabet[random.Next(alphabet.Length)];
            }

            return new String(stringChars);
        }
    }
}
