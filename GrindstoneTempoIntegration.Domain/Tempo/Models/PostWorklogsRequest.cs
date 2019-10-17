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
        
        public PostWorklogsRequest(string authorAccountId, WorkItem workItem, TimeEntry timeEntry)
        {
            this.issueKey = workItem.TicketId;
            this.timeSpentSeconds = (int)(timeEntry.End - timeEntry.Start).TotalSeconds;
            this.billableSeconds = (int)(timeEntry.End - timeEntry.Start).TotalSeconds;
            this.startDate = timeEntry.Start.ToString("yyyy-MM-dd");
            this.startTime = timeEntry.Start.ToString("HH:mm:ss");

            string description = !(string.IsNullOrWhiteSpace(timeEntry.Description)) ? timeEntry.Description : workItem.Name;
            this.description = description;

            this.authorAccountId = authorAccountId;
        }
    }
}