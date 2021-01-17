using AppCore.Enums;

namespace AppCore.Entities
{
    public class CellEntity : EntityBase
    {
        public VehicleTypeEnum IdVehicleType { get; set; }

        public int NumCellAvaliable { get; set; }
    }
}
