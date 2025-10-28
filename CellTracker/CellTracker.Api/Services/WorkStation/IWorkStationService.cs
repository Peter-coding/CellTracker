using CellTracker.Api.Models;

namespace CellTracker.Api.Services.WorkStationService
{
    public interface IWorkStationService
    {
        public WorkStation AddWorkStation(WorkStation workStation);
        public IQueryable<WorkStation> GetAllWorkStations();
        public Task<WorkStation> GetWorkStationById(Guid id);
        public void RemoveWorkStationById(Guid id);
        public void UpdateWorkStation(WorkStation task);
    }
}
