using System.ComponentModel.DataAnnotations;

namespace BooksProject.Models
{
    public class BookInputModel
    {
        [Required]
        [Range(0, int.MaxValue)]
        public int Id { get; set; }
        
        [Required]
        [StringLength(50, MinimumLength = 10)]
        public string Name { get; set; }
        
        [Required]
        [StringLength(50, MinimumLength = 10)]
        public string Author { get; set; }
        
        [Required]
        [Range(0, int.MaxValue)]
        public int Year { get; set; }
        
        [Required]
        [StringLength(50, MinimumLength = 10)]
        public string Genre { get; set; }
        
        [Required]
        [Range(0, int.MaxValue)]
        public int Price { get; set; }
        
        [Required]
        [Range(0, double.MaxValue)]
        public double Rating { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Reviews { get; set; }
    }
}
