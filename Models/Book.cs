using System.ComponentModel.DataAnnotations;

namespace BookLibrary.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        [Range(1400, 2023)]
        public int Year { get; set; }
        [Range(1, 10000)]
        public int Pages { get; set; }

    }
}
