using MetroFramework.Forms;
using WindowSubs.Audio;
using WindowSubs.GPT.Models;

namespace WindowSubs
{
    public partial class Form1 : MetroForm
    {
        private readonly AppConfig _appConfig;
        private readonly AudioRecorder _audioRecorder;
        public Form1(AppConfig config)
        {
            InitializeComponent();

            _appConfig = config;
            FormBorderStyle = FormBorderStyle.None;
            TopMost = true;
            ShowInTaskbar = false;

            _audioRecorder = new AudioRecorder(_appConfig);
            _audioRecorder.TranscriptionReceived += OnTranscriptionReceived;
            _audioRecorder.OriginalAudioToTextReceived += OnOriginalAudioToTextReceived;
            LoadLanguages();
            ShowNotification("WindowsSub Status", "Initiated");
        }

        private void LoadLanguages()
        {
            comboBox1.Items.AddRange((object[])Enum.GetNames(typeof(Language)));
            comboBox1.SelectedItem = nameof(Language.Spanish);
        }
        private void ShowNotification(string title, string message)
        {
            notifyIcon1.BalloonTipTitle = title;
            notifyIcon1.BalloonTipText = message;
            notifyIcon1.ShowBalloonTip(2000);
        }

        private void CreateTryIcon()
        {
            var contextMenuStrip = new ContextMenuStrip();
            contextMenuStrip.Items.Add("Start", SystemIcons.Asterisk.ToBitmap(), Start);
            contextMenuStrip.Items.Add("Stop", SystemIcons.Hand.ToBitmap(), Stop);
            contextMenuStrip.Items.Add("Settings", SystemIcons.Application.ToBitmap(), Configuration);
            contextMenuStrip.Items.Add("Exit", SystemIcons.Warning.ToBitmap(), Exit);
            notifyIcon1.ContextMenuStrip = contextMenuStrip;
        }
        private void Stop(object sender, EventArgs e)
        {
            _audioRecorder.Stop();
            ShowNotification("WindowsSub Status", "Audio Stoped");
        }
        private void Exit(object sender, EventArgs e)
        {
            _audioRecorder.Stop();
            Application.Exit();
        }
        private void Configuration(object sender, EventArgs e)
        {
            // Show settings window
            var configForm = new Settings();
            configForm.Show();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            // Obtain screen dimensions.
            var screenWidth = Screen.PrimaryScreen.WorkingArea.Width;
            var screenHeight = Screen.PrimaryScreen.WorkingArea.Height;

            // Calculates the X position to center the window on the screen.
            var x = ((screenWidth - this.Width) / 2) + 50;

            // Calculates the Y position to place the window at the bottom of the screen.
            var y = (screenHeight - this.Height) - 50;

            // Sets the position of the window.
            Location = new Point(x, y);
            CreateTryIcon();
        }

        private void Start(object sender, EventArgs e)
        {
            _audioRecorder.Start();
            ShowNotification("WindowsSub Status", "Started audio capture and processing.");
        }
        private void OnTranscriptionReceived(string transcription)
        {
            Invoke(() =>
            {
                textBox2.Text += transcription + Environment.NewLine + Environment.NewLine;
            });
        }
        private void OnOriginalAudioToTextReceived(string originalText)
        {
            Invoke(() =>
            {
                textBox1.Text += originalText + Environment.NewLine + Environment.NewLine;
            });
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Enum.TryParse(comboBox1.SelectedItem.ToString(), out Language selectedLanguage))
            {
                _appConfig.Language = selectedLanguage;
            }
        }
    }
}