using CellTracker.Api.Models.Configuration;

namespace CellTracker.Api.Services.WorkStationService
{
    public interface IWorkStationService
    {
        public Task<WorkStation> AddWorkStation(WorkStation workStation);
        public Task<IEnumerable<WorkStation>> GetAllWorkStations();
        public Task<WorkStation> GetWorkStationById(Guid id);
        public Task<bool> RemoveWorkStationById(Guid id);
        public Task<WorkStation> UpdateWorkStation(WorkStation task);
    }
}
