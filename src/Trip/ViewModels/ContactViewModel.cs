using System.ComponentModel.DataAnnotations;

namespace WorldTrip.ViewModels
{
    public class ContactViewModel
    {
        [Required]
        [StringLength(80,MinimumLength=5)]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(1000, MinimumLength = 20)]
        public string Message { get; set; }
    }
}
