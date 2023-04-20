namespace ReadingJournal.Services
{
    using ReadingJournal.DataAccess;
    using ReadingJournal.DataModels;
    using System;
    using System.Collections.Generic;
	using System.Linq;
	public class BookService: IBookService
    {
        private readonly AppDBContext db;
        public BookService(AppDBContext db)
        {
            this.db = db;
        }
        public List<Book> GetAll(int skip, int take)
        {
            return this.db.Books.Where(x => x.IsDeleted == false).OrderByDescending(x => x.Id).Skip(skip).Take(take).ToList();
        }

		public int GetCount()
		{
            return this.db.Books.Where(x => x.IsDeleted == false).Count();
		}

        public void Add(Book book)
        {
            this.db.Books.Add(book);
            this.db.SaveChanges();
        }

        public Book GetById(int id)
        {
            return this.db.Books.FirstOrDefault(x => x.Id == id);
        }

		public void Update(Book book)
		{
            var bookToUpdate = this.db.Books.FirstOrDefault(x => x.Id == book.Id);

            if(bookToUpdate == null) { return; }

            bookToUpdate.Author = book.Author;
            bookToUpdate.Title = book.Title;
            bookToUpdate.Description = book.Description;
            bookToUpdate.PictureURL = book.PictureURL;
            bookToUpdate.PublishDate = book.PublishDate;

            this.db.SaveChanges();
		}

		public void Delete(int id)
		{
            var bookToDelete = this.db.Books.FirstOrDefault(x => x.Id == id);
            if(bookToDelete == null) { return; }

			bookToDelete.IsDeleted = true;
            this.db.SaveChanges();
		}

        public void AddReview(Review review)
        {
			this.db.Reviews.Add(review);
			this.db.SaveChanges();
		}

		public string GetScore(int bookId)
		{
			var scores = this.db.Reviews
	            .Where(i => i.BookId == bookId)
	            .Select(i => i.Score)
	            .ToList();
			var averageScore = scores.DefaultIfEmpty(5).Average().ToString("0.00");

			return averageScore;
		}

        public Book GetBookAndScore(int bookId)
        {
            var bookDetailsData = this.GetById(bookId);

			bookDetailsData.Score = this.GetScore(bookId);

            return bookDetailsData;
		}

		public List<Review> GetAll(int bookId, int skip, int take)
		{
			return this.db.Reviews.Where(x => x.BookId == bookId).OrderByDescending(x => x.Id).Skip(skip).Take(take).ToList();
		}

		public int GetReviewsCount(int bookId)
		{
			return this.db.Reviews.Where(x => x.BookId == bookId).Count();
		}

        public List<Book> GetTopBooks()
        {
            var topBooks = this.db.Books
                .Join(
                    this.db.Reviews,
                    b => b.Id,
                    r => r.BookId,
                    (b, r) => new { Book = b, Score = r.Score }
                )
                .GroupBy(
                    b => new { b.Book.Id, b.Book.Title, b.Book.Author, b.Book.Genre },
                    (key, group) => new { Book = key, AverageScore = group.Average(r => r.Score) }
                )
                .Where(b => Math.Round(b.AverageScore, 2) == 5.00)
                .OrderByDescending(b => b.Book.Title)
                .Select(b => new Book
                {
                    Title = b.Book.Title,
                    Author = b.Book.Author,
                    Id = b.Book.Id
                })
                .ToList();

            return topBooks;
        }
    }
}
