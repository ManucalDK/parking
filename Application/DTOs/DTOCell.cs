using AppCore.Enums;

namespace Application.DTOs
{
    public class DTOCell : BaseDTO
    {
        public VehicleTypeEnum IdVehicleType { get; set; }

        public int NumCellAvaliable { get; set; }
    }
}
