using System;
using System.Drawing;
using System.Windows.Forms;
using MunicipalServiceApp.Models;
using MunicipalServiceApp.Services;
using MunicipalServiceApp.DataStructures;

namespace MunicipalServiceApp
{
    public partial class ViewReportsForm : Form
    {
        private IssueManager issueManager;
        private ListView reportsListView;

        // White and blue color palette
        private readonly Color DarkBlue = Color.FromArgb(13, 71, 161);      // Dark blue
        private readonly Color MediumBlue = Color.FromArgb(25, 118, 210);   // Medium blue
        private readonly Color LightBlue = Color.FromArgb(33, 150, 243);    // Light blue
        private readonly Color AccentBlue = Color.FromArgb(63, 81, 181);    // Accent blue
        private readonly Color PureWhite = Color.White;                     // Pure white
        private readonly Color LightGray = Color.FromArgb(245, 245, 245);
        private readonly Color DarkText = Color.FromArgb(33, 33, 33);

        public ViewReportsForm()
        {
            InitializeComponent();
            issueManager = IssueManager.Instance;
            SetupViewReportsForm();
            LoadReports();
        }

        private void SetupViewReportsForm()
        {
            // Set form properties
            this.Text = "View My Reports";
            this.Size = new Size(900, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(248, 250, 252);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // Create header panel
            Panel headerPanel = new Panel
            {
                Size = new Size(980, 80),
                Location = new Point(10, 10),
                BackColor = DarkBlue,
                BorderStyle = BorderStyle.None
            };

            // Add blue accent bar
            Panel accentBar = new Panel
            {
                Size = new Size(980, 4),
                Location = new Point(0, 0),
                BackColor = LightBlue,
                BorderStyle = BorderStyle.None
            };

            Label headerLabel = new Label
            {
                Text = "📊 My Issue Reports",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.White,
                Size = new Size(400, 35),
                Location = new Point(20, 15),
                BackColor = Color.Transparent
            };

            Label headerSubLabel = new Label
            {
                Text = "Track and manage your submitted municipal issues",
                Font = new Font("Segoe UI", 11, FontStyle.Regular),
                ForeColor = Color.FromArgb(200, 255, 200),
                Size = new Size(400, 25),
                Location = new Point(20, 45),
                BackColor = Color.Transparent
            };

            headerPanel.Controls.AddRange(new Control[] { accentBar, headerLabel, headerSubLabel });

            // Create main content panel
            Panel contentPanel = new Panel
            {
                Size = new Size(880, 520),
                Location = new Point(10, 100),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            // Statistics panel
            Panel statsPanel = new Panel
            {
                Size = new Size(840, 60),
                Location = new Point(20, 20),
                BackColor = LightGray,
                BorderStyle = BorderStyle.None
            };

            Label totalReportsLabel = new Label
            {
                Text = $"Total Reports: {issueManager.GetTotalIssueCount()}",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = DarkText,
                Size = new Size(200, 25),
                Location = new Point(20, 18),
                BackColor = Color.Transparent
            };

            Label pendingReportsLabel = new Label
            {
                Text = $"Pending: {issueManager.GetPendingIssueCount()}",
                Font = new Font("Segoe UI", 12, FontStyle.Regular),
                ForeColor = AccentBlue,
                Size = new Size(150, 25),
                Location = new Point(250, 18),
                BackColor = Color.Transparent
            };

            statsPanel.Controls.AddRange(new Control[] { totalReportsLabel, pendingReportsLabel });

            // Reports list view
            reportsListView = new ListView
            {
                Size = new Size(840, 380),
                Location = new Point(20, 100),
                View = View.Details,
                FullRowSelect = true,
                GridLines = true,
                Font = new Font("Segoe UI", 10, FontStyle.Regular)
            };

            // Add columns
            reportsListView.Columns.Add("ID", 50);
            reportsListView.Columns.Add("Date", 100);
            reportsListView.Columns.Add("Location", 200);
            reportsListView.Columns.Add("Category", 120);
            reportsListView.Columns.Add("Status", 100);
            reportsListView.Columns.Add("Description", 250);

            contentPanel.Controls.AddRange(new Control[] { statsPanel, reportsListView });

            // Create button panel
            Panel buttonPanel = new Panel
            {
                Size = new Size(880, 60),
                Location = new Point(10, 630),
                BackColor = LightGray,
                BorderStyle = BorderStyle.FixedSingle
            };

            // Action buttons
            Button refreshButton = new Button
            {
                Text = "🔄 Refresh",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Size = new Size(120, 35),
                Location = new Point(20, 20),
                BackColor = MediumBlue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            refreshButton.FlatAppearance.BorderSize = 0;
            refreshButton.Click += RefreshButton_Click;

            Button exportButton = new Button
            {
                Text = "📤 Export",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Size = new Size(120, 35),
                Location = new Point(160, 20),
                BackColor = LightBlue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            exportButton.FlatAppearance.BorderSize = 0;
            exportButton.Click += ExportButton_Click;

            Button backButton = new Button
            {
                Text = "← Back to Menu",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Size = new Size(140, 35),
                Location = new Point(300, 20),
                BackColor = Color.FromArgb(117, 117, 117),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            backButton.FlatAppearance.BorderSize = 0;
            backButton.Click += BackButton_Click;

            buttonPanel.Controls.AddRange(new Control[] { backButton, refreshButton, exportButton });

            // Add all panels to form
            this.Controls.AddRange(new Control[] { headerPanel, contentPanel, buttonPanel });
        }

        private void LoadReports()
        {
            reportsListView.Items.Clear();
            
            CustomList<Issue> allIssues = issueManager.GetAllIssues();
            
            for (int i = 0; i < allIssues.Count; i++)
            {
                Issue issue = allIssues[i];
                
                ListViewItem item = new ListViewItem(issue.Id.ToString());
                item.SubItems.Add(issue.DateReported.ToString("MM/dd/yyyy"));
                item.SubItems.Add(issue.Location);
                item.SubItems.Add(issue.Category.ToString());
                item.SubItems.Add(issue.Status.ToString());
                item.SubItems.Add(issue.Description.Length > 50 ? 
                    issue.Description.Substring(0, 47) + "..." : 
                    issue.Description);
                
                // Color code by status
                switch (issue.Status)
                {
                    case IssueStatus.Submitted:
                        item.BackColor = Color.FromArgb(255, 248, 225); // Light yellow
                        break;
                    case IssueStatus.InProgress:
                        item.BackColor = Color.FromArgb(227, 242, 253); // Light blue
                        break;
                    case IssueStatus.Resolved:
                        item.BackColor = Color.FromArgb(232, 245, 233); // Light green
                        break;
                }
                
                reportsListView.Items.Add(item);
            }
            
            // Update statistics
            UpdateStatistics();
        }

        private void UpdateStatistics()
        {
            foreach (Control control in this.Controls)
            {
                if (control is Panel contentPanel && contentPanel.BackColor == Color.White)
                {
                    foreach (Control child in contentPanel.Controls)
                    {
                        if (child is Panel statsPanel && statsPanel.BackColor == LightGray)
                        {
                            foreach (Control stat in statsPanel.Controls)
                            {
                                if (stat is Label label)
                                {
                                    if (label.Text.StartsWith("Total Reports:"))
                                    {
                                        label.Text = $"Total Reports: {issueManager.GetTotalIssueCount()}";
                                    }
                                    else if (label.Text.StartsWith("Pending:"))
                                    {
                                        label.Text = $"Pending: {issueManager.GetPendingIssueCount()}";
                                    }
                                }
                            }
                            break;
                        }
                    }
                    break;
                }
            }
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            LoadReports();
            MessageBox.Show("Reports refreshed successfully!", "Refresh Complete", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ExportButton_Click(object sender, EventArgs e)
        {
            string reportSummary = issueManager.GetCategoryStatistics();
            
            string fullReport = "Municipal Services - Issue Report Summary\n" +
                "Generated: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\n\n" +
                reportSummary + "\n" +
                "This report shows the distribution of issues by category.\n" +
                "For detailed issue tracking, please contact your municipal office.";
            
            MessageBox.Show(fullReport, "Issue Report Summary", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
