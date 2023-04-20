namespace ReadingJournal.Models
{
	using System.Collections.Generic;
	public class BookViewModelList
	{
		public List<BookViewModel> List { get; set; }
		public int CurrentPage { get; set; }

		public int TotalPages { get; set; }
	}
}
