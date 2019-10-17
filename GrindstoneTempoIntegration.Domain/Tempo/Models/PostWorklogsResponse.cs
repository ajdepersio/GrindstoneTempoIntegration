using GrindstoneTempoIntegration.Domain.Tempo.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrindstoneTempoIntegration.Domain.Tempo.Models
{
    public class PostWorklogsResponse
    {
        public string self { get; set; }
        public int tempoWorklogId { get; set; }
        public int jiraWorklogId { get; set; }
        public Issue issue { get; set; }
        public int timeSpentSeconds { get; set; }
        public int billableSeconds { get; set; }
        public string startDate { get; set; }
        public string startTime { get; set; }
        public string description { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public Author author { get; set; }
        public Attributes attributes { get; set; }
    }
}
