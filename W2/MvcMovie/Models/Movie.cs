using System.ComponentModel.DataAnnotations;

namespace MvcMovie.Models;

public class Movie
{
    public int Id { get; set; }

    [Required]
    [StringLength(60)]
    public string Title { get; set; } = string.Empty;

    [Display(Name = "Release Date")]
    [DataType(DataType.Date)]
    public DateTime ReleaseDate { get; set; }

    [Required]
    public string Genre { get; set; } = string.Empty;

    [Range(0.01, 1000)]
    [DataType(DataType.Currency)]
    public decimal Price { get; set; }

    [Required]
    public string Rating { get; set; } = string.Empty;
}