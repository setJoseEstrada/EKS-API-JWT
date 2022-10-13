using System.ComponentModel.DataAnnotations;

namespace APIEKS.Models.Request
{
    public class AuthRequest
    {
        [Required]
        public string correo { get; set; }
        [Required]
        public string contra { get; set; }
    }
}
