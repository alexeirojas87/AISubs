using System.Text.Json;
using WindowSubs.GPT.Models;

namespace WindowSubs
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            LoadLanguages();
            LoadConfig();
        }
        private void LoadLanguages()
        {
            targetLanguageCombo.Items.AddRange((object[])Enum.GetNames(typeof(Language)));
            targetLanguageCombo.SelectedItem = nameof(Language.Spanish);
            targetLanguageCombo.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        private void LoadConfig()
        {
            if (File.Exists("config.json"))
            {
                var json = File.ReadAllText("config.json");
                var config = JsonSerializer.Deserialize<AppConfig>(json);

                if (config is not null)
                {
                    apiKeyTextBox.Text = config.ApiKey;
                    audioChunkNumeric.Value = config.ChunkTime;
                    targetLanguageCombo.SelectedIndex = (int)config.Language;
                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var config = new AppConfig
            {
                ApiKey = apiKeyTextBox.Text,
                ChunkTime = int.Parse(audioChunkNumeric.Text),
                Language = (Language)Enum.Parse(typeof(Language), targetLanguageCombo.SelectedItem.ToString()!)
            };

            var json = JsonSerializer.Serialize(config);
            File.WriteAllText("config.json", json);
            Application.Restart();
        }
    }
}
