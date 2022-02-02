using System.ComponentModel.DataAnnotations;

namespace Storage.Models.Entities
{
    public class Product
    {
        public int Id { get; set; }

        [Display(Name = "Produkt")]
        [Required]
        [StringLength(40, ErrorMessage = "Produkt: 2 – 40 tecken", MinimumLength = 2)]
        public string? Name { get; set; }

        [Display(Name = "Pris")]
        [Required]
        [Range(0, 100000)]  
        public int Price { get; set; }

        [Display(Name = "Orderdatum")]
        [DataType(DataType.Date)]
        public DateTime Orderdate { get; set; }

        [Display(Name = "Kategori")]
        [Required]
        [StringLength(40, ErrorMessage = "Kategori: 2 – 40 tecken", MinimumLength = 2)]
        public string? Category { get; set; }

        [Display(Name = "Hylla")]
        [StringLength(20, ErrorMessage = "Hylla: 1 – 20 tecken", MinimumLength = 1)]
        public string? Shelf { get; set; }

        [Display(Name = "Mängd")]
        public int Count { get; set; }

        [Display(Name = "Beskrivning")]
        [StringLength(100, ErrorMessage = "Beskrivning: 0 – 100 tecken")]
        public string? Description { get; set; }
    }
}
