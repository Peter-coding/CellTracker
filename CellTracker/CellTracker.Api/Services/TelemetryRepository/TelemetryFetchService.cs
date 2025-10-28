using CellTracker.Api.Configuration.ExternalConnection;
using CellTracker.Api.Ingestion.Model;
using CellTracker.Api.Models;
using CellTracker.Api.Repositories;
using InfluxDB.Client;
using Microsoft.AspNetCore.Routing;
using System.Runtime.Intrinsics.X86;

namespace CellTracker.Api.Services.TelemetryRepository
{
    public class TelemetryFetchService : ITelemetryFetchService
    {
        private readonly InfluxDBClient _influxDBClient;
        private readonly IUnitOfWork _unitOfWork;
        public TelemetryFetchService(IUnitOfWork unitOfWork)
        {
            var connectionString = ConnectionConfiguration.GetInfluxDbConnectionString();
            _influxDBClient = new InfluxDBClient(connectionString);
            _unitOfWork = unitOfWork;
        }

        public async Task<List<TelemetryData>> GetBetweenAsync(DateTime from, DateTime to)
        {
            var fromUtc = from.ToUniversalTime();
            var toUtc = to.ToUniversalTime();

            string query = $@"
                from(bucket: ""CellTracker"")
                   |> range(start: {fromUtc:yyyy-MM-ddTHH:mm:ssZ}, stop: {toUtc:yyyy-MM-ddTHH:mm:ssZ})
                   |> filter(fn: (r) => r._measurement == ""Telemetry"")
                   |> pivot(rowKey: [""_time""], columnKey: [""_field""], valueColumn: ""_value"")
                 ";

            return await _influxDBClient.GetQueryApi().QueryAsync<TelemetryData>(query, Environment.GetEnvironmentVariable("INFLUXDB_ORG"));
        }

        public async Task<int> GetCountInCurrentShiftAsync(string operatorId, string workStationId)
        {
            var currentTime = DateTime.UtcNow;
            var shiftStart = GetCurrentShiftStart(currentTime);

            string query = $@"
                from(bucket: ""CellTracker"")
                    |> range(start: {shiftStart:yyyy-MM-ddTHH:mm:ssZ}, stop: {currentTime:yyyy-MM-ddTHH:mm:ssZ})
                    |> filter(fn: (r) => r._measurement == ""Telemetry"")
                    |> pivot(rowKey: [""_time""], columnKey: [""_field""], valueColumn: ""_value"")
                    |> filter(fn: (r) => r.OperatorId == ""{operatorId}"" and r.WorkStationId == ""{workStationId}"")
                ";

            var results = await _influxDBClient.GetQueryApi().QueryAsync<TelemetryData>(query, Environment.GetEnvironmentVariable("INFLUXDB_ORG"));

            return results.Count;
        }

        public async Task<List<TelemetryData>> GetTelemetryDataInCurrentShiftAsync(string operatorId, string workStationId)
        {
            var currentTime = DateTime.UtcNow;
            var shiftStart = GetCurrentShiftStart(currentTime);

            string query = $@"
                from(bucket: ""CellTracker"")
                    |> range(start: {shiftStart:yyyy-MM-ddTHH:mm:ssZ}, stop: {currentTime:yyyy-MM-ddTHH:mm:ssZ})
                    |> filter(fn: (r) => r._measurement == ""Telemetry"")
                    |> pivot(rowKey: [""_time""], columnKey: [""_field""], valueColumn: ""_value"")
                    |> filter(fn: (r) => r.OperatorId == ""{operatorId}"" and r.WorkStationId == ""{workStationId}"")
                ";

            return await _influxDBClient.GetQueryApi().QueryAsync<TelemetryData>(query, Environment.GetEnvironmentVariable("INFLUXDB_ORG"));
        }


        public async Task<Dictionary<string, int>> GetTelemetryCountPerWorkStationInCurrentShiftAsync(Guid cellId)
        {
           
        }

        public async Task<Dictionary<string, int>> GetTelemetryCountPerProductionLineAsync(Guid productionLineId)
        {

        }


        private DateTime GetCurrentShiftStart(DateTime currentTime)
        {
            var currentHour = currentTime.Hour;
            DateTime shiftStart;
            if (currentHour >= 6 && currentHour < 14)
            {
                shiftStart = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, 6, 0, 0, DateTimeKind.Utc);
            }
            else if (currentHour >= 14 && currentHour < 22)
            {
                shiftStart = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, 14, 0, 0, DateTimeKind.Utc);
            }
            else
            {
                var previousDay = currentTime.AddDays(-1);
                shiftStart = new DateTime(currentTime.Year, currentTime.Month, previousDay.Day, 22, 0, 0, DateTimeKind.Utc);
            }

            return shiftStart;
        }
    }
}
