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

        [Column("CellId")]
        public string CellId { get; set; }

        [Column("ProductionLineId")]
        public string ProductionLineId { get; set; }

        [Column("FactoryId")]
        public string FactoryId { get; set; }

        [Column("OperatorId")]
        public string OperatorId { get; set; }

        [Column("ProductId")]
        public string ProductId { get; set; }

        [Column("IsCompleted")]
        public bool IsCompleted { get; set; }

        [Column("Error")]
        public byte Error { get; set; }

        [Column("TimeStamp", IsTimestamp = true)]
        public DateTime TimeStamp { get; set; }
        
    }
}
