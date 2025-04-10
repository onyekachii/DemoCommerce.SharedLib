

using System.ComponentModel.DataAnnotations;

namespace eCommerce.SharedLibrary.DTO
{
    public record AppUserDto(
        int Id,
        [Required]string Name,
        [Required] string Phone,
        [Required, EmailAddress] string Email,
        [Required] string Password,
        [Required] string Address,
        [Required] string Role
        );
}
