using System;
using System.Drawing;
using System.Windows.Forms;

namespace MunicipalServiceApp
{
    public partial class WelcomeForm : Form
    {
        private Timer fadeTimer;
        private Timer slideTimer;
        private double opacity = 0.0;
        private int slidePosition = 50;
        private Panel welcomePanel;
        private Label titleLabel;
        private Label subtitleLabel;
        private Button getStartedButton;

        // White and blue color palette
        private readonly Color DarkBlue = Color.FromArgb(13, 71, 161);      // Dark blue
        private readonly Color MediumBlue = Color.FromArgb(25, 118, 210);   // Medium blue
        private readonly Color LightBlue = Color.FromArgb(33, 150, 243);    // Light blue
        private readonly Color AccentBlue = Color.FromArgb(63, 81, 181);    // Accent blue
        private readonly Color PureWhite = Color.White;                     // Pure white
        private readonly Color LightGray = Color.FromArgb(245, 245, 245);

        public WelcomeForm()
        {
            InitializeComponent();
            SetupWelcomeScreen();
            StartAnimations();
        }

        private void SetupWelcomeScreen()
        {
            // Form properties
            this.Text = "Municipal Services Portal";
            this.Size = new Size(900, 750);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = PureWhite;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Opacity = 0; // Start invisible for fade-in effect

            // Main welcome panel
            welcomePanel = new Panel
            {
                Size = new Size(700, 500),
                Location = new Point(100, 125 + slidePosition), // Start lower for slide effect
                BackColor = PureWhite,
                BorderStyle = BorderStyle.FixedSingle
            };

            // Blue accent bar
            Panel accentBar = new Panel
            {
                Size = new Size(700, 8),
                Location = new Point(0, 0),
                BackColor = MediumBlue,
                BorderStyle = BorderStyle.None
            };

            // Title
            titleLabel = new Label
            {
                Text = "🏛️ Welcome to Municipal Services Portal",
                Font = new Font("Segoe UI", 28, FontStyle.Bold),
                ForeColor = DarkBlue,
                Size = new Size(650, 60),
                Location = new Point(25, 80),
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Subtitle
            subtitleLabel = new Label
            {
                Text = "Connecting South African Communities\nwith Municipal Services",
                Font = new Font("Segoe UI", 16, FontStyle.Regular),
                ForeColor = MediumBlue,
                Size = new Size(650, 80),
                Location = new Point(25, 160),
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Description
            Label descriptionLabel = new Label
            {
                Text = "Report issues, access information, and engage with your local municipality\nthrough our comprehensive digital platform.",
                Font = new Font("Segoe UI", 12, FontStyle.Regular),
                ForeColor = Color.FromArgb(66, 66, 66),
                Size = new Size(650, 60),
                Location = new Point(25, 260),
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Get Started button with modern design
            getStartedButton = new Button
            {
                Text = "Get Started →",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Size = new Size(250, 60),
                Location = new Point(225, 380),
                BackColor = LightBlue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };

            getStartedButton.FlatAppearance.BorderSize = 0;
            getStartedButton.FlatAppearance.MouseOverBackColor = MediumBlue;
            getStartedButton.FlatAppearance.MouseDownBackColor = DarkBlue;

            // Add rounded corners effect
            getStartedButton.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            };

            // Button click event
            getStartedButton.Click += GetStartedButton_Click;

            // Add hover animation
            getStartedButton.MouseEnter += (s, e) =>
            {
                getStartedButton.Size = new Size(260, 65);
                getStartedButton.Location = new Point(220, 377);
            };

            getStartedButton.MouseLeave += (s, e) =>
            {
                getStartedButton.Size = new Size(250, 60);
                getStartedButton.Location = new Point(225, 380);
            };

            // Add controls to welcome panel
            welcomePanel.Controls.AddRange(new Control[] {
                accentBar, titleLabel, subtitleLabel, descriptionLabel, getStartedButton
            });

            // Add panels to form
            this.Controls.Add(welcomePanel);

            // Setup timers for animations
            fadeTimer = new Timer { Interval = 50 };
            fadeTimer.Tick += FadeTimer_Tick;

            slideTimer = new Timer { Interval = 30 };
            slideTimer.Tick += SlideTimer_Tick;
        }

        private void StartAnimations()
        {
            fadeTimer.Start();
            slideTimer.Start();
        }

        private void FadeTimer_Tick(object sender, EventArgs e)
        {
            opacity += 0.05;
            if (opacity >= 1.0)
            {
                opacity = 1.0;
                fadeTimer.Stop();
            }
            this.Opacity = opacity;
        }

        private void SlideTimer_Tick(object sender, EventArgs e)
        {
            slidePosition -= 2;
            if (slidePosition <= 0)
            {
                slidePosition = 0;
                slideTimer.Stop();
            }

            welcomePanel.Location = new Point(100, 125 + slidePosition);
            
            // Update shadow panel position
            foreach (Control control in this.Controls)
            {
                if (control.BackColor == Color.FromArgb(50, 0, 0, 0))
                {
                    control.Location = new Point(105, 130 + slidePosition);
                    break;
                }
            }
        }

        private void GetStartedButton_Click(object sender, EventArgs e)
        {
            // Create fade-out effect before transitioning
            Timer fadeOutTimer = new Timer { Interval = 30 };
            fadeOutTimer.Tick += (s, args) =>
            {
                this.Opacity -= 0.1;
                if (this.Opacity <= 0)
                {
                    fadeOutTimer.Stop();
                    
                    // Show main form
                    Form1 mainForm = new Form1();
                    mainForm.Show();
                    
                    // Hide welcome form
                    this.Hide();
                }
            };
            fadeOutTimer.Start();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (fadeTimer != null)
            {
                fadeTimer.Stop();
                fadeTimer.Dispose();
            }
            if (slideTimer != null)
            {
                slideTimer.Stop();
                slideTimer.Dispose();
            }
            base.OnFormClosing(e);
        }
    }
}
