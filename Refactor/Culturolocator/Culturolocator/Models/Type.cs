using System.ComponentModel.DataAnnotations;

namespace Culturolocator.Models
{
    public class Type : BaseEntity
    {
        public List<HistoricalCulturalObjects> HistoricalCulturalObjects { get; set; }

        public Type(string Name) : base(Name)
        {
        }
    }
}
