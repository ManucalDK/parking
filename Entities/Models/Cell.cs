using Entities.Interfaces;

namespace Entities.Models
{
    public class Cell: ICell
    {
        public int Id { get; set; }

        public int IdVehicleType { get; set; }

        public int NumCellAvaliable { get; set; }
    }
}
