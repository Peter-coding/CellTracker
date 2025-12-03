using CellTracker.Api.Configuration.ExternalConnection;
using CellTracker.Api.Ingestion.Model;
using CellTracker.Api.Models;
using CellTracker.Api.Repositories;
using CellTracker.Api.Services.CellService;
using InfluxDB.Client;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using System.Runtime.Intrinsics.X86;

namespace CellTracker.Api.Services.TelemetryRepository
{
    public class TelemetryFetchService : ITelemetryFetchService
    {
        private readonly InfluxDBClient _influxDBClient;
        public TelemetryFetchService(IUnitOfWork unitOfWork)
        {
            var connectionString = ConnectionConfiguration.GetInfluxDbConnectionString();
            _influxDBClient = new InfluxDBClient(connectionString);
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

        public async Task<List<TelemetryData>> GetTelemetryOfWsCurrentShiftAsync(Guid wsId, DateTime currentTime)
        {
            var shiftStart = GetCurrentShiftStart(currentTime);
            var shiftEnd = shiftStart.AddHours(8);

            string query = $@"
                from(bucket: ""CellTracker"")
                  |> range(start: {shiftStart:yyyy-MM-ddTHH:mm:ssZ}, stop: {shiftEnd:yyyy-MM-ddTHH:mm:ssZ})
                  |> filter(fn: (r) => r._measurement == ""Telemetry"")
                  |> pivot(rowKey: [""_time""], columnKey: [""_field""], valueColumn: ""_value"")
                  |> filter(fn: (r) => r.WorkStationId == ""{wsId}"")
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

            //string query = $@"
            //    from(bucket: ""CellTracker"")
            //        |> range(start: {shiftStart:yyyy-MM-ddTHH:mm:ssZ}, stop: {currentTime:yyyy-MM-ddTHH:mm:ssZ})
            //        |> filter(fn: (r) => r._measurement == ""Telemetry"")
            //        |> pivot(rowKey: [""_time""], columnKey: [""_field""], valueColumn: ""_value"")
            //        |> filter(fn: (r) => r.OperatorId == ""{operatorId}"" and r.WorkStationId == ""{workStationId}"")
            //    ";

            string query = $@"
                from(bucket: ""CellTracker"")
                    |> range(start: {shiftStart:yyyy-MM-ddTHH:mm:ssZ}, stop: {currentTime:yyyy-MM-ddTHH:mm:ssZ})
                    |> filter(fn: (r) => r._measurement == ""Telemetry"")
                    |> pivot(rowKey: [""_time""], columnKey: [""_field""], valueColumn: ""_value"")
                    |> filter(fn: (r) => r.WorkStationId == ""{workStationId}"")
                ";

            return await _influxDBClient.GetQueryApi().QueryAsync<TelemetryData>(query, Environment.GetEnvironmentVariable("INFLUXDB_ORG"));
        }


        //public async Task<Dictionary<string, int>> GetTelemetryCountPerWorkStationInCurrentShiftAsync(Guid cellId)
        //{
        //    var currentTime = DateTime.UtcNow;
        //    var shiftStart = GetCurrentShiftStart(currentTime);

        //    var workStations = await _cellService.GetWorkStationsOfCellAsync(cellId);
        //    var workStationIds = workStations.Select(ws => ws.Id.ToString()).ToList();

        //    string query = $@"
        //    from(bucket: ""CellTracker"")
        //      |> range(start: {shiftStart:yyyy-MM-ddTHH:mm:ssZ}, stop: {currentTime:yyyy-MM-ddTHH:mm:ssZ})
        //      |> filter(fn: (r) => 
        //          r._measurement == ""Telemetry"" and 
        //          contains(value: r.WorkStationId, set: {JsonConvert.SerializeObject(workStationIds)}))
        //      |> group(columns: [""WorkStationId""])
        //      |> count()
        //      |> keep(columns: [""WorkStationId"", ""_value""])
        //    ";

        //    var tables = await _influxDBClient.GetQueryApi().QueryAsync(query, Environment.GetEnvironmentVariable("INFLUXDB_ORG"));

        //    var result = new Dictionary<string, int>();

        //    foreach (var record in tables.SelectMany(t => t.Records))
        //    {
        //        var workStationId = record.GetValueByKey("WorkStationId")?.ToString();
        //        var count = Convert.ToInt32(record.GetValue());
        //        if (workStationId != null)
        //            result[workStationId] = count;
        //    }

        //    return result;
        //}

        public async Task<Dictionary<string, int>> GetTelemetryCountPerProductionLineAsync(Guid productionLineId)
        {
            //TODO: implement
            return new Dictionary<string, int>();
        }

        public async Task<List<TelemetryData>> GetTelemetryDataInCurrentShiftOfWorkStationAsync(Guid wsId, DateTime currentTime)
        {
            var shiftStart = GetCurrentShiftStart(DateTime.UtcNow);
            var shiftEnd = shiftStart.AddHours(8);
            string query = $@"
                from(bucket: ""CellTracker"")
                  |> range(start: {shiftStart.AddYears(-1):yyyy-MM-ddTHH:mm:ssZ}, stop: {shiftEnd.AddYears(1):yyyy-MM-ddTHH:mm:ssZ})
                  |> filter(fn: (r) => r._measurement == ""Telemetry"")
                  |> pivot(rowKey: [""_time""], columnKey: [""_field""], valueColumn: ""_value"")
                  |> filter(fn: (r) => r.WorkStationId == ""{wsId}"")
                ";


            var result = await _influxDBClient.GetQueryApi().QueryAsync<TelemetryData>(query, Environment.GetEnvironmentVariable("INFLUXDB_ORG"));

            var telemetryData = new List<TelemetryData>();

            foreach (var record in result)
            {
                telemetryData.Add(new TelemetryData
                {
                    Id = record.Id,
                    WorkStationId = record.WorkStationId,
                    OperatorId = record.OperatorId,
                    IsCompleted = record.IsCompleted,
                    Error = record.Error,
                    TimeStamp = record.TimeStamp
                });
            }

            return telemetryData;
        }

        public DateTime GetCurrentShiftStart(DateTime currentTime)
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
