using System.Collections.Generic;

namespace EnglishWordHelperClient.Models
{
    public class Word
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Transcription { get; set; }
        public ICollection<WordTranslate> Translates { get; set; }
    }
}
