using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(maximumLength: 1000)]
        public string Title { get; set; }

        [Required]
        public int Pages { get; set; }

        [ForeignKey("Author")]
        public int AuthorId { get; set; }

        virtual public Author Author { get; set; }
    }
}
