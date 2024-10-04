using System.ComponentModel.DataAnnotations;

namespace Store.Services.Services.BasketService.Dto
{
    public class BasketItemDto
    {
        [Required]
        [Range(1,int.MaxValue)]
        public int ProductId { get; set; }
        [Required]

        public string ProductName { get; set; }
        [Required]
        [Range(0.1, double.MaxValue,ErrorMessage ="Price Must Be Greter than Zero")]

        public decimal Price { get; set; }
        [Required]
        [Range(1,10, ErrorMessage = "Quantity Must Be Greter than Zero")]
        public int Quantity { get; set; }
        [Required]

        public string PictureUrl { get; set; }
        [Required]

        public string BrandName { get; set; }
        [Required]

        public string TypeName { get; set; }
    }
}