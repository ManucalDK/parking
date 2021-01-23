using AppCore.Enums;

namespace Application.DTOs
{
    public class DtoCell : BaseDto
    {
        public VehicleTypeEnum IdVehicleType { get; set; }

        public int NumCellAvaliable { get; set; }
    }
}
