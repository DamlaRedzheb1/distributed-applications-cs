using System.ComponentModel.DataAnnotations;

namespace GreenGardenCatalog.Entities
{
    public class Plant
    {
        public  int Id { get; set; }

        [MaxLength(150)]
        public required string Name { get; set; }
        public required string PlantingSeason { get; set; } 
        public double HeightInCm { get; set; }

        
        public decimal Price { get; set; }

        public bool IsInStock { get; set; }


        public int? SoilId { get; set; }
        public Soil? Soil { get; set; } 

        public int? FertilizerId { get; set; }
        public Fertilizer? Fertilizer { get; set; }
    }
}
