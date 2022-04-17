using System.Collections.Generic;

namespace EnglishWordHelperClient.Models
{
    public class Word
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Transcription { get; set; }
        public List<string> Translates { get; set; } = new List<string>();
    }
}
