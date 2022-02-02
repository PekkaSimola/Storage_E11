#nullable disable
using System.ComponentModel;

namespace Storage.Models.ViewModels
{
    public class InventoryViewModel
    {
        public int Id { get; set; }
         
        [DisplayName("Produkt")]
        public string Name { get; set; }

        [DisplayName("Pris (kr)")]
        public int Price { get; set; }

        [DisplayName("Antal i lager")]
        public int Count { get; set; }

        [DisplayName("Lagervärde (kr)")]
        public string InventoryValue { get; set; }
    }
}
