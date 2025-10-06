using CellTracker.Api.Models.Base;

namespace CellTracker.Api.Models.OperatorTask
{
    public class OperatorTask : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int QuantityGoal { get; set; }
    }
}
