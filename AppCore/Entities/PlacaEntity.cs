using AppCore.Enums;

namespace AppCore.Entities
{
    public class PlacaEntity : EntityBase
    {
        public int Length { get; set; }

        public PlacaType Type { get; set; }

        public int LastNumberFrom { get; set; }
    }
}
