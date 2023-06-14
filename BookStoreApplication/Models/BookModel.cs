using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using BookStoreApplication.Helpers;
using Microsoft.AspNetCore.Http;

namespace BookStoreApplication.Models
{
    public class BookModel
    {
        public int Id { get; set; }
        [StringLength(100, MinimumLength = 5)]
        [Required(ErrorMessage = "Please enter the title")]
        //[MyCustomValidation]
        public string Title { get; set; }
        [StringLength(100, MinimumLength = 5)]
        [Required]
        public string Author { get; set; }
        [Required]
        public string Description { get; set; }
        public string Category { get; set; }
        [Required]
        public string Language { get; set; }
        [Required (ErrorMessage ="Please enter number of pages")]  // This custom message will not work if we do not update in BookRepository and make it nullabale
        [Display(Name = "Total Number of Pages")]      // This is for display
        public int? TotalPages { get; set; }
        [Display(Name = "Upload Cover Photo of Book")]
        [Required]
        public IFormFile CoverPhoto { get; set; }
        public string CoverImageUrl { get; set; }

        [Display(Name = "Upload Gallery Photos of Book")]
        [Required]
        public IFormFileCollection GalleryFiles { get; set; }
        public List<GalleryModel> Gallery { get; set; }

        [Display(Name = "Upload PDF of Book")]
        [Required]
        public IFormFile BookPdf { get; set; }
        public string BookPdfUrl { get; set; }
    }
}
