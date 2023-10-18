using System.Text;
using System.Text.Json;
using WindowSubs.GPT.Models;

namespace WindowSubs.GPT
{
    public class Translate
    {
        private const string OpenAiApiUrl = "https://api.openai.com/v1/chat/completions";

        public static async Task<string> TranslateAsync(string text, string targetLanguage, string apiKey)
        {
            var requestObj = new
            {
                model = "gpt-3.5-turbo",
                messages = new object[]
                {
                new { role = "system", content = "You are a helpful assistant." },
                new { role = "user", content = $"Translate the following text to {targetLanguage}: {text}" }
                },
                temperature = 0,
                max_tokens = 256
            };

            var jsonContent = JsonSerializer.Serialize(requestObj);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

            var response = await httpClient.PostAsync(OpenAiApiUrl, content);
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var translationResponse = JsonSerializer.Deserialize<TranslationResponse>(jsonResponse);
            var translatedText = translationResponse?.Choices.FirstOrDefault()?.Message.Content;

            return translatedText ?? string.Empty;
        }
    }
}
