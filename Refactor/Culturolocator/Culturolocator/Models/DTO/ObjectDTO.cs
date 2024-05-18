namespace Culturolocator.Models.DTO
{
    //used only to parse the json data into actual object
    public class ObjectDTO
    {
        public double lon {  get; set; }
        public double lat { get; set; }

        public String name { get; set; }

        public String type { get; set; }

        public String state {  get; set; }

        public String full_address {  get; set; }
    }
}
