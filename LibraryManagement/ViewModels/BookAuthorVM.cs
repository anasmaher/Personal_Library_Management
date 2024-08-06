using LibraryManagement.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagement.ViewModels
{
    public class BookAuthorVM
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        [MaxLength(1000)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public int AuthorId { get; set; }
        
        public List<Author> Authors { get; set; }

        [NotMapped]
        public IFormFile? Image { get; set; }

        [Required]
        public string PhotoUrl { get; set; }

        [NotMapped]
        public IFormFile? Pdf { get; set; }

        [Required]
        public string PdfUrl { get; set; }
    }
}
