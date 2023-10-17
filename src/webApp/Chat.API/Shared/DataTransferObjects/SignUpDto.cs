using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects
{
    public record SignUpDto
    {
        [Required]
        [MaxLength(15)]
        public string Nickname { get; init; }

        [Required]
        public string Password { get; init; }
    }
}
