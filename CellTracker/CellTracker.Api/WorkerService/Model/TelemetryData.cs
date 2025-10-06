using InfluxDB.Client.Core;

namespace CellTracker.Api.Ingestion.Model
{
    [Measurement("Telemetry")]
    public class TelemetryData
    {
        [Column("TelemetryId", IsTag = true)]
        public int Id { get; set; }

        [Column("WorkStationId")]
        public string WorkStationId { get; set; }

        [Column("OperatorId")]
        public string OperatorId { get; set; }

        [Column("TimeStamp", IsTimestamp = true)]
        public DateTime TimeStamp { get; set; }
        
    }
}
