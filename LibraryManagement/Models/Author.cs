using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagement.Models
{
    public class Author
    {
        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength:50, ErrorMessage ="Name Cant Be more than 20 characters.")]
        public string Name { get; set; }

        public string PhotoUrl { get; set; }

        [NotMapped]
        public IFormFile? Image { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }

        public IdentityUserEx User { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
