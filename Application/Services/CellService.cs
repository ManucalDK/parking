using AppCore.Entities;
using AppCore.Enums;
using Application.Interfaces;
using System.Linq;

namespace Application.Services
{
    public class CellService : ICellService
    {
        IRepository<CellEntity> _cellRepository;

        public CellService(IRepository<CellEntity> cellRepository)
        {
            _cellRepository = cellRepository;
        }

        public bool ExistsQuotaByVehicleType(VehicleTypeEnum vehicleType)
        {
            var cell = _cellRepository.List(c => c.IdVehicleType == vehicleType).FirstOrDefault();
            return cell?.NumCellAvaliable > 0;
        }

        public CellEntity DecreaseCell(VehicleTypeEnum vehicleType, int decrease)
        {
            var cellEntity = _cellRepository.List(cell => cell.IdVehicleType == vehicleType).FirstOrDefault();
            cellEntity.NumCellAvaliable -= decrease;
            _cellRepository.Update(cellEntity);
            return cellEntity;
        }

        public CellEntity IncreaseCell(VehicleTypeEnum vehicleType, int increase)
        {
            var cellEntity = _cellRepository.List(cell => cell.IdVehicleType == vehicleType).FirstOrDefault();
            cellEntity.NumCellAvaliable += increase;
            _cellRepository.Update(cellEntity);
            return cellEntity;
        }
    }
}
