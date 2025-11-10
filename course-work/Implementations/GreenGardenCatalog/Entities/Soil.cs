using System.ComponentModel.DataAnnotations;

namespace GreenGardenCatalog.Entities
{
    public class Soil
    {
        public  int Id { get; set; }
        public required string Name { get; set; }

        [Range(0.0, 14.0)]
        public required double? Ph {  get; set; }

        public required string Type { get; set; }

        public ICollection<Plant>? Plants { get; set; }
    }
}
