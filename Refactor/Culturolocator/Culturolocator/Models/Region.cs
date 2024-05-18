using System.ComponentModel.DataAnnotations;

namespace Culturolocator.Models
{
    public class Region : BaseEntity
    {
        public List<HistoricalCulturalObjects> HistoricalCulturalObjects { get; set; }

        public Region(string Name) : base(Name)
        {

        }
    }
}
