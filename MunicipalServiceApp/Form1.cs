using System;
using System.Drawing;
using System.Windows.Forms;
using MunicipalServiceApp.Services;

namespace MunicipalServiceApp
{
    public partial class Form1 : Form
    {
        private IssueManager issueManager;
        private Label statsLabel;

        // White and blue color palette
        private readonly Color DarkBlue = Color.FromArgb(13, 71, 161);      // Dark blue
        private readonly Color MediumBlue = Color.FromArgb(25, 118, 210);   // Medium blue
        private readonly Color LightBlue = Color.FromArgb(33, 150, 243);    // Light blue
        private readonly Color AccentBlue = Color.FromArgb(63, 81, 181);    // Accent blue
        private readonly Color PureWhite = Color.White;                     // Pure white
        private readonly Color LightGray = Color.FromArgb(245, 245, 245);
        private readonly Color DarkText = Color.FromArgb(33, 33, 33);

        public Form1()
        {
            InitializeComponent();
            issueManager = IssueManager.Instance;
            SetupMainMenu();
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            UpdateStatsLabel();
        }

        private void UpdateStatsLabel()
        {
            if (statsLabel != null)
            {
                statsLabel.Text = $"Total Issues Reported: {issueManager.GetTotalIssueCount()}";
            }
        }

