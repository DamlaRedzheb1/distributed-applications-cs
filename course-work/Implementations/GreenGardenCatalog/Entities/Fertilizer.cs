using System.ComponentModel.DataAnnotations;

namespace GreenGardenCatalog.Entities
{
    public class Fertilizer
    {
        public  int Id { get; set; }

        [MaxLength(150)]
        public required string Name { get; set; }
        public string Usage { get; set; }

        public bool IsInStock { get; set; }
        
        [Range(0.1, 100.0)]
        public double WeightInKg { get; set; }

        [Range(0.01, 10000.00)]
        public required decimal Price { get; set; }  

        public ICollection<Plant>? Plants { get; set; }
    }
}
