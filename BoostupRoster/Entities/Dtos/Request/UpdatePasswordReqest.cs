using System.ComponentModel.DataAnnotations;

namespace Boostup.API.Entities.Dtos.Request
{
    public class UpdatePasswordReqest
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string ResetToken { get; set; }

        [Required]
        public string Password { get; set; }

    }
}
