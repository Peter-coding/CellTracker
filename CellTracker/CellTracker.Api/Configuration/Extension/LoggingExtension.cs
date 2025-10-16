using CellTracker.Api.Configuration.ExternalConnection;
using NpgsqlTypes;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.PostgreSQL;
using System.Data;

namespace CellTracker.Api.Configuration.Extension
{
    public static class LoggingExtension
    {
        public static IServiceCollection AddConfigureSerilog(
            this IServiceCollection services)
        {
            services.AddSerilog((services, lc) =>
            {
                lc.WriteTo.PostgreSQL(
                        connectionString: ConnectionConfiguration.GetConnectionString(),
                        tableName: "Logs",
                        schemaName: "cell_tracker",
                        needAutoCreateTable: true,
                        columnOptions: GetColumnOptions())
                    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                    .MinimumLevel.Information()
                    .Enrich.FromLogContext();
            });
            return services;
        }

        static IDictionary<string, ColumnWriterBase> GetColumnOptions()
        {
            return new Dictionary<string, ColumnWriterBase>
            {
                { "message", new RenderedMessageColumnWriter(NpgsqlDbType.Text) },
                { "message_template", new MessageTemplateColumnWriter(NpgsqlDbType.Text) },
                { "level", new LevelColumnWriter(true, NpgsqlDbType.Varchar) },
                { "raise_date", new TimestampColumnWriter(NpgsqlDbType.Timestamp) },
                { "exception", new ExceptionColumnWriter(NpgsqlDbType.Text) },
                { "properties", new LogEventSerializedColumnWriter(NpgsqlDbType.Jsonb) },
                { "props_test", new PropertiesColumnWriter(NpgsqlDbType.Jsonb) },
        
                // Custom columns
                { "UserName", new SinglePropertyColumnWriter("UserName", PropertyWriteMethod.ToString, NpgsqlDbType.Varchar, "l50") },
                { "Type", new SinglePropertyColumnWriter("Type", PropertyWriteMethod.ToString, NpgsqlDbType.Varchar, "l100") },
                { "Function", new SinglePropertyColumnWriter("Function", PropertyWriteMethod.ToString, NpgsqlDbType.Varchar, "l100") }
            };
        }
    }
}
