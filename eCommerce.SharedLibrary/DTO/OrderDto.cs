using System.ComponentModel.DataAnnotations;

namespace eCommerce.SharedLibrary.DTO;

public record OrderDto(
        int Id,
        [Required, Range(1, int.MaxValue)] int ProductId,
        [Required, Range(1, int.MaxValue)] int ClientId,
        [Required, Range(1, int.MaxValue)] int PurchasedQuantity,
        DateTime OrderedDate
    );
