using System;

namespace MunicipalServiceApp.Models
{
    /// <summary>
    /// Represents a municipal service issue reported by a citizen
    /// </summary>
    public class Issue
    {
        public int Id { get; set; }
        public string Location { get; set; }
        public IssueCategory Category { get; set; }
        public string Description { get; set; }
        public string AttachedFilePath { get; set; }
        public DateTime DateReported { get; set; }
        public IssueStatus Status { get; set; }
        public string CitizenName { get; set; }

        public Issue()
        {
            DateReported = DateTime.Now;
            Status = IssueStatus.Submitted;
        }

        public Issue(string location, IssueCategory category, string description, string attachedFilePath = null)
        {
            Location = location;
            Category = category;
            Description = description;
            AttachedFilePath = attachedFilePath;
            DateReported = DateTime.Now;
            Status = IssueStatus.Submitted;
        }

        public override string ToString()
        {
            return $"Issue #{Id}: {Category} at {Location} - {Status}";
        }
    }

    /// <summary>
    /// Enumeration of available issue categories
    /// </summary>
    public enum IssueCategory
    {
        Sanitation,
        Roads,
        Utilities,
        StreetLighting,
        WaterSupply,
        WasteManagement,
        PublicSafety,
        Parks,
        Other
    }

    /// <summary>
    /// Enumeration of issue status types
    /// </summary>
    public enum IssueStatus
    {
        Submitted,
        UnderReview,
        InProgress,
        Resolved,
        Closed
    }
}
