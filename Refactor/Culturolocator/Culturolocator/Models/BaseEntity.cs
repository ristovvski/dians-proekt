using System.ComponentModel.DataAnnotations;

namespace Culturolocator.Models
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }
        
        public BaseEntity(string Name)
        {
            this.Name = Name;
        }

    }
}
