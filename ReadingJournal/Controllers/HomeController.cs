namespace ReadingJournal.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using ReadingJournal.DataModels;
    using ReadingJournal.Models;
    using ReadingJournal.Services;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Text.Json;
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBookService bookService;

		private static readonly int booksPerPage = 9;
		private static readonly int reviewsPerPage = 10;

        public object BookService { get; set; }

        public HomeController(IBookService bookService)
        {
            this.bookService = bookService;
        }

        public IActionResult Index(int currentPage = 1)
        {
            var skip = (currentPage - 1) * HomeController.booksPerPage;
            var take = HomeController.booksPerPage;

            var books = this.bookService.GetAll(skip, take);
            var totalBooksCount = this.bookService.GetCount();

            var totalPages = totalBooksCount / HomeController.booksPerPage;
            if(totalBooksCount % HomeController.booksPerPage > 0)
            {
                totalPages++;
            }


			var model = new BookViewModelList
            {
                List = GetBooksViewModel(books),
                CurrentPage = currentPage,
                TotalPages = totalPages
			};

            return View(model);
        }

        public IActionResult DeleteBook(int bookId)
        {
            this.bookService.Delete(bookId);
            return Ok();
        }

		[HttpPost]
		public IActionResult EditBook(BookViewModel book)
        {
			this.bookService.Update(GetBookDataModel(book));
			return RedirectToAction("Index");
		}

        public IActionResult BookEditDetails(int bookId)
        {
            var bookDataModel = this.bookService.GetById(bookId);

			return View(GetBookViewModel(bookDataModel));
        }

        [HttpGet]
        public IActionResult AddBook()
        {
            return View();
        }

        [HttpPost]
		public IActionResult AddBook(BookViewModel book)
		{
            this.bookService.Add(GetBookDataModel(book));
			return RedirectToAction("Index");
		}

		[HttpGet]
		public IActionResult BookDetails(int bookId, int currentPage = 1)
		{
			var bookViewModel = GetBookViewModel(this.bookService.GetBookAndScore(bookId));

			var reviewsViewModel = GetReviewsByByBook(bookId, currentPage);

			var viewModel = new Tuple<BookViewModel, ReviewViewModelList>(bookViewModel, reviewsViewModel);

			return View(viewModel);
		}

		[HttpPost]
		public IActionResult AddReview([FromBody] JsonElement data)
		{
			var review = JsonSerializer.Deserialize<ReviewViewModel>(data.GetRawText());

            this.bookService.AddReview(GetReviewDataModel(review));

			return Ok();
		}

		public IActionResult BestBooks()
		{
			var topBooks = this.bookService.GetTopBooks();

            var model = new BookViewModelList
            {
                List = GetBooksViewModel(topBooks),
                CurrentPage = 0,
                TotalPages = 1
            };

            return View(model);
		}

		public ReviewViewModelList GetReviewsByByBook(int bookId, int currentPage = 1)
		{
			var skip = (currentPage - 1) * HomeController.reviewsPerPage;
			var take = HomeController.reviewsPerPage;

			var reviews = this.bookService.GetAll(bookId, skip, take);
			var totalReviewsByBookCount = this.bookService.GetReviewsCount(bookId);

			var totalPages = totalReviewsByBookCount / HomeController.reviewsPerPage;
			if (totalReviewsByBookCount % HomeController.reviewsPerPage > 0)
			{
				totalPages++;
			}

			var reviewViewModelList = new ReviewViewModelList
			{
				List = GetReviewsViewModel(reviews),
				CurrentPage = currentPage,
				TotalPages = totalPages
			};

			return reviewViewModelList;
		}

		public IActionResult AboutUs()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

		public Book GetBookDataModel(BookViewModel book)
        {
            return new Book
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                Genre = book.Genre,
                PictureURL = book.PictureURL,
                PublishDate = book.PublishDate,
                Description = book.Description
            };
		}

		private BookViewModel GetBookViewModel(Book book)
		{
            return new BookViewModel
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                PictureURL = book.PictureURL,
                Description = book.Description,
                Genre = book.Genre,
				PublishDate = book.PublishDate,
                Score = book.Score
            };
		}

		private List<BookViewModel> GetBooksViewModel(List<Book> source)
        {
            var books = new List<BookViewModel>();
            
            foreach(var book in source) 
            {
                books.Add(this.GetBookViewModel(book));
            }

            return books;
        }

		private Review GetReviewDataModel(ReviewViewModel review)
		{
			return new Review
			{
				Id = review.Id,
                BookId = review.BookId,
				Score = review.Score,
				FromName = review.FromName,
				Description = review.Description,
			};
		}

		private ReviewViewModel GetReviewViewModel (Review review)
		{
			return new ReviewViewModel
			{
				FromName = review.FromName,
				Description = review.Description,
				Score = review.Score
			};
		}

		private List<ReviewViewModel> GetReviewsViewModel(List<Review> source)
		{
			var reviews = new List<ReviewViewModel>();

			foreach (var review in source)
			{
				reviews.Add(this.GetReviewViewModel(review));
			}

			return reviews;
		}
	}
}
