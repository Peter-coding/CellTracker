using CellTracker.Api.Models.Configuration;
using CellTracker.Api.Models.Dto;
using CellTracker.Api.Models.Statistics;

namespace CellTracker.Api.Services.WorkStationService
{
    public interface IWorkStationService
    {
        public Task<WorkStation> AddWorkStation(CreateWorkStationDto workStationDto);
        public Task<IEnumerable<WorkStation>> GetAllWorkStations();
        public Task<WorkStation> GetWorkStationById(Guid id);
        public Task<bool> RemoveWorkStationById(Guid id);
        public Task<WorkStation> UpdateWorkStation(UpdateWorkStationDto workStationDto);
        public Task<QualityRatio> GetEfficiencyOfWorkStation(Guid id);
    }
}
