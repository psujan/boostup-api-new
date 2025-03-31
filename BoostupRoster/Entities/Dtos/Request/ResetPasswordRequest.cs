using System.ComponentModel.DataAnnotations;

namespace Boostup.API.Entities.Dtos.Request
{
    public class ResetPasswordRequest
    {
        [Required]
        public string Email { get; set; }
    }
}
