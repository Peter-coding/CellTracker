using CellTracker.Api.Models;
using CellTracker.Api.Repositories;

namespace CellTracker.Api.Services.WorkStationService
{
    public class WorkStationService : IWorkStationService
    {
        private readonly IUnitOfWork _unitOfWork;
        public WorkStationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public WorkStation AddWorkStation(WorkStation workStation)
        {
            throw new NotImplementedException();
        }

        public IQueryable<WorkStation> GetAllWorkStations()
        {
            throw new NotImplementedException();
        }

        public Task<WorkStation> GetWorkStationById(Guid id)
        {
            throw new NotImplementedException();
        }

        public void RemoveWorkStationById(Guid id)
        {
            throw new NotImplementedException();
        }

        public void UpdateWorkStation(WorkStation task)
        {
            throw new NotImplementedException();
        }
    }
}
