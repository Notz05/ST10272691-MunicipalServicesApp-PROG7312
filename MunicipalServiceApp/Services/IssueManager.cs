using System;
using System.Linq;
using MunicipalServiceApp.Models;
using MunicipalServiceApp.DataStructures;

namespace MunicipalServiceApp.Services
{
    /// <summary>
    /// Singleton service class for managing municipal issues
    /// Uses custom data structures for efficient issue management
    /// </summary>
    public class IssueManager
    {
        private static IssueManager instance;
        private static readonly object lockObject = new object();
        
        private CustomList<Issue> allIssues;
        private IssueQueue pendingIssues;
        private int nextIssueId;

        private IssueManager()
        {
            allIssues = new CustomList<Issue>();
            pendingIssues = new IssueQueue();
            nextIssueId = 1;
        }

        /// <summary>
        /// Gets the singleton instance of IssueManager
        /// </summary>
        public static IssueManager Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (lockObject)
                    {
                        if (instance == null)
                        {
                            instance = new IssueManager();
                        }
                    }
                }
                return instance;
            }
        }

        /// <summary>
        /// Adds a new issue to the system
        /// </summary>
        public void AddIssue(Issue issue)
        {
            if (issue == null)
                throw new ArgumentNullException(nameof(issue));

            issue.Id = nextIssueId++;
            allIssues.Add(issue);
            
            // Add to pending queue if status is submitted
            if (issue.Status == IssueStatus.Submitted)
            {
                pendingIssues.Enqueue(issue);
            }
        }

        /// <summary>
        /// Gets all issues in the system
        /// </summary>
        public CustomList<Issue> GetAllIssues()
        {
            return allIssues;
        }

        /// <summary>
        /// Gets issues by category
        /// </summary>
        public CustomList<Issue> GetIssuesByCategory(IssueCategory category)
        {
            CustomList<Issue> filteredIssues = new CustomList<Issue>();
            
            for (int i = 0; i < allIssues.Count; i++)
            {
                if (allIssues[i].Category == category)
                {
                    filteredIssues.Add(allIssues[i]);
                }
            }
            
            return filteredIssues;
        }

        /// <summary>
        /// Gets issues by status
        /// </summary>
        public CustomList<Issue> GetIssuesByStatus(IssueStatus status)
        {
            CustomList<Issue> filteredIssues = new CustomList<Issue>();
            
            for (int i = 0; i < allIssues.Count; i++)
            {
                if (allIssues[i].Status == status)
                {
                    filteredIssues.Add(allIssues[i]);
                }
            }
            
            return filteredIssues;
        }

        /// <summary>
        /// Gets the next pending issue from the queue
        /// </summary>
        public Issue GetNextPendingIssue()
        {
            if (pendingIssues.IsEmpty)
                return null;
                
            return pendingIssues.Dequeue();
        }

        /// <summary>
        /// Updates the status of an issue
        /// </summary>
        public bool UpdateIssueStatus(int issueId, IssueStatus newStatus)
        {
            for (int i = 0; i < allIssues.Count; i++)
            {
                if (allIssues[i].Id == issueId)
                {
                    allIssues[i].Status = newStatus;
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Finds an issue by ID
        /// </summary>
        public Issue FindIssueById(int issueId)
        {
            for (int i = 0; i < allIssues.Count; i++)
            {
                if (allIssues[i].Id == issueId)
                {
                    return allIssues[i];
                }
            }
            return null;
        }

        /// <summary>
        /// Gets the total count of issues
        /// </summary>
        public int GetTotalIssueCount()
        {
            return allIssues.Count;
        }

        /// <summary>
        /// Gets the count of pending issues
        /// </summary>
        public int GetPendingIssueCount()
        {
            return pendingIssues.Count;
        }

        /// <summary>
        /// Searches for issues containing the specified text in location or description
        /// </summary>
        public CustomList<Issue> SearchIssues(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
                return new CustomList<Issue>();

            CustomList<Issue> results = new CustomList<Issue>();
            string lowerSearchText = searchText.ToLower();

            for (int i = 0; i < allIssues.Count; i++)
            {
                Issue issue = allIssues[i];
                if ((issue.Location != null && issue.Location.ToLower().Contains(lowerSearchText)) ||
                    (issue.Description != null && issue.Description.ToLower().Contains(lowerSearchText)))
                {
                    results.Add(issue);
                }
            }

            return results;
        }

        /// <summary>
        /// Gets statistics about issues by category
        /// </summary>
        public string GetCategoryStatistics()
        {
            var categoryStats = new int[Enum.GetValues(typeof(IssueCategory)).Length];
            
            for (int i = 0; i < allIssues.Count; i++)
            {
                categoryStats[(int)allIssues[i].Category]++;
            }

            string stats = "Issue Statistics by Category:\n";
            var categories = Enum.GetValues(typeof(IssueCategory));
            
            for (int i = 0; i < categories.Length; i++)
            {
                stats += $"{categories.GetValue(i)}: {categoryStats[i]} issues\n";
            }

            return stats;
        }
    }
}
