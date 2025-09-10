using System;
using System.Drawing;
using System.Windows.Forms;
using MunicipalServiceApp.Services;

namespace MunicipalServiceApp
{
    public partial class Form1 : Form
    {
        private IssueManager issueManager;

        public Form1()
        {
            InitializeComponent();
            issueManager = IssueManager.Instance;
            SetupMainMenu();
        }

        private void SetupMainMenu()
        {
            // Set form properties
            this.Text = "Municipal Services Portal - South Africa";
            this.Size = new Size(900, 750);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(240, 248, 255); // Light blue background
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // Create header panel
            Panel headerPanel = new Panel
            {
                Size = new Size(880, 120),
                Location = new Point(10, 10),
                BackColor = Color.FromArgb(25, 118, 210), // Professional blue
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
                Text = "Connecting Citizens with Municipal Services",
                Font = new Font("Segoe UI", 12, FontStyle.Regular),
                ForeColor = Color.FromArgb(200, 230, 255),
                Size = new Size(400, 25),
                Location = new Point(20, 65),
                BackColor = Color.Transparent
            };

            // Statistics label
            Label statsLabel = new Label
            {
                Text = $"Total Issues Reported: {issueManager.GetTotalIssueCount()}",
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                ForeColor = Color.White,
                Size = new Size(200, 20),
                Location = new Point(650, 75),
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.MiddleRight
            };

            headerPanel.Controls.AddRange(new Control[] { titleLabel, subtitleLabel, statsLabel });

            // Create main content panel
            Panel contentPanel = new Panel
            {
                Size = new Size(880, 500),
                Location = new Point(10, 140),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            // Welcome message
            Label welcomeLabel = new Label
            {
                Text = "Welcome to the Municipal Services Portal\n\nSelect a service below to get started:",
                Font = new Font("Segoe UI", 14, FontStyle.Regular),
                ForeColor = Color.FromArgb(33, 33, 33),
                Size = new Size(800, 80),
                Location = new Point(40, 30),
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.TopLeft
            };

            // Service buttons container
            Panel buttonsPanel = new Panel
            {
                Size = new Size(800, 350),
                Location = new Point(40, 120),
                BackColor = Color.Transparent
            };

            // Report Issues button (active)
            Button reportIssuesBtn = CreateServiceButton(
                "🚨 Report Issues",
                "Report municipal issues and service requests",
                new Point(20, 20),
                Color.FromArgb(76, 175, 80), // Green
                true
            );
            reportIssuesBtn.Click += ReportIssuesBtn_Click;

            // Local Events button (disabled)
            Button localEventsBtn = CreateServiceButton(
                "📅 Local Events & Announcements",
                "View upcoming events and municipal announcements",
                new Point(360, 20),
                Color.FromArgb(158, 158, 158), // Gray
                false
            );

            // Service Request Status button (disabled)
            Button serviceStatusBtn = CreateServiceButton(
                "📋 Service Request Status",
                "Track the status of your submitted requests",
                new Point(20, 180),
                Color.FromArgb(158, 158, 158), // Gray
                false
            );

            // Help button (active)
            Button helpBtn = CreateServiceButton(
                "❓ Help & Support",
                "Get help and support information",
                new Point(360, 180),
                Color.FromArgb(33, 150, 243), // Blue
                true
            );
            helpBtn.Click += HelpBtn_Click;

            // Add buttons to the buttons panel instead of directly to form
            buttonsPanel.Controls.AddRange(new Control[] { 
                reportIssuesBtn, localEventsBtn, serviceStatusBtn, helpBtn 
            });

            contentPanel.Controls.AddRange(new Control[] { welcomeLabel, buttonsPanel });

            // Footer panel
            Panel footerPanel = new Panel
            {
                Size = new Size(880, 40),
                Location = new Point(10, 700),
                BackColor = Color.FromArgb(245, 245, 245),
                BorderStyle = BorderStyle.FixedSingle
            };

            Label footerLabel = new Label
            {
                Text = "© 2025 Municipal Services Portal - Serving South African Communities",
                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                ForeColor = Color.FromArgb(117, 117, 117),
                Size = new Size(860, 30),
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
                Size = new Size(320, 140),
                Location = location,
                BackColor = backColor,
                FlatStyle = FlatStyle.Flat,
                Cursor = enabled ? Cursors.Hand : Cursors.Default,
                Enabled = enabled,
                Text = $"{title}\n\n{description}\n\n{(enabled ? "Available" : "Coming Soon")}",
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.TopLeft,
                Padding = new Padding(15)
            };

            serviceButton.FlatAppearance.BorderSize = 0;
            
            if (enabled)
            {
                serviceButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(
                    Math.Min(255, backColor.R + 20),
                    Math.Min(255, backColor.G + 20),
                    Math.Min(255, backColor.B + 20)
                );
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
