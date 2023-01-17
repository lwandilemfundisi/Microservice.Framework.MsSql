using Microservice.Framework.Common;
using Microservice.Framework.Sql.Connections;

namespace Microservice.Framework.MsSql.Configurations
{
    public interface IMsSqlConfiguration : ISqlConfiguration<IMsSqlConfiguration>
    {
        RepeatDelay ServerBusyRepeatDelay { get; }

        IMsSqlConfiguration SetServerBusyRepeatDelay(RepeatDelay repeatDelay);
    }
}
