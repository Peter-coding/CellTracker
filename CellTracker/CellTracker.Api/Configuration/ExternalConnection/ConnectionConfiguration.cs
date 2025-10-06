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
    }
}
