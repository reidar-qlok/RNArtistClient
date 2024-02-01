using System.ComponentModel.DataAnnotations;

namespace ArtistClient.Models
{
    public class Artist
    {
        [Key]
        public int ArtistId { get; set; }
        [Required]
        [MaxLength(40, ErrorMessage = "Artist name must be 40 characters or less"), MinLength(2)]
        public string ArtistName { get; set; }
        public List<Song> Songs { get; set; }
    }
}