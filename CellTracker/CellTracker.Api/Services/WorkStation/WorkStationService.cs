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

        public async Task<WorkStation> AddWorkStation(CreateWorkStationDto workStationDto)
        {
            var cell = await _unitOfWork.CellRepository.GetByIdAsync(workStationDto.CellId);
            if(cell == null)
            {
                throw new ArgumentException("Cell not found");
            }

            WorkStation workStation = new WorkStation
            {
                MqttDeviceId = workStationDto.MqttDeviceId,
                Name = workStationDto.Name,
                Description = workStationDto.Description,
                CellId = workStationDto.CellId,
                OrdinalNumber = await GetNextOrdinalNumberForWorkStation(workStationDto.CellId),
                CreatedAt = DateTime.UtcNow
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
            return await _unitOfWork.WorkStationRepository.GetAll().Where(workStation => workStation.IsDeleted != true).ToListAsync();
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

        public async Task<WorkStation> UpdateWorkStation(UpdateWorkStationDto workStationDto)
        {
            var workStation = await _unitOfWork.WorkStationRepository.GetByIdAsync(workStationDto.Id);

            //TODO add automapper
            workStation.Name = workStationDto.Name;
            workStation.Description = workStationDto.Description;
            workStation.CellId = workStationDto.CellId;
            workStation.OrdinalNumber = workStationDto.OrdinalNumber;
            workStation.IsDeleted = workStationDto.IsDeleted;
            workStation.MqttDeviceId = workStationDto.MqttDeviceId;
            workStation.ModifiedAt = DateTime.UtcNow;

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
            var workStations = await _unitOfWork.WorkStationRepository.GetAll()
                .Where(ws => ws.CellId == cellId).ToListAsync();

            if (cell != null && !workStations.Any())
            {
                return 1;
            }

            return workStations.Max(ws => ws.OrdinalNumber) + 1;
        }
    }
}
