using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels
{
    public class AddBookViewModel
    {
        [Display(Name = "Назва книги")]
        [Required(ErrorMessage = "Введіть назву книги")]
        [StringLength(maximumLength: 500, MinimumLength = 1)]
        public string Title { get; set; }

        [Display(Name = "Кількість сторінок")]
        [Required(ErrorMessage = "Вкажіть кількість аркушів")]
        public int Pages { get; set; }

        [Display(Name = "Введіть Id автора")]
        [Required(ErrorMessage = "Вкажіть Id автора")]
        public int AuthorId { get; set; }
    }

    public class BookItemViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Назва книги")]
        [Required(ErrorMessage = "Введіть назву книги")]
        [StringLength(maximumLength: 500, MinimumLength = 1)]
        public string Title { get; set; }

        [Display(Name = "Кількість сторінок")]
        [Required(ErrorMessage = "Вкажіть кількість аркушів")]
        public int Pages { get; set; }

        [Display(Name = "Введіть Id автора")]
        [Required(ErrorMessage = "Вкажіть Id автора")]
        public string Author { get; set; }
    }

    public class EditBookViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Назва книги")]
        [Required(ErrorMessage = "Введіть назву книги")]
        [StringLength(maximumLength: 500, MinimumLength = 1)]
        public string Title { get; set; }

        [Display(Name = "Кількість сторінок")]
        [Required(ErrorMessage = "Вкажіть кількість аркушів")]
        public int Pages { get; set; }

        [Display(Name = "Введіть Id автора")]
        [Required(ErrorMessage = "Вкажіть Id автора")]
        public int AuthorId { get; set; }
    }

    public class SearchBookViewModel
    {
        public string Title { get; set; }

        public string Author { get; set; }

    }

    public class BookViewModel
    {
        public IEnumerable<BookItemViewModel> Books { get; set; }

        public int TotalPages { get; set; }

        [Range(1, short.MaxValue)]
        public int Pages { get; set; }
        public int CurrentPage { get; set; }

        public SearchBookViewModel Search { get; set; }
    }
}
