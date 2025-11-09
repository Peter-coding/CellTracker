using CellTracker.Api.Models.Configuration;
using CellTracker.Api.Models.Dto;
using CellTracker.Api.Repositories;
using InfluxDB.Client.Api.Domain;
using Microsoft.EntityFrameworkCore;

namespace CellTracker.Api.Services.WorkStationService
{
    public class WorkStationService : IWorkStationService
    {
        private readonly IUnitOfWork _unitOfWork;
        public WorkStationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<WorkStation> AddWorkStation(WorkStationDto workStationDto)
        {
            var cell = await _unitOfWork.CellRepository.GetByIdAsync(workStationDto.CellId);
            if(cell == null)
            {
                throw new ArgumentException("Cell not found");
            }

            WorkStation workStation = new WorkStation
            {
                Name = workStationDto.Name,
                Description = workStationDto.Description,
                CellId = workStationDto.CellId,
                OrdinalNumber = await GetNextOrdinalNumberForWorkStation(workStationDto.CellId),
                CreatedAt = DateTime.Now
            };
            _unitOfWork.WorkStationRepository.Add(workStation);
            var count = await _unitOfWork.CompleteAsync();
            if (count == 0)
            {
                return null;
            }

            return workStation;
        }

        public async Task<IEnumerable<WorkStation>> GetAllWorkStations()
        {
            return await _unitOfWork.WorkStationRepository.GetAll().ToListAsync();
        }

        public async Task<WorkStation> GetWorkStationById(Guid id)
        {
            return await _unitOfWork.WorkStationRepository.GetByIdAsync(id);
        }

        public async Task<bool> RemoveWorkStationById(Guid id)
        {
            var item = await _unitOfWork.WorkStationRepository.GetByIdAsync(id);
            if(item.IsDeleted == true)
            {
                return true;
            }
            _unitOfWork.WorkStationRepository.RemoveById(id);
            var count = await _unitOfWork.CompleteAsync();
            if (count == 0)
            {
                return false;
            }

            return true;
        }

        public async Task<WorkStation> UpdateWorkStation(WorkStation workStation)
        {
            _unitOfWork.WorkStationRepository.Update(workStation);
            var count = await _unitOfWork.CompleteAsync();
            if (count == 0)
            {
                return null;
            }

            return workStation;
        }

        private async Task<int> GetNextOrdinalNumberForWorkStation(Guid cellId)
        {
            var cell = await _unitOfWork.CellRepository.GetByIdAsync(cellId);
            if (cell != null && !cell.WorkStations.Any())
            {
                return 1;
            }
            return cell!.WorkStations.Max(ws => ws.OrdinalNumber) + 1;
        }
    }
}
