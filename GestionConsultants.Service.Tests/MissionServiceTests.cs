using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using FluentAssertions;
using GestionConsultants.Core.Constant;
using GestionConsultants.Core.Domain;
using GestionConsultants.Core.Enum;
using GestionConsultants.Core.Exception;
using GestionConsultants.Data.Context;
using GestionConsultants.Service.Mission;
using GestionConsultants.Service.Mission.Request;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace GestionConsultants.Service.Tests
{
    public class MissionServiceTests : TestingContext<MissionService>
    {
        public MissionServiceTests()
        {
            base.Setup();

            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        #region Placer consultant

        [Fact]
        public void PlacerConsultant_DoitLancerException_QuandPosteInterneNonRenseigne()
        {
            // Arrange
            var request = new PlacerConsultantRequest();

            // Act
            Action action = () => ClassUnderTest.PlacerConsultant(request);

            // Assert
            action
                .Should()
                .Throw<ValidationException>()
                .WithMessage(PlacerConsultantValidationMessage.PosteInterneNonRenseigne);
        }

        [Fact]
        public void PlacerConsultant_DoitLancerException_QuandRateNonRenseigne()
        {
            // Arrange
            var request = new PlacerConsultantRequest { PosteInterne = "Architecte" };

            // Act
            Action action = () => ClassUnderTest.PlacerConsultant(request);

            // Assert
            action
                .Should()
                .Throw<ValidationException>()
                .WithMessage(PlacerConsultantValidationMessage.RateNonRenseigne);
        }

        [Fact]
        public void PlacerConsultant_DoitLancerException_QuandConsultantExistePas()
        {
            // Arrange
            var request = new PlacerConsultantRequest { PosteInterne = "Architecte", ConsultantId = 1, Rate = 100 };
            InjectClassFor(InitializeContext());

            // Act
            Action action = () => ClassUnderTest.PlacerConsultant(request);

            // Assert
            action
                .Should()
                .Throw<ValidationException>()
                .WithMessage(PlacerConsultantValidationMessage.ConsultantIntrouvable);
        }

        [Fact]
        public void PlacerConsultant_DoitLancerException_QuandMissionExistePas()
        {
            // Arrange
            var request = new PlacerConsultantRequest { PosteInterne = "Architecte", ConsultantId = 1, MissionId = 1, Rate = 100 };
            var context = InitializeContext();
            var consultant = fixture.Build<Core.Domain.Consultant>().With(c => c.Id, 1).Create();

            context.Consultants.Add(consultant);
            context.SaveChanges();
            InjectClassFor(context);

            // Act
            Action action = () => ClassUnderTest.PlacerConsultant(request);

            // Assert
            action
                .Should()
                .Throw<ValidationException>()
                .WithMessage(PlacerConsultantValidationMessage.MissionIntrouvable);
        }

        [Fact]
        public void PlacerConsultant_DoitLancerException_QuandRateTropEleve()
        {
            // Arrange
            var request = new PlacerConsultantRequest { PosteInterne = "Architecte", ConsultantId = 1, MissionId = 1, Rate = 350 };
            var context = InitializeContext();

            var consultant = fixture
                .Build<Core.Domain.Consultant>()
                .With(c => c.Id, 1)
                .Create();
            var mission = fixture
                .Build<Core.Domain.Mission>()
                .With(m => m.Id, 1)
                .With(m => m.RateMaximum, 300)
                .Create();

            context.Consultants.Add(consultant);
            context.Missions.Add(mission);
            context.SaveChanges();
            InjectClassFor(context);

            // Act
            Action action = () => ClassUnderTest.PlacerConsultant(request);

            // Assert
            action
                .Should()
                .Throw<ValidationException>()
                .WithMessage(PlacerConsultantValidationMessage.RateTropEleve);
        }

        [Fact]
        public void PlacerConsultant_DoitLancerException_QuandRateTropEleveAvecCommission()
        {
            // Arrange
            var request = new PlacerConsultantRequest { PosteInterne = "Architecte", ConsultantId = 1, MissionId = 1, Rate = 280 };
            var context = InitializeContext();

            var consultant = fixture
                .Build<Core.Domain.Consultant>()
                .With(c => c.Id, 1)
                .With(c => c.Experience, Experience.Junior)
                .Create();
            var mission = fixture
                .Build<Core.Domain.Mission>()
                .With(m => m.Id, 1)
                .With(m => m.RateMaximum, 300)
                .Create();

            context.Consultants.Add(consultant);
            context.Missions.Add(mission);
            context.SaveChanges();
            InjectClassFor(context);

            // Act
            Action action = () => ClassUnderTest.PlacerConsultant(request);

            // Assert
            action
                .Should()
                .Throw<ValidationException>()
                .WithMessage(PlacerConsultantValidationMessage.RateTropEleve);
        }

        [Fact]
        public void PlacerConsultant_DoitLancerException_QuandExperienceNonSuffisante()
        {
            // Arrange
            var request = new PlacerConsultantRequest { PosteInterne = "Architecte", ConsultantId = 1, MissionId = 1, Rate = 280 };
            var context = InitializeContext();

            var consultant = fixture
                .Build<Core.Domain.Consultant>()
                .With(c => c.Id, 1)
                .With(c => c.Experience, Experience.Junior)
                .Create();
            var mission = fixture
                .Build<Core.Domain.Mission>()
                .With(m => m.Id, 1)
                .With(m => m.RateMaximum, 500)
                .With(m => m.ExperienceMinimumRequise, Experience.Medior)
                .Create();

            context.Consultants.Add(consultant);
            context.Missions.Add(mission);
            context.SaveChanges();
            InjectClassFor(context);

            // Act
            Action action = () => ClassUnderTest.PlacerConsultant(request);

            // Assert
            action
                .Should()
                .Throw<ValidationException>()
                .WithMessage(PlacerConsultantValidationMessage.ExperienceNonSuffisante);
        }

        [Fact]
        public void PlacerConsultant_DoitEtreSeuleMissionActive_QuandFonctionne()
        {
            // Arrange
            var request = new PlacerConsultantRequest { PosteInterne = "Architecte", ConsultantId = 1, MissionId = 1, Rate = 280 };
            var context = InitializeContext();
            var missionsConsultantst = context.MissionConsultants.ToList();

            var consultant = fixture
                .Build<Core.Domain.Consultant>()
                .With(c => c.Id, 1)
                .With(c => c.Experience, Experience.Senior)
                .With(c => c.Missions, new List<MissionConsultant>())
                .Create();
            var mission = fixture
                .Build<Core.Domain.Mission>()
                .With(m => m.Id, 1)
                .With(m => m.RateMaximum, 500)
                .With(m => m.ExperienceMinimumRequise, Experience.Medior)
                .With(c => c.MissionsConsultants, new List<MissionConsultant>())
                .Create();
            var missionConsultant = fixture
                .Build<MissionConsultant>()
                .With(c => c.Consultant, consultant)
                .With(c => c.ConsultantId, 1)
                .With(c => c.Mission, mission)
                .With(c => c.MissionId, 2)
                .With(c => c.EstActif, true)
                .Create();

            context.Consultants.Add(consultant);
            context.Missions.Add(mission);
            context.MissionConsultants.Add(missionConsultant);
            context.SaveChanges();
            InjectClassFor(context);

            // Act
            ClassUnderTest.PlacerConsultant(request);

            // Assert
            var missionsConsultants = context.MissionConsultants.ToList();
            missionsConsultants.Count.Should().Be(2);
            missionsConsultants.Count(mc => mc.EstActif).Should().Be(1);
        }

        #endregion

        #region Calculer rate

        [Theory]
        [InlineData(Experience.Junior, 15)]
        [InlineData(Experience.Medior, 10)]
        [InlineData(Experience.Senior, 5)]
        public void CalculerCommissionEntreprise_DoitRenvoyerLeBonRate(Experience experience, double commissionAttendue)
        {
            // Act
            var commissionEntreprise = ClassUnderTest.CalculerCommissionEntreprise(experience);

            // Assert
            commissionEntreprise.Should().Be(commissionAttendue);
        }

        #endregion

        #region Méthodes privées

        private ConsultantContext InitializeContext()
        {
            var options = new DbContextOptionsBuilder<ConsultantContext>()
                .UseInMemoryDatabase(databaseName: "ConsultantContextDatabase")
                .Options;

            var context = new ConsultantContext(options);
            context.Database.EnsureDeleted();

            context.SaveChanges();
            return context;
        }

        #endregion

    }
}