        private void SetupMainMenu()
        {
            // Set form properties
            this.Text = "Municipal Services Portal - South Africa";
            this.Size = new Size(1000, 800);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(248, 250, 252); // Light background
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // Create header panel
            Panel headerPanel = new Panel
            {
                Size = new Size(980, 120),
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

            // Header title
            Label titleLabel = new Label
            {
                Text = "🏛️ Municipal Services Portal",
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                ForeColor = Color.White,
                Size = new Size(600, 40),
                Location = new Point(20, 20),
                BackColor = Color.Transparent
            };

            // Header subtitle
            Label subtitleLabel = new Label
            {
                Text = "Connecting South African Communities with Municipal Services",
                Font = new Font("Segoe UI", 12, FontStyle.Regular),
                ForeColor = Color.FromArgb(200, 255, 200),
                Size = new Size(500, 25),
                Location = new Point(20, 65),
                BackColor = Color.Transparent
            };

            // Statistics label
            statsLabel = new Label
            {
                Text = $"Total Issues Reported: {issueManager.GetTotalIssueCount()}",
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                ForeColor = Color.White,
                Size = new Size(200, 20),
                Location = new Point(750, 75),
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.MiddleRight
            };

            headerPanel.Controls.AddRange(new Control[] { accentBar, titleLabel, subtitleLabel, statsLabel });

            // Create main content panel
            Panel contentPanel = new Panel
            {
                Size = new Size(980, 580),
                Location = new Point(10, 140),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            // Welcome message
            Label welcomeLabel = new Label
            {
                Text = "Welcome to the Municipal Services Portal\n\nSelect a service below to get started:",
                Font = new Font("Segoe UI", 14, FontStyle.Regular),
                ForeColor = DarkText,
                Size = new Size(900, 80),
                Location = new Point(40, 30),
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.TopLeft
            };

            // Service buttons container
            Panel buttonsPanel = new Panel
            {
                Size = new Size(900, 420),
                Location = new Point(40, 120),
                BackColor = Color.Transparent
            };

            // Report Issues button (active)
            Button reportIssuesBtn = CreateServiceButton(
                "🚨 Report Issues",
                "Report municipal issues and service requests",
                new Point(20, 20),
                MediumBlue,
                true
            );
            reportIssuesBtn.Click += ReportIssuesBtn_Click;

            // Local Events button (disabled)
            Button localEventsBtn = CreateServiceButton(
                "📅 Local Events & Announcements",
                "View upcoming events and municipal announcements",
                new Point(320, 20),
                Color.FromArgb(158, 158, 158), // Gray
                false
            );

            // Service Request Status button (disabled)
            Button serviceStatusBtn = CreateServiceButton(
                "📋 Service Request Status",
                "Track the status of your submitted requests",
                new Point(620, 20),
                Color.FromArgb(158, 158, 158), // Gray
                false
            );

            // View Reports button (new feature)
            Button viewReportsBtn = CreateServiceButton(
                "📊 View My Reports",
                "View all your submitted issue reports",
                new Point(20, 180),
                DarkBlue,
                true
            );
            viewReportsBtn.Click += ViewReportsBtn_Click;

            // Help button (active)
            Button helpBtn = CreateServiceButton(
                "❓ Help & Support",
                "Get help and support information",
                new Point(320, 180),
                LightBlue,
                true
            );
            helpBtn.Click += HelpBtn_Click;

            // Emergency Services button (new)
            Button emergencyBtn = CreateServiceButton(
                "🚑 Emergency Services",
                "Quick access to emergency contacts",
                new Point(620, 180),
                Color.FromArgb(183, 28, 28), // Red for emergency
                true
            );
            emergencyBtn.Click += EmergencyBtn_Click;

            // Add buttons to the buttons panel instead of directly to form
            buttonsPanel.Controls.AddRange(new Control[] { 
                reportIssuesBtn, localEventsBtn, serviceStatusBtn, 
                viewReportsBtn, helpBtn, emergencyBtn 
            });

            contentPanel.Controls.AddRange(new Control[] { welcomeLabel, buttonsPanel });

            // Footer panel
            Panel footerPanel = new Panel
            {
                Size = new Size(980, 40),
                Location = new Point(10, 730),
                BackColor = LightGray,
                BorderStyle = BorderStyle.FixedSingle
            };

            Label footerLabel = new Label
            {
                Text = "© 2025 Municipal Services Portal - Serving South African Communities",
                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                ForeColor = Color.FromArgb(117, 117, 117),
                Size = new Size(960, 30),
                Location = new Point(10, 5),
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.MiddleCenter
            };

            footerPanel.Controls.Add(footerLabel);

            // Add all panels to form
            this.Controls.AddRange(new Control[] { headerPanel, contentPanel, footerPanel });
        }

        private Button CreateServiceButton(string title, string description, Point location, Color backColor, bool enabled)
        {
            Button serviceButton = new Button
            {
                Size = new Size(280, 140),
                Location = location,
                BackColor = backColor,
                FlatStyle = FlatStyle.Flat,
                Cursor = enabled ? Cursors.Hand : Cursors.Default,
                Enabled = enabled,
                Text = $"{title}\n\n{description}\n\n{(enabled ? "Available" : "Coming Soon")}",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.TopLeft,
                Padding = new Padding(15)
            };

            serviceButton.FlatAppearance.BorderSize = 0;
            
            // Enhanced hover effects
            if (enabled)
            {
                Color hoverColor = Color.FromArgb(
                    Math.Min(255, backColor.R + 30),
                    Math.Min(255, backColor.G + 30),
                    Math.Min(255, backColor.B + 30)
                );
                serviceButton.FlatAppearance.MouseOverBackColor = hoverColor;
                
                // Add smooth hover animation
                serviceButton.MouseEnter += (s, e) =>
                {
                    serviceButton.Size = new Size(285, 145);
                    serviceButton.Location = new Point(location.X - 2, location.Y - 2);
                };
                
                serviceButton.MouseLeave += (s, e) =>
                {
                    serviceButton.Size = new Size(280, 140);
                    serviceButton.Location = location;
                };
            }

            return serviceButton;
        }

        private void ReportIssuesBtn_Click(object sender, EventArgs e)
        {
            ReportIssuesForm reportForm = new ReportIssuesForm();
            reportForm.ShowDialog();
            
            // Refresh statistics after returning from report form
            RefreshStatistics();
        }

        private void HelpBtn_Click(object sender, EventArgs e)
        {
            string helpMessage = "Municipal Services Portal Help\n\n" +
                "🚨 Report Issues: Submit reports about municipal problems\n" +
                "📅 Local Events: View community events (Coming Soon)\n" +
                "📋 Service Status: Track your requests (Coming Soon)\n\n" +
                "For technical support, contact your local municipality.\n\n" +
                "This application helps citizens engage with municipal services efficiently.";

            MessageBox.Show(helpMessage, "Help & Support", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ViewReportsBtn_Click(object sender, EventArgs e)
        {
            ViewReportsForm viewReportsForm = new ViewReportsForm();
            viewReportsForm.ShowDialog();
        }

        private void EmergencyBtn_Click(object sender, EventArgs e)
        {
            string emergencyMessage = "🚑 Emergency Services Contacts\n\n" +
                "Police: 10111\n" +
                "Fire & Rescue: 10177\n" +
                "Ambulance: 10177\n" +
                "Municipal Emergency: 0860 103 089\n\n" +
                "For life-threatening emergencies, call immediately!\n" +
                "For non-urgent municipal issues, please use the Report Issues feature.";

            MessageBox.Show(emergencyMessage, "Emergency Services", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void RefreshStatistics()
        {
            // Find and update the statistics label
            foreach (Control control in this.Controls)
            {
                if (control is Panel panel && panel.BackColor == Color.FromArgb(25, 118, 210))
                {
                    foreach (Control child in panel.Controls)
                    {
                        if (child is Label label && label.Text.StartsWith("Total Issues"))
                        {
                            label.Text = $"Total Issues Reported: {issueManager.GetTotalIssueCount()}";
                            break;
                        }
                    }
                    break;
                }
            }
        }
    }
}
