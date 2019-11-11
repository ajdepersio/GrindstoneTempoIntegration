using GrindstoneTempoIntegration.Domain.Grindstone;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrindstoneTempoIntegration.Domain.Tempo.Models
{
    public class PostWorklogsRequest
    {
        public string issueKey { get; set; }
        public int timeSpentSeconds { get; set; }
        public int billableSeconds { get; set; }
        public string startDate { get; set; }
        public string startTime { get; set; }
        public string description { get; set; }
        public string authorAccountId { get; set; }

        public PostWorklogsRequest(string authorAccountId, WorkItem workItem, TimeEntry timeEntry, int timezoneAdjustment)
        {
            //Round up time to the next min
            var time = (int)(timeEntry.End - timeEntry.Start).TotalSeconds;
            if (time % 60 != 0)
            {
                time = time + (60 - (time % 60));
            }

            this.issueKey = workItem.TicketId;
            this.timeSpentSeconds = time;
            this.billableSeconds = time;
            this.startDate = timeEntry.Start.AddHours(timezoneAdjustment).ToString("yyyy-MM-dd");
            this.startTime = timeEntry.Start.AddHours(timezoneAdjustment).ToString("HH:mm:ss");

            string description = !(string.IsNullOrWhiteSpace(timeEntry.Description)) ? timeEntry.Description : workItem.Name;
            this.description = description;

            this.authorAccountId = authorAccountId;
        }

        public override string ToString()
        {
            return $"Ticket: {this.issueKey} Start: {this.startDate} {this.startTime} Time: {this.timeSpentSeconds} Description: {this.description}";
        }
    }
}