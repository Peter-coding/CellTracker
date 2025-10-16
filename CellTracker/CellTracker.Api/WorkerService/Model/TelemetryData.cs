
namespace CellTracker.Api.Ingestion.Model
{
    public class TelemetryData
    {
        public DateTime TimeStamp { get; set; }
        public string WorkStationId { get; set; }
        public string OperatorId { get; set; }
        public string ProductId { get; set; }
        public bool IsCompleted { get; set; }
        public byte Error { get; set; }
    }
}
