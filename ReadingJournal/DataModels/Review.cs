namespace ReadingJournal.DataModels
{
	public class Review
	{
		public int Id { get; set; }

		public int BookId { get; set; }

		public int Score { get; set; }

		public string FromName { get; set; }

		public string Description { get; set; }
	}
}
