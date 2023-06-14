using BookStoreApplication.Models;
using BookStoreApplication.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreApplication.Controllers
{
    [Route("[controller]/[action]")]
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository = null;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BookController(IBookRepository bookRepository, IWebHostEnvironment webHostEnvironment)
        {
            _bookRepository = bookRepository;
            _webHostEnvironment = webHostEnvironment;
        }
        [Route ("~/allbooks")]
       public async Task<ViewResult> GetAllBooks()
        {
            var data = await _bookRepository.GetAllBooks();
            return View(data);
        }
        [Route("~/book-details/{id:int}", Name ="bookDetailsRoute")]
        public async Task<ViewResult> GetBook(int id)
        {
            var data = await  _bookRepository.GetBookById(id);
            return View(data);
        }
        [Route("~/searchbooks")]
        public List<BookModel> SearchBooks (string bookName, string authorName)
        {
            return _bookRepository.SearchBook(bookName, authorName);
        }
        [Route("~/addnewbook")]
        [Authorize]
        public ViewResult AddNewBook(bool isSuccess = false, int bookId = 0)
        {
            ViewBag.IsSuccess = isSuccess;
            ViewBag.BookId = bookId;

            // Get all available languages using CultureInfo
            var languages = CultureInfo.GetCultures(CultureTypes.AllCultures)
                .Select(c => new SelectListItem { Text = c.DisplayName, Value = c.DisplayName });

            ViewBag.Language = languages;

            return View();
        }

        [Route("~/Addnewbook")] [HttpPost]
        public async Task<IActionResult> AddNewBook(BookModel bookModel)
        {
            if (ModelState.IsValid)
            {
                if (bookModel.CoverPhoto != null)
                {
                    string folder = "books/cover/";
                    bookModel.CoverImageUrl = await UploadImage(folder, bookModel.CoverPhoto);
                }

                if (bookModel != null)
                {
                    string folder = "books/gallery/";

                    bookModel.Gallery = new List<GalleryModel>();

                    foreach (var file in bookModel.GalleryFiles)
                    {
                        var gallery = new GalleryModel()
                        {
                            Name = file.FileName,
                            URL = await UploadImage(folder, file)
                        };
                        bookModel.Gallery.Add(gallery);
                        
                    }
                   
                }

                if (bookModel.BookPdf != null)
                {
                    string folder = "books/pdf/";
                    bookModel.BookPdfUrl = await UploadImage(folder, bookModel.BookPdf);
                }


                int id = await _bookRepository.AddNewBook(bookModel);
                if (id > 0)
                {
                    ViewBag.IsSuccess = true; // Set the IsSuccess value to true
                    ViewBag.BookId = id; // Set the BookId value
                    return RedirectToAction(nameof(AddNewBook), new { isSuccess = true, bookId = id });
                }
            }

            // Get all available languages using CultureInfo
            var languages = CultureInfo.GetCultures(CultureTypes.AllCultures)
                .Select(c => new SelectListItem { Text = c.DisplayName, Value = c.DisplayName });

            ViewBag.Language = languages;

            return View();
        }

        private async Task<string> UploadImage(string folderPath, IFormFile file)
        {
            
            folderPath += Guid.NewGuid().ToString() + "_" + file.FileName;

             
            string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folderPath);

            await file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));

            return "/" + folderPath;  // added "/" to show the image on UI
        }
    }
}
