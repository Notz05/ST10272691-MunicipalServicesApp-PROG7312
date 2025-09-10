using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using MunicipalServiceApp.Models;
using MunicipalServiceApp.Services;

namespace MunicipalServiceApp
{
    public partial class ReportIssuesForm : Form
    {
        private IssueManager issueManager;
        private string attachedFilePath;
        private ProgressBar progressBar;
        private Label progressLabel;
        private Timer progressTimer;
        private int progressStep;

        // Form controls
        private TextBox locationTextBox;
        private ComboBox categoryComboBox;
        private RichTextBox descriptionRichTextBox;
        private Button attachFileButton;
        private Label attachedFileLabel;
        private Button submitButton;
        private Button backButton;

        public ReportIssuesForm()
        {
            InitializeComponent();
            issueManager = IssueManager.Instance;
            SetupReportIssuesForm();
        }

        private void SetupReportIssuesForm()
        {
            // Set form properties
            this.Text = "Report Municipal Issues";
            this.Size = new Size(800, 750);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(240, 248, 255);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // Create header panel
            Panel headerPanel = new Panel
            {
                Size = new Size(780, 80),
                Location = new Point(10, 10),
                BackColor = Color.FromArgb(25, 118, 210),
                BorderStyle = BorderStyle.None
            };

            Label headerLabel = new Label
            {
                Text = "🚨 Report Municipal Issues",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.White,
                Size = new Size(400, 35),
                Location = new Point(20, 15),
                BackColor = Color.Transparent
            };

            Label headerSubLabel = new Label
            {
                Text = "Help us improve your community by reporting issues",
                Font = new Font("Segoe UI", 11, FontStyle.Regular),
                ForeColor = Color.FromArgb(200, 230, 255),
                Size = new Size(400, 25),
                Location = new Point(20, 45),
                BackColor = Color.Transparent
            };

            headerPanel.Controls.AddRange(new Control[] { headerLabel, headerSubLabel });

            // Create main content panel
            Panel contentPanel = new Panel
            {
                Size = new Size(780, 570),
                Location = new Point(10, 100),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            // Location input section
            Label locationLabel = new Label
            {
                Text = "📍 Location *",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(33, 33, 33),
                Size = new Size(150, 25),
                Location = new Point(30, 30),
                BackColor = Color.Transparent
            };

            locationTextBox = new TextBox
            {
                Font = new Font("Segoe UI", 11),
                Size = new Size(500, 30),
                Location = new Point(30, 60),
                BorderStyle = BorderStyle.FixedSingle
            };

            Label locationHint = new Label
            {
                Text = "Please provide the specific location or address of the issue",
                Font = new Font("Segoe UI", 9, FontStyle.Italic),
                ForeColor = Color.FromArgb(117, 117, 117),
                Size = new Size(400, 20),
                Location = new Point(30, 95),
                BackColor = Color.Transparent
            };

            // Category selection section
            Label categoryLabel = new Label
            {
                Text = "🏷️ Category *",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(33, 33, 33),
                Size = new Size(150, 25),
                Location = new Point(30, 130),
                BackColor = Color.Transparent
            };

            categoryComboBox = new ComboBox
            {
                Font = new Font("Segoe UI", 11),
                Size = new Size(300, 30),
                Location = new Point(30, 160),
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            // Populate category dropdown
            foreach (IssueCategory category in Enum.GetValues(typeof(IssueCategory)))
            {
                categoryComboBox.Items.Add(category.ToString());
            }

            // Description section
            Label descriptionLabel = new Label
            {
                Text = "📝 Description *",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(33, 33, 33),
                Size = new Size(150, 25),
                Location = new Point(30, 210),
                BackColor = Color.Transparent
            };

            descriptionRichTextBox = new RichTextBox
            {
                Font = new Font("Segoe UI", 11),
                Size = new Size(500, 120),
                Location = new Point(30, 240),
                BorderStyle = BorderStyle.FixedSingle
            };

            Label descriptionHint = new Label
            {
                Text = "Please provide detailed information about the issue",
                Font = new Font("Segoe UI", 9, FontStyle.Italic),
                ForeColor = Color.FromArgb(117, 117, 117),
                Size = new Size(400, 20),
                Location = new Point(30, 365),
                BackColor = Color.Transparent
            };

            // File attachment section
            Label attachmentLabel = new Label
            {
                Text = "📎 Attach Files (Optional)",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(33, 33, 33),
                Size = new Size(200, 25),
                Location = new Point(30, 400),
                BackColor = Color.Transparent
            };

            attachFileButton = new Button
            {
                Text = "Choose File",
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                Size = new Size(120, 35),
                Location = new Point(30, 430),
                BackColor = Color.FromArgb(33, 150, 243),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            attachFileButton.FlatAppearance.BorderSize = 0;
            attachFileButton.Click += AttachFileButton_Click;

            attachedFileLabel = new Label
            {
                Text = "No file selected",
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                ForeColor = Color.FromArgb(117, 117, 117),
                Size = new Size(350, 25),
                Location = new Point(160, 437),
                BackColor = Color.Transparent
            };

            // Progress section (initially hidden)
            progressLabel = new Label
            {
                Text = "",
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                ForeColor = Color.FromArgb(76, 175, 80),
                Size = new Size(400, 25),
                Location = new Point(30, 470),
                BackColor = Color.Transparent,
                Visible = false
            };

            progressBar = new ProgressBar
            {
                Size = new Size(400, 20),
                Location = new Point(30, 495),
                Style = ProgressBarStyle.Continuous,
                Visible = false
            };

            contentPanel.Controls.AddRange(new Control[] {
                locationLabel, locationTextBox, locationHint,
                categoryLabel, categoryComboBox,
                descriptionLabel, descriptionRichTextBox, descriptionHint,
                attachmentLabel, attachFileButton, attachedFileLabel,
                progressLabel, progressBar
            });

            // Create button panel
            Panel buttonPanel = new Panel
            {
                Size = new Size(780, 60),
                Location = new Point(10, 680),
                BackColor = Color.FromArgb(245, 245, 245),
                BorderStyle = BorderStyle.FixedSingle
            };

            backButton = new Button
            {
                Text = "← Back to Main Menu",
                Font = new Font("Segoe UI", 11, FontStyle.Regular),
                Size = new Size(160, 40),
                Location = new Point(20, 10),
                BackColor = Color.FromArgb(158, 158, 158),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            backButton.FlatAppearance.BorderSize = 0;
            backButton.Click += BackButton_Click;

            submitButton = new Button
            {
                Text = "Submit Issue Report",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Size = new Size(180, 40),
                Location = new Point(580, 10),
                BackColor = Color.FromArgb(76, 175, 80),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            submitButton.FlatAppearance.BorderSize = 0;
            submitButton.Click += SubmitButton_Click;

            buttonPanel.Controls.AddRange(new Control[] { backButton, submitButton });

            // Add all panels to form
            this.Controls.AddRange(new Control[] { headerPanel, contentPanel, buttonPanel });

            // Initialize progress timer
            progressTimer = new Timer();
            progressTimer.Interval = 100;
            progressTimer.Tick += ProgressTimer_Tick;
        }

        private void AttachFileButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Select a file to attach",
                Filter = "All Files (*.*)|*.*|Images (*.jpg;*.jpeg;*.png;*.gif;*.bmp)|*.jpg;*.jpeg;*.png;*.gif;*.bmp|Documents (*.pdf;*.doc;*.docx;*.txt)|*.pdf;*.doc;*.docx;*.txt",
                FilterIndex = 2,
                RestoreDirectory = true
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                attachedFilePath = openFileDialog.FileName;
                string fileName = Path.GetFileName(attachedFilePath);
                
                // Check file size (limit to 10MB)
                FileInfo fileInfo = new FileInfo(attachedFilePath);
                if (fileInfo.Length > 10 * 1024 * 1024)
                {
                    MessageBox.Show("File size must be less than 10MB. Please select a smaller file.", 
                        "File Too Large", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    attachedFilePath = null;
                    attachedFileLabel.Text = "No file selected";
                    attachedFileLabel.ForeColor = Color.FromArgb(117, 117, 117);
                    return;
                }

                attachedFileLabel.Text = $"📎 {fileName}";
                attachedFileLabel.ForeColor = Color.FromArgb(76, 175, 80);
            }
        }

        private void SubmitButton_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                StartProgressAnimation();
                
                // Create new issue
                Issue newIssue = new Issue
                {
                    Location = locationTextBox.Text.Trim(),
                    Category = (IssueCategory)Enum.Parse(typeof(IssueCategory), categoryComboBox.SelectedItem.ToString()),
                    Description = descriptionRichTextBox.Text.Trim(),
                    AttachedFilePath = attachedFilePath
                };

                // Add to issue manager
                issueManager.AddIssue(newIssue);

                // Show success message after progress completes
                progressStep = 0;
                progressTimer.Start();
            }
        }

        private bool ValidateForm()
        {
            string errorMessage = "";

            if (string.IsNullOrWhiteSpace(locationTextBox.Text))
            {
                errorMessage += "• Please specify the location of the issue\n";
            }

            if (categoryComboBox.SelectedIndex == -1)
            {
                errorMessage += "• Please select a category for the issue\n";
            }

            if (string.IsNullOrWhiteSpace(descriptionRichTextBox.Text))
            {
                errorMessage += "• Please provide a description of the issue\n";
            }

            if (!string.IsNullOrEmpty(errorMessage))
            {
                MessageBox.Show("Please correct the following issues:\n\n" + errorMessage, 
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void StartProgressAnimation()
        {
            progressLabel.Text = "Submitting your report...";
            progressLabel.Visible = true;
            progressBar.Visible = true;
            progressBar.Value = 0;
            
            // Disable form controls during submission
            submitButton.Enabled = false;
            locationTextBox.Enabled = false;
            categoryComboBox.Enabled = false;
            descriptionRichTextBox.Enabled = false;
            attachFileButton.Enabled = false;
        }

        private void ProgressTimer_Tick(object sender, EventArgs e)
        {
            progressStep++;
            
            if (progressStep <= 20)
            {
                progressBar.Value = progressStep * 5;
                
                if (progressStep == 5)
                    progressLabel.Text = "Validating information...";
                else if (progressStep == 10)
                    progressLabel.Text = "Processing attachments...";
                else if (progressStep == 15)
                    progressLabel.Text = "Saving to database...";
                else if (progressStep == 20)
                    progressLabel.Text = "Report submitted successfully! 🎉";
            }
            else
            {
                progressTimer.Stop();
                
                // Show success message
                MessageBox.Show("Your issue has been successfully reported!\n\n" +
                    "Thank you for helping improve our community. " +
                    "You will receive updates on the status of your report.",
                    "Report Submitted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                // Close the form
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (progressTimer != null)
            {
                progressTimer.Stop();
                progressTimer.Dispose();
            }
            base.OnFormClosing(e);
        }
    }
}
