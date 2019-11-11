using GrindstoneTempoIntegration.Domain.Grindstone;
using GrindstoneTempoIntegration.Domain.Tempo;
using GrindstoneTempoIntegration.Domain.Tempo.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

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

        [TestMethod]
        public void PostWorklogsRequest_CtorTest()
        {
            var authorAccountId = "aabbbccc";
            var workItem = new WorkItem(new T() { i = "Test", n = "Test" });
            var timeEntry = new TimeEntry(new R()
            {
                s = new DateTime(2019, 1, 1, 0, 0, 0),
                e = new DateTime(2019, 1, 1, 0, 10, 0),
                n = "Test Description",
                i = "Test",
                t = "Test"
            });
            var timezoneAdjustment = 0;

            var postWorklogsRequest = new PostWorklogsRequest(authorAccountId, workItem, timeEntry, timezoneAdjustment);

            Assert.AreEqual(600, postWorklogsRequest.timeSpentSeconds);
            Assert.AreEqual("Test Description", postWorklogsRequest.description);

            timeEntry = new TimeEntry(new R()
            {
                s = new DateTime(2019, 1, 1, 0, 0, 0),
                e = new DateTime(2019, 1, 1, 0, 10, 1),
                i = "Test",
                t = "Test"
            });

            postWorklogsRequest = new PostWorklogsRequest(authorAccountId, workItem, timeEntry, timezoneAdjustment);

            Assert.AreEqual(660, postWorklogsRequest.timeSpentSeconds);
            Assert.AreEqual("Test", postWorklogsRequest.description);
        }
    }
}
