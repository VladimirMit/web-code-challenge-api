using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.CodeChallenge.Application.Features.Participants;
using Application.CodeChallenge.Domain.Entities;
using Application.CodeChallenge.Domain.Enums;
using Application.CodeChallenge.Domain.Services;
using Application.CodeChallenge.Domain.ValueObjects;
using Application.CodeChallenge.Infrastructure.Database;
using AutoFixture;
using AutoFixture.Kernel;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;

namespace Application.CodeChallenge.Tests.Application.Participants
{
    internal class AddSolutionTests
    {
        private Fixture _fixture;
        
        [SetUp]
        public void SetUp()
        {
            _fixture = new Fixture();
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            _fixture.Customizations.Add(new PropertyRequestRelay());
        }
        
        public DataContext GetDbContext(Action<DataContext> setup)
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase($"Test_{Guid.NewGuid()}")
                .Options;

            var context = new DataContext(options);

            setup(context);

            context.SaveChanges();

            return context;
        }

        [Test]
        public async Task AddSolution_InvokeSolutionCheckServiceExactlyOnce_Test()
        { 
            var participant = _fixture.Create<Participant>();
            var codeTask = _fixture.Create<CodeTask>();
            var context = GetDbContext(ctx =>
            {
                ctx.Participants.Add(participant);
                ctx.CodeTask.Add(codeTask);
            });
            
            var checkResolverMock = new Mock<ICheckSolutionService>();

            var handler = new AddSolution.Handler(context, (_) => checkResolverMock.Object);

            var command = new AddSolution.Command(participant.Id, codeTask.Id, Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString());

            await handler.Handle(command, CancellationToken.None);

            checkResolverMock.Verify(
                s => s.Check(command.Code, codeTask.TestCases,
                    codeTask.InputType, CancellationToken.None), Times.Once);

        }

        [TestCase(Status.Success)]
        [TestCase(Status.Error)]
        [TestCase(Status.FailedToExecute)]
        public async Task AddSolution_StatusShouldMatch_Test(Status status)
        {
            var participant = _fixture.Create<Participant>();
            var codeTask = _fixture.Create<CodeTask>();
            
            var context = GetDbContext(ctx =>
            {
                ctx.Participants.Add(participant);
                ctx.CodeTask.Add(codeTask);
            });

            var checkResolverMock = new Mock<ICheckSolutionService>();
            checkResolverMock.Setup(c => c.Check(It.IsAny<string>(),
                    It.IsAny<IReadOnlyCollection<CodeTaskTestCase>>(),
                    It.IsAny<InputType>(), CancellationToken.None))
                .ReturnsAsync(status);

            var handler = new AddSolution.Handler(context, (_) => checkResolverMock.Object);
            var command = new AddSolution.Command(participant.Id, codeTask.Id, Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString());

            var response = await handler.Handle(command, CancellationToken.None);

            var dbStatus = context.Participants.First().Solutions.Single(s => s.Code == command.Code).Status;
            
            Assert.AreEqual(status, response.Status);
            Assert.AreEqual(status, dbStatus);
        }
        
        [Test]
        public async Task AddSolution_ShouldUpdateExisting_Test()
        {
            var participant = _fixture.Create<Participant>();
            var codeTask = _fixture.Create<CodeTask>();

            participant.AddSolution(codeTask, "languageName", Guid.NewGuid().ToString(), Status.FailedToExecute);
            
            var context = GetDbContext(ctx =>
            {
                ctx.Participants.Add(participant);
                ctx.CodeTask.Add(codeTask);
            });

            var expectedStatus = Status.Success;
            var expectedCode = Guid.NewGuid().ToString();
            
            var checkResolverMock = new Mock<ICheckSolutionService>();
            checkResolverMock.Setup(c => c.Check(It.IsAny<string>(),
                    It.IsAny<IReadOnlyCollection<CodeTaskTestCase>>(),
                    It.IsAny<InputType>(), CancellationToken.None))
                .ReturnsAsync(expectedStatus);

            var handler = new AddSolution.Handler(context, (_) => checkResolverMock.Object);
            var command = new AddSolution.Command(participant.Id, codeTask.Id, expectedCode,
                Guid.NewGuid().ToString());

            var response = await handler.Handle(command, CancellationToken.None);

            var dbSolution = context.Participants.First(p => p.Id == participant.Id).Solutions
                .Single(s => s.Task?.Id == codeTask.Id);

            Assert.AreEqual(expectedStatus, response.Status);
            Assert.AreEqual(expectedStatus, dbSolution.Status);
            Assert.AreEqual(expectedCode, dbSolution.Code);
        }
    }
}
