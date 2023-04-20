namespace ReadingJournal.Services
{
    using System.Collections.Generic;
    using ReadingJournal.DataModels;

    public interface IBookService
    {
        List<Book> GetAll(int skip, int take);

        int GetCount();

        void Add(Book book);

        Book GetById(int id);

        void Update(Book book);

        void Delete(int id);

		void AddReview(Review review);

		string GetScore(int id);

        Book GetBookAndScore(int bookId);

        List<Review> GetAll(int bookId, int skip, int take);

		int GetReviewsCount(int bookId);

        List<Book> GetTopBooks();
	}
}
