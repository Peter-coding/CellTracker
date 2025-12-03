namespace CellTracker.Api.Models.Dto
{
    public class CreateOperatorTaskDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int QuantityGoal { get; set; }
        public Guid WorkStationId { get; set; }
    }
}
