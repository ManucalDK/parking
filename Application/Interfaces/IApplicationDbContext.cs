using AppCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<CellEntity> Cells { get; set; }
        DbSet<EntryEntity> Entries { get; set; }
        DbSet<DepartureEntity> Departures { get; set; }
        DbSet<PlacaEntity> Placas { get; set; }

    }
}
