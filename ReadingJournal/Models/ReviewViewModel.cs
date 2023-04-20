namespace ReadingJournal.Models
{
	public class ReviewViewModel
	{
		public int Id { get; set; }

		public int BookId { get; set; }

		public int Score { get; set; }

		public string FromName { get; set; }

		public string Description { get; set; }
	}
}
