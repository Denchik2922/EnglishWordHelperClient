using System.Collections.Generic;

namespace EnglishWordHelperClient.Models
{
	public class WordDetails
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Transcription { get; set; }
		public string UrlAudio { get; set; }
		public ICollection<WordExample> Examples { get; set; }
		public ICollection<WordTranslate> Translates { get; set; }
		public ICollection<WordPicture> Pictures { get; set; }
	}
}
