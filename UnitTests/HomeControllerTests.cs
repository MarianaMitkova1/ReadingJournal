namespace UnitTests
{
    using Microsoft.AspNetCore.Mvc;
    using NUnit.Framework;
    using ReadingJournal.Controllers;
    using ReadingJournal.DataModels;
    using ReadingJournal.Models;
    using ReadingJournal.Services;
    using System;
    using System.Collections.Generic;
    using System.Text.Json;

    public class HomeControllerTests
    {
        private IBookService bookService;
        private HomeController homeController;

        [SetUp]
        public void Setup()
        {
            this.bookService = new MockBookService();
            this.homeController = new HomeController(this.bookService);
        }

        [Test]
        public void Constructor_WithBookService_InitializesController()
        {
            var homeController = new HomeController(this.bookService);

            Assert.IsNotNull(homeController);
        }

        [Test]
        public void DeleteBook_WithValidBookId_ReturnsOkResult()
        {
            var bookId = 1;

            var result = this.homeController.DeleteBook(bookId);

            Assert.IsInstanceOf<OkResult>(result);
        }

        [Test]
        public void AddBook_WithValidBook_ReturnsRedirectToActionResult()
        {
            var book = new BookViewModel { Title = "Test Book", Author = "Test Author" };

            var result = this.homeController.AddBook(book);

            Assert.IsInstanceOf<RedirectToActionResult>(result);
        }

        [Test]
        public void AddReview_WithValidReview_ReturnsOkResult()
        {
            var review = new ReviewViewModel {  };
            review.BookId = 1;
            review.FromName = "Test";
            review.Score = 3;
            review.Description = "Test Description";

            var json = JsonSerializer.Serialize(review);
            var jsonElement = JsonDocument.Parse(json).RootElement;

            var result = this.homeController.AddReview(jsonElement);

            Assert.IsInstanceOf<OkResult>(result);
        }

        [Test]
        public void GetBookDataModel_ReturnsCorrectBookInstance()
        {
            var bookViewModel = new BookViewModel
            {
                Id = 1,
                Title = "Test Book",
                Author = "John Doe",
                Genre = "Fiction",
                PictureURL = "https://example.com/book.jpg",
                PublishDate = DateTime.Now,
                Description = "This is a test book."
            };

            var result = homeController.GetBookDataModel(bookViewModel);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<Book>(result);
            Assert.That(result.Id, Is.EqualTo(bookViewModel.Id));
            Assert.That(result.Title, Is.EqualTo(bookViewModel.Title));
            Assert.That(result.Author, Is.EqualTo(bookViewModel.Author));
            Assert.That(result.Genre, Is.EqualTo(bookViewModel.Genre));
            Assert.That(result.PictureURL, Is.EqualTo(bookViewModel.PictureURL));
            Assert.That(result.PublishDate, Is.EqualTo(bookViewModel.PublishDate));
            Assert.That(result.Description, Is.EqualTo(bookViewModel.Description));
        }
    }

    public class MockBookService : IBookService
    {
        public void Add(Book book) { }
        public void AddReview(Review review) { }
        public void Delete(int bookId) { }
        public List<Book> GetAll(int skip, int take) { return null; }
        public List<Review> GetAll(int bookId, int skip, int take) { return null; }

        public Book GetBookAndScore(int bookId)
        {
            throw new System.NotImplementedException();
        }

        public Book GetById(int bookId) { return null; }
        public int GetCount() { return 0; }
        public int GetReviewsCount(int bookId) { return 0; }

        public string GetScore(int id){ return id.ToString(); }

        public List<Book> GetTopBooks() { return null; }
        public void Update(Book book) { }
    }
}
