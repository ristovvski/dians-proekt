
using System.ComponentModel.DataAnnotations;

namespace Culturolocator.Models
{
    public class HistoricalCulturalObjects : BaseEntity
    {
        public double Lon {  get; set; }
        public double Lat { get; set; }
        public string FullAddress { get; set; }

        public Guid RegionId {  get; set; }
        public Region Region {  get; set; }

        public Guid TypeId { get; set; }
        public Type Type { get; set; }

        public HistoricalCulturalObjects(string Name) : base(Name) { }
    }
}
