using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediaVault.Data;

public class MediaItem
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El título es obligatorio.")]
    [StringLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string MediaType { get; set; } = "Book"; // "Book" o "Movie"

    public string? Genre { get; set; }

    [Required]
    public string Status { get; set; } = "Pending"; // "Pending" o "Completed"

    [Range(1, 5, ErrorMessage = "La calificación debe estar entre 1 y 5.")]
    public int Rating { get; set; } = 1;

    public string? Notes { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Relación con el usuario autenticado
    [Required]
    public string UserId { get; set; } = string.Empty;

    [ForeignKey("UserId")]
    public ApplicationUser? User { get; set; }
}