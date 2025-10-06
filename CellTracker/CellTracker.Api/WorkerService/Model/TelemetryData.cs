namespace CellTracker.Api.Ingestion.Model
{
    public class TelemetryData
    {
        public int Id { get; set; }
        public string WorkStationId { get; set; }
        public string OperatorId { get; set; }
        public DateTime TimeStamp { get; set; }
        
    }
}
