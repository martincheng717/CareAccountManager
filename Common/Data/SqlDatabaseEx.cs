using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Gdot.Care.Common.Enum;
using Gdot.Care.Common.Interface;
using Gdot.Care.Common.Model;
using Gdot.Care.Common.Utilities;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Gdot.Care.Common.Extension;
using Gdot.Care.Common.Logging;

namespace Gdot.Care.Common.Data
{
    [ExcludeFromCodeCoverage]
    public class SqlDatabaseEx: SqlDatabase, ISqlDatabaseEx, IDisposable
    {
        private int? _commandTimeout;
        private readonly string _commandText;
        private readonly CommandTypeEnum _commandType;
        private DbCommand _cmd;
        private static readonly ILogger Log = Logging.Log.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// ConnectionString is default to ConfigManager.ConnectionString
        /// </summary>
        /// <param name="commandText">commandText is required field</param>
        /// <param name="commandType">commandType is default to CommandTypeEnum.SqlStoredProcedureCommand</param>
        public SqlDatabaseEx(string commandText, CommandTypeEnum commandType = CommandTypeEnum.SqlStoredProcedureCommand) : base(ConfigManager.Instance.DefaultConnection)
        {
            _commandText = commandText;
            _commandType = commandType;
        }
        /// <summary>
        /// connectionString, commandText, commandType are required.
        /// </summary>
        /// <param name="connectionName"></param>
        /// <param name="commandText"></param>
        /// <param name="commandType"></param>
        /// <param name="commandTimeout">commandTimeout is default to 30 when not passed in</param>
        public SqlDatabaseEx(string connectionName, string commandText, CommandTypeEnum commandType = CommandTypeEnum.SqlStoredProcedureCommand, int? commandTimeout = 30) : base(ConfigManager.Instance.GetConnectionString(connectionName))
        {
            _commandTimeout = commandTimeout;
            _commandText = commandText;
            _commandType = commandType;            
        }

        public DbCommand Command
        {
            get
            {
                if (_cmd == null)
                {
                    _cmd = _commandType == CommandTypeEnum.SqlStoredProcedureCommand
                        ? GetStoredProcCommand(_commandText)
                        : GetSqlStringCommand(_commandText);
                    if (_commandTimeout.HasValue)
                    {
                        _cmd.CommandTimeout = _commandTimeout.Value;
                    }
                    _cmd.Connection = new SqlConnection(ConnectionString);
                    _cmd.Connection.Open();
                }
                return _cmd;
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logData">additional logging to metricwatcher</param>
        /// <returns></returns>
        public async Task<DbDataReader> ExecuteReaderAsync(IDictionary<string, object> logData = null)
        {
            var isError = false;

            var metric = new MetricWatcher(Constants.MetricDataClient,
                            new MetricWatcherOption
                            {
                                ManualStartStop = true,
                                LoggingOption = LogOptionEnum.FullLog,
                                LogMessage = BuildLogMessage(logData)
                            });
            try
            {
                metric.Start();
                return await Command.ExecuteReaderAsync();
            }
            catch (Exception ex)
            {
                isError = true;               
                Log.Error(new LogObject(CommonEventType.SqlDatabaseEx.GetDescription(), BuildLogMessage(logData)), ex);
                throw;
            }
            finally
            {
                metric.Stop(isError);
            }
        }
        
        public async Task<int> ExecuteNonQueryAsync(IDictionary<string, object> logData = null)
        {
            var isError = false;
            var metric = new MetricWatcher(Constants.MetricDataClient,
                            new MetricWatcherOption
                            {
                                ManualStartStop = true,
                                LoggingOption = LogOptionEnum.FullLog,
                                LogMessage = BuildLogMessage(logData)
                            });
            try
            {
                metric.Start();
                return await Command.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                isError = true;
                Log.Error(new LogObject(CommonEventType.SqlDatabaseEx.GetDescription(), BuildLogMessage(logData)), ex);
                throw;
            }
            finally
            {
                metric.Stop(isError);
            }
        }

        private IDictionary<string, object> BuildLogMessage(IDictionary<string, object> logData)
        {
            var logMessage = new Dictionary<string, object>
            {
                {"DatabaseName", Command.Connection.Database},
                {"CommandText", _commandText}
            };
            if (logData != null)
            {
                logMessage = (Dictionary<string, object>) logMessage.Merge(logData);
            }
            return logMessage;
        }

        public void Dispose()
        {
            _cmd.Connection?.Close();
        }
    }
}
