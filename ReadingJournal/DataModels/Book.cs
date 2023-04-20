namespace ReadingJournal.DataModels
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Book
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string Genre { get; set; }

		public string Description { get; set; }

		public string PictureURL { get; set; }

        public DateTime? PublishDate { get; set; }

        public bool IsDeleted { get; set; }

		[NotMapped]
		public string Score { get; set; }
	}
}
