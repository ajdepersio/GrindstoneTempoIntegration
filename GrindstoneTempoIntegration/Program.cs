using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.IO;
using GrindstoneTempoIntegration.Domain.Grindstone;
using System.Collections.Generic;
using System.Linq;
using GrindstoneTempoIntegration.Domain.Tempo;
using GrindstoneTempoIntegration.Domain.Tempo.Entities;
using GrindstoneTempoIntegration.Domain.Tempo.Models;

namespace GrindstoneTempoIntegration
{
    class Program
    {
        public static IConfigurationRoot Configuration;

        public static HashSet<WorkItem> WorkItems;

        public static HashSet<TimeEntry> TimeEntries;

        public static List<PostWorklogsRequest> PostWorklogsRequests = new List<PostWorklogsRequest>();

        private static string TempoAccountId;

        private static DateTime StartDateTime;

        private static DateTime EndDateTime;


        static void Main(string[] args)
        {
            string startArg = "";
            string endArg = "";
            if (args.Length == 2)
            {
                startArg = args[0];
                endArg = args[1];
            }
            ProcessArguments(startArg, endArg);

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            Configuration = builder.Build();
            TempoRepository.BearerToken = Configuration["TempoApiKey"];

            LoadGrindstoneData();
            LoadTempoData();

            foreach (var timeEntry in TimeEntries)
            {
                PostWorklogsRequests.Add(new PostWorklogsRequest(TempoAccountId, WorkItems.First(x => x.GrindstoneId == timeEntry.WorkItemId), timeEntry, int.Parse(Configuration["TimezoneAdjustment"])));
            }

            foreach (var request in PostWorklogsRequests)
            {
                Console.WriteLine(request.ToString());
            }

            Console.WriteLine("Press Y to continue");
            var answer = Console.ReadKey();
            if (answer.KeyChar.ToString().ToUpper() == "Y")
            {
                Console.WriteLine("\nPosting Data to Tempo...");
                var rep = new TempoRepository();
                foreach (var request in PostWorklogsRequests)
                {
                    Console.WriteLine(request.ToString());
                    if (Configuration["DebugMode"] == "false")
                    {
                        rep.PostWorklog(request);
                    }
                    else
                    {
                        Console.WriteLine("Debug Mode Enabled.  Not Sending.");
                    }
                }
            }


            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }


        private static void ProcessArguments(string start, string end)
        {
            while (!DateTime.TryParse(start, out StartDateTime))
            {
                Console.WriteLine("Enter the starting date/time in the following format: YYYY-MM-dd hh:mm:ss");
                start = Console.ReadLine();
            }

            while (!DateTime.TryParse(end, out EndDateTime))
            {
                Console.WriteLine("Enter the ending date/time in the following format: YYYY-MM-dd hh:mm:ss");
                end = Console.ReadLine();
            }
        }

        private static void LoadGrindstoneData()
        {

            Console.WriteLine("Loading Grindstone Data...");
            using (StreamReader file = File.OpenText(Configuration["GrindstoneDatabaseLocation"]))
            {
                try
                {
                    var serializer = new JsonSerializer();
                    var gsdb = (GrindstoneDatabase)serializer.Deserialize(file, typeof(GrindstoneDatabase));

                    WorkItems = gsdb.f.t
                        .Select(x => new WorkItem(x))
                        .ToHashSet();
                    Console.WriteLine($"Work Items: {WorkItems.Count}");

                    TimeEntries = gsdb.f.r
                        .Where(x => x.s.AddHours(double.Parse(Configuration["TimezoneAdjustment"])) >= StartDateTime && x.e.AddHours(double.Parse(Configuration["TimezoneAdjustment"])) <= EndDateTime)
                        .Select(x => new TimeEntry(x))
                        .ToHashSet();
                    Console.WriteLine($"Time Entries: {TimeEntries.Count}");
                }
                finally
                {
                    file.Close();
                }
            }
            Console.WriteLine("Loading Grindstone Data: Complete!");
        }

        private static void LoadTempoData()
        {
            Console.WriteLine("Loading Tempo Data...");
            var rep = new TempoRepository();
            var teams = rep.GetTeams();
            Console.WriteLine($"Team Count: {teams.results.Count}");
            var members = new List<Member>();
            foreach (var team in teams.results)
            {
                var teamMembers = rep.GetMembers(team.id);
                members.AddRange(teamMembers.results.Select(x => x.member));
            }
            //Clear out duplicates
            var distinctMembers = members.Distinct(new MemberComparer());
            Console.WriteLine($"Member Count: {distinctMembers.Count()}");
            TempoAccountId = distinctMembers.First(x => x.displayName == Configuration["TempoDisplayName"]).accountId;
            Console.WriteLine($"Tempo Account ID: {TempoAccountId}");

            Console.WriteLine("Loading Tempo Data: Complete!");
        }
    }
}
