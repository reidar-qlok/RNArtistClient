using System.ComponentModel.DataAnnotations;

namespace ArtistClient.Models
{
    public class User
    {
        [Key]
        public string Username { get; set; }
        public string DisplayName { get; set; }
    }
}
