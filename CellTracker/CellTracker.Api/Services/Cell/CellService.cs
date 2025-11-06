using CellTracker.Api.Models.Configuration;
using CellTracker.Api.Models.Dto;
using CellTracker.Api.Repositories;
using Microsoft.EntityFrameworkCore;
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

        public async Task<Cell> AddCell(Cell cell)
        {
            _unitOfWork.CellRepository.Add(cell);
            var count = await _unitOfWork.CompleteAsync();
            if (count == 0)
            {
                return null;
            }
            return cell;
        }

        public async Task<IEnumerable<Cell>> GetAllCells()
        {
            return await _unitOfWork.CellRepository.GetAll().ToListAsync();
        }

        public async Task<Cell> GetCellById(Guid id)
        {
            return await _unitOfWork.CellRepository.GetByIdAsync(id);
        }

        public async Task<ICollection<WorkStation>> GetWorkStationsOfCellAsync(Guid guid)
        {
              return _unitOfWork.WorkStationRepository.GetAll().Where(ws => ws.CellId == guid).ToList();
        }

        public async Task<bool> RemoveCellById(Guid id)
        {
            _unitOfWork.CellRepository.RemoveById(id);
            var count = await _unitOfWork.CompleteAsync();
            if (count == 0)
            {
                return false;
            }

            return true;
        }

        public async Task<Cell> UpdateCell(Cell cell)
        {
            _unitOfWork.CellRepository.Update(cell);
            var count = await _unitOfWork.CompleteAsync();
            if (count == 0)
            {
                return null;
            }

            return cell;
        }
    }
}
