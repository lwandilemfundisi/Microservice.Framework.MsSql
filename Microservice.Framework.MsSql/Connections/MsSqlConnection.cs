using Microservice.Framework.Common;
using Microservice.Framework.MsSql.Configurations;
using Microservice.Framework.MsSql.Integrations;
using Microservice.Framework.MsSql.RelienceStrategies;
using Microservice.Framework.Sql.Connections;
using Microsoft.Extensions.Logging;

namespace Microservice.Framework.MsSql.Connections
{
    public class MsSqlConnection 
        : SqlConnection<IMsSqlConfiguration, IMsSqlErrorResilientStrategy, IMsSqlConnectionFactory>, IMsSqlConnection
    {
        public MsSqlConnection(
            ILogger<MsSqlConnection> logger,
            IMsSqlConfiguration configuration,
            IMsSqlConnectionFactory connectionFactory,
            ITransientFaultHandler<IMsSqlErrorResilientStrategy> transientFaultHandler)
            : base(logger, configuration, connectionFactory, transientFaultHandler)
        {
        }

        public override Task<IReadOnlyCollection<TResult>> InsertMultipleAsync<TResult, TRow>(
            Label label,
            string connectionStringName,
            CancellationToken cancellationToken,
            string sql,
            IEnumerable<TRow> rows)
        {
            Logger.LogTrace(
                "Using optimized table type to insert with SQL: {Sql}",
                sql);
            var tableParameter = new TableParameter<TRow>("@rows", rows, new { });
            return QueryAsync<TResult>(label, connectionStringName, cancellationToken, sql, tableParameter);
        }
    }
}
