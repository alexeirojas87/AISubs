using System.Text.Json;

namespace WindowSubs.GPT
{
    public class WhisperTranscribe
    {
        private const string WhisperApiUrl = "https://api.openai.com/v1/audio/transcriptions";

        public static async Task<string> TranscribeAudioAsync(string filePath, string apiKey)
        {
            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

            using var fileStream = new FileStream(filePath, FileMode.Open);
            using var content = new MultipartFormDataContent
            {
                { new StreamContent(fileStream), "file", Path.GetFileName(filePath) },
                { new StringContent("whisper-1"), "model" }
            };

            var response = await httpClient.PostAsync(WhisperApiUrl, content);
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var jsonDoc = JsonDocument.Parse(jsonResponse);
            var text = jsonDoc.RootElement.GetProperty("text").GetString();

            return text ?? string.Empty;
        }

    }
}
