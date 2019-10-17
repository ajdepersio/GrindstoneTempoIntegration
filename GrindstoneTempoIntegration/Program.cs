using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.IO;
using GrindstoneTempoIntegration.Domain.Grindstone;
using System.Collections.Generic;
using System.Linq;
using GrindstoneTempoIntegration.Domain.Tempo;

namespace GrindstoneTempoIntegration
{
    class Program
    {
        public static IConfigurationRoot Configuration;

        public static HashSet<WorkItem> WorkItems;

        public static HashSet<TimeEntry> TimeEntries;

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

            Console.WriteLine("Press any key to exit...");
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
                var serializer = new JsonSerializer();
                var gsdb = (GrindstoneDatabase)serializer.Deserialize(file, typeof(GrindstoneDatabase));

                WorkItems = gsdb.f.t
                    .Select(x => new WorkItem(x))
                    .ToHashSet();
                Console.WriteLine($"Work Items: {WorkItems.Count}");

                TimeEntries = gsdb.f.r
                    .Where(x => x.s >= StartDateTime && x.e <= EndDateTime)
                    .Select(x => new TimeEntry(x))
                    .ToHashSet();
                Console.WriteLine($"Time Entries: {TimeEntries.Count}");
            }
            Console.WriteLine("Loading Grindstone Data: Complete!");
        }
    }
}
