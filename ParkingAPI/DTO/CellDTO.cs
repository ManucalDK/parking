using Entities.Interfaces;

namespace ParkingAPI.DTO
{
    class CellDTO: ICell
    {

        public int IdVehicleType { get; set; }

        public int NumCellAvaliable { get; set; }
    }
}
