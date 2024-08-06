using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Models
{
    public class IdentityUserEx : IdentityUser
    {
        [Required, MaxLength(50)]
        public string FirstName { get; set; }

        [Required, MaxLength(50)]
        public string LastName { get; set; }

        public virtual ICollection<Book> Books { get; set; }
        
        public ICollection<Author> Authors { get; set; }
    }
}
