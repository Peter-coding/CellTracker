using CellTracker.Api.Models.Configuration;
using CellTracker.Api.Models.Dto;
using CellTracker.Api.Repositories;
using System.Threading.Tasks;

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

        public async Task<ICollection<WorkStation>> GetWorkStationsOfCellAsync(Guid guid)
        {
              return _unitOfWork.WorkStationRepository.GetAll().Where(ws => ws.CellId == guid).ToList();
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
