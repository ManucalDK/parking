using AppCore.Entities;
using AppCore.Enums;
using AppCore.Exceptions;
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
            if (cellEntity.NumCellAvaliable < 0)
            {
                throw new CellException("No hay más celdas disponibles");
            }

            _cellRepository.Update(cellEntity);
            return cellEntity;
        }

        public CellEntity IncreaseCell(VehicleTypeEnum vehicleType, int increase)
        {
            var cellEntity = _cellRepository.List(cell => cell.IdVehicleType == vehicleType).FirstOrDefault();
            if(cellEntity.NumCellAvaliable < cellEntity.NumTotalCells)
            {
                cellEntity.NumCellAvaliable += increase;
            } else
            {
                throw new CellException("No hay más celdas disponibles");
            }
            
            _cellRepository.Update(cellEntity);
            return cellEntity;
        }
    }
}
