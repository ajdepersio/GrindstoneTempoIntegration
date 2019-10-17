using GrindstoneTempoIntegration.Domain.Tempo;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GrindstoneTempoIntegration.Domain.Tests
{
    [TestClass]
    public class TempoRepositoryTest
    {
        [TestInitialize]
        public void Startup()
        {
            TempoRepository.BearerToken = "aaabbbccc";
        }

        [TestMethod]
        public void GetTeamsTest()
        {
            var repo = new TempoRepository();
            var response = repo.GetTeams();

            Assert.IsNotNull(response);
            Assert.IsTrue(response.results.Count > 0);
        }

        [TestMethod]
        public void GetMembersTest()
        {
            var repo = new TempoRepository();
            var teams = repo.GetTeams();
            var response = repo.GetMembers(teams.results[0].id);

            Assert.IsNotNull(response);
            Assert.IsTrue(response.results.Count > 0);
        }

    }
}
