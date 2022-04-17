using System.Collections.Generic;

namespace EnglishWordHelperClient.Models
{
	public class WordDetails
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Transcription { get; set; }
		public string UrlAudio { get; set; }
		public List<string> Examples { get; set; } = new List<string>();
		public List<string> Translates { get; set; } = new List<string>();
		public List<string> Pictures { get; set; } = new List<string>();
	}
}
