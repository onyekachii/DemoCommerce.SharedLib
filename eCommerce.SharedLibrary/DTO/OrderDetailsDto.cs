
using System.ComponentModel.DataAnnotations;

namespace eCommerce.SharedLibrary.DTO
{
    public record OrderDetailsDto(
        [Required]int OrderId,
        [Required] int ProductId,
        [Required] int ClientId,
        [Required] int PurchasedQuantity,
        [Required] DateTime OrderedDate,
        [Required] string ProductName,
        [Required] string Phone,
        [Required, EmailAddress] string Email,
        [Required, DataType(DataType.Currency)] decimal UnitPrice,
        [Required, DataType(DataType.Currency)] decimal TotalPrice
    );
}
