namespace ReadingJournal.Models
{
    using System.Collections.Generic;
    public class ReviewViewModelList
	{
		public List<ReviewViewModel> List { get; set; }

		public int CurrentPage { get; set; }

		public int TotalPages { get; set; }
	}
}
