namespace CellTracker.Api.Configuration.ExternalConnection
{
    public static class ConnectionConfiguration
    {
        public static string GetConnectionString()
        {
            var host = Environment.GetEnvironmentVariable("DB_HOST");
            var dbName = Environment.GetEnvironmentVariable("DB_NAME");
            var dbUser = Environment.GetEnvironmentVariable("DB_USER");
            var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");

            return $"Host={host};Port=5432;Database={dbName};Username={dbUser};Password={dbPassword};TrustServerCertificate=true;";
            //return $"Host=localhost;Port=5432;Database=Tracker;Username=test;Password=test;TrustServerCertificate=true;";
        }

        public static string GetInfluxDbConnectionString()
        {
            var influxDbToken = Environment.GetEnvironmentVariable("INFLUXDB_TOKEN");
            var influxDbUrl = Environment.GetEnvironmentVariable("INFLUXDB_URL");
            var influxDbOrg = Environment.GetEnvironmentVariable("INFLUXDB_ORG");
            var influxDbBucket = Environment.GetEnvironmentVariable("INFLUXDB_BUCKET");
            return $"{influxDbUrl}?token={influxDbToken}&org={influxDbOrg}&bucket={influxDbBucket}";
            //return $"http://localhost:8086?token=my-token?org=my-org?bucket=my-bucket";
        }
    }
}
