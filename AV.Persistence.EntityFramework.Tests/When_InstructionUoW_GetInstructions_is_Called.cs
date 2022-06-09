using System;
using System.Collections.Generic;
using AV.Common.Entities;
using AV.Common.Interfaces;
using AV.Common.Interfaces.Repositories;
using AV.Common.Interfaces.UnitOfWorks;
using AV.Persistence.EntityFramework.UnitOfWorks;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NSubstitute;
using NUnit.Framework;

namespace AV.Persistence.EntityFramework.Tests
{
    [TestFixture]
    public class When_InstructionUoW_GetInstructions_Is_Called
    {
        private InstructionUoW _instructionUoW;

        [SetUp]
        public void SetupInstructionUoW()
        {
            _instructionUoW = new InstructionUoW(Substitute.For<IdentityDbContext<User, Role, Guid>>(),
                new UserManagerStub(),
                Substitute.For<IUserManagerUnitOfWork>(),
                Substitute.For<IAccountsRepository>(),
                Substitute.For<IInstructionRepository>(),
                Substitute.For<ILocationsRepository>());
        }

        [Test]
        public void Should_fail_validation_when_requester_is_null()
        {
            //Arrange
            // Act
            var sut = _instructionUoW.GetInstructionValidator(null);
            //Assert
            sut.IsValid.Should().BeFalse();
            WriteMessagesToConsole(sut.Messages);
        }

        [Test]
        public void Should_fail_validation_when_request_is_not_active()
        {
            //Arrange
            // Act
            var requester = CreateValidUser();
            requester.Active = false;
            var sut = _instructionUoW.GetInstructionValidator(requester);
            //Assert
            sut.IsValid.Should().BeFalse();
            WriteMessagesToConsole(sut.Messages);
        }

        [Test]
        public void Should_fail_validation_when_requester_has_no_account()
        {
            //Arrange
            // Act
            var requester = CreateValidUser();
            requester.Accounts = null;
            var sut = _instructionUoW.GetInstructionValidator(requester);
            //Assert
            sut.IsValid.Should().BeFalse();
            WriteMessagesToConsole(sut.Messages);
        }

        [Test]
        public void Should_fail_validation_when_requester_has_null_accounts()
        {
            //Arrange
            // Act
            var requester = CreateValidUser();
            requester.Active = false;
            var sut = _instructionUoW.GetInstructionValidator(requester);
            //Assert
            sut.IsValid.Should().BeFalse();
            WriteMessagesToConsole(sut.Messages);
        }

        [Test]
        public void Should_fail_validation_when_requester_has_no_active_account()
        {
            //Arrange
            // Act
            var requester = CreateValidUser();
            foreach (var account in requester.Accounts)
            {
                account.ExpiryDate = account.ExpiryDate = DateTime.Now.AddDays(-3);
            }
            var sut = _instructionUoW.GetInstructionValidator(requester);
            //Assert
            sut.IsValid.Should().BeFalse();
            WriteMessagesToConsole(sut.Messages);
        }

        [Test]
        public void Should_fail_validation_when_requester_no_corporate_account()
        {
            //Arrange
            // Act
            var requester = CreateValidUser();
            foreach (var account in requester.Accounts)
            {
                account.IsCorporate = false;
                account.IsValuer = false;
            }
            var sut = _instructionUoW.GetInstructionValidator(requester);
            //Assert
            sut.IsValid.Should().BeFalse();
            foreach (var message in sut.Messages)
            {
                Console.WriteLine(message);
            }
        }

        [Test]
        public void Should_pass_validation_when_requester_has_corporate_account()
        {
            //Arrange
            // Act
            var requester = CreateValidUser();
            foreach (var account in requester.Accounts)
            {
                account.IsCorporate = true;
                account.IsValuer = false;
            }
            var sut = _instructionUoW.GetInstructionValidator(requester);
            //Assert
            sut.IsValid.Should().BeTrue();
            
        }

        [Test]
        public void Should_pass_validation_when_requester_no_valuer_account()
        {
            //Arrange
            // Act
            var requester = CreateValidUser();
            foreach (var account in requester.Accounts)
            {
                account.IsCorporate = false;
                account.IsValuer = true;
            }
            var sut = _instructionUoW.GetInstructionValidator(requester);
            //Assert
            sut.IsValid.Should().BeTrue();
        }

        [Test]
        public void Should_pass_validation_when_user_is_active_with_active_transaction_account()
        {
            //Arrange
            // Act
            var requester = CreateValidUser();
            var sut = _instructionUoW.GetInstructionValidator(requester);
            //Assert
            sut.IsValid.Should().BeTrue();
        }

        private User CreateValidUser()
        {
            return new User
            {
                Active = true,
                Accounts = new List<Account>
                {
                    new Account
                    {
                        ExpiryDate = DateTime.UtcNow.AddMonths(7),
                        VerifiedByUserId = Guid.NewGuid(),
                        IsCorporate = true,
                    }
                }
            };
        }
        private static void WriteMessagesToConsole(IList<string> messages)
        {
            foreach (var message in messages)
            {
                Console.WriteLine(message);
            }
        }
    }

    public class UserManagerStub : UserManager<User>
    {
        public UserManagerStub(IUserStore<User> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<User> passwordHasher, IEnumerable<IUserValidator<User>> userValidators, IEnumerable<IPasswordValidator<User>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<User>> logger) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
        }

        public UserManagerStub()
        : base(
            Substitute.For<IUserStore<User>>(), 
            Substitute.For<IOptions<IdentityOptions>>(),
            Substitute.For<IPasswordHasher<User>>(), 
            Substitute.For<IEnumerable<IUserValidator<User>>>(), 
            Substitute.For<IEnumerable<IPasswordValidator<User>>>(),
            Substitute.For<ILookupNormalizer>(),
            Substitute.For<IdentityErrorDescriber>(),
            Substitute.For<IServiceProvider>(),
            Substitute.For<ILogger<UserManager<User>>>()

            )
        {
        }
    }
}