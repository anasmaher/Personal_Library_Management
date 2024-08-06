using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagement.Models
{
    public class Book
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(1000)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public string PhotoUrl { get; set; }

        public string PdfUrl { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }

        public virtual IdentityUserEx User{ get; set; }
        
        [ForeignKey("Author")]
        public int AuthorId { get; set; } 

        public virtual Author Author { get; set; }
    }
}
