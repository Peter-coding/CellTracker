using CellTracker.Api.Models.Base;
using CellTracker.Api.Models.Configuration;
using System.Text.Json.Serialization;

namespace CellTracker.Api.Models.OperatorTask
{
    public class OperatorTask : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int QuantityGoal { get; set; }
        public Guid WorkStationId { get; set; }

        [JsonIgnore]
        public WorkStation WorkStation { get; set; }
    }
}
