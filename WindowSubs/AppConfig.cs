using WindowSubs.GPT.Models;

namespace WindowSubs
{
    public class AppConfig
    {
        public string ApiKey { get; set; }
        public int ChunkTime { get; set; }
        public Language Language { get; set; }
    }
}
