using System;
using AV.Common.Entities;
using AV.Common.Interfaces;
using AV.Common.Interfaces.Repositories;
using AV.Common.Interfaces.UnitOfWorks;
using AV.Persistence.EntityFramework.UnitOfWorks;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Moq;
using NSubstitute;
using NUnit.Framework;

namespace AV.Persistence.EntityFramework.Tests
{
    [TestFixture]
    public class InstructionUoWTests
    {
        private InstructionUoW _instructionUoW;
        private readonly Mock<UserManager<User>> _userManagerMock;
        private readonly Mock<IUserManagerUnitOfWork> _userManagerUoWMock;
        private readonly Mock<IAccountsRepository> _accountsRepositoryMock;
        private readonly Mock<IInstructionRepository> _instructionRepositoryMock;
        private readonly Mock<ILocationsRepository> _locationsRepositoryMock;
        

        public InstructionUoWTests()
        {
            _userManagerMock = MockHelpers.MockUserManager<User>();
            _userManagerUoWMock = new Mock<IUserManagerUnitOfWork>();
            _accountsRepositoryMock = new Mock<IAccountsRepository>();
            _instructionRepositoryMock = new Mock<IInstructionRepository>();
            _locationsRepositoryMock = new Mock<ILocationsRepository>();

            _instructionUoW = new InstructionUoW(Substitute.For<IdentityDbContext<User, Role, Guid>>(),
               _userManagerMock.Object,
                _userManagerUoWMock.Object,
                _accountsRepositoryMock.Object,
                _instructionRepositoryMock.Object,
                _locationsRepositoryMock.Object);
        }

        [Test]
        public void Should_fail_validation_when_instruction_is_null()
        {
            //Arrange
            // Act
            var sut = _instructionUoW.CreateInstructionValidator(null);
            //Assert
            sut.IsValid.Should().BeFalse();
        }

        [Test]
        public void Should_fail_validation_when_issuer_is_not_supplied()
        {
            //Arrange
            // Act
            var instruction = CreateValidInstruction();
            instruction.IssuerId = Guid.Empty;
            var sut = _instructionUoW.CreateInstructionValidator(instruction);
            //Assert
            sut.IsValid.Should().BeFalse();
        }

        [Test]
        public void Should_pass_validation_when_issuer_is_supplied()
        {
            //Arrange
            // Act
            var instruction = CreateValidInstruction();
            var sut = _instructionUoW.CreateInstructionValidator(instruction);
            //Assert
            sut.IsValid.Should().BeTrue();
        }

        [Test]
        public void Should_pass_validation_when_issuer_has_active_account()
        {
            //Arrange
            // Act
            var instruction = CreateValidInstruction();
            var sut = _instructionUoW.CreateInstructionValidator(instruction);
            //Assert
            sut.IsValid.Should().BeTrue();
        }

        private Instruction CreateValidInstruction()
        {
            return new Instruction
            {
                IssuerId = Guid.NewGuid(),
                AccountId = new Guid("1085ccc3-87d1-4c7e-9f6c-e73bc4d84f94"),
                Account = new Account
                {
                    Id = new Guid("1085ccc3-87d1-4c7e-9f6c-e73bc4d84f94"),
                    ExpiryDate = DateTime.Now.AddMonths(3),
                    IsCorporate = true,
                    VerifiedByUserId = Guid.NewGuid()
                }
                
            };
        }
    }
}
