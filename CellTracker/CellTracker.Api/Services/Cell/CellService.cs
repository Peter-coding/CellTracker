using CellTracker.Api.Models;
using CellTracker.Api.Repositories;

namespace CellTracker.Api.Services.CellService
{
    public class CellService : ICellService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CellService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public Cell AddCell(Cell cell)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Cell> GetAllCells()
        {
            throw new NotImplementedException();
        }

        public Task<Cell> GetCellById(Guid id)
        {
            throw new NotImplementedException();
        }

        public void RemoveCellById(Guid id)
        {
            throw new NotImplementedException();
        }

        public void UpdateCell(Cell cell)
        {
            throw new NotImplementedException();
        }
    }
}
