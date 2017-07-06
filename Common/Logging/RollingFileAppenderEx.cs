using System;
using System.Collections.Generic;
using System.IO;
using Gdot.Care.Common.Exceptions;
using Gdot.Care.Common.Extension;
using Gdot.Care.Common.Utilities;
using log4net.Appender;
using log4net.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Diagnostics.CodeAnalysis;

namespace Gdot.Care.Common.Logging
{
    [ExcludeFromCodeCoverage]
    public class RollingFileAppenderEx: RollingFileAppender
    {
        private static readonly object Obj = new object();
        protected static readonly dynamic Config = ConfigManager.Instance.Configuration;
        protected static readonly JsonSerializerSettings JsonSettings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            DateFormatHandling = DateFormatHandling.IsoDateFormat,
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            Converters = new List<JsonConverter> { new Newtonsoft.Json.Converters.StringEnumConverter()}
        };
        protected override void Append(LoggingEvent loggingEvent)
        {
            try
            {
                if (FilterEvent(loggingEvent))
                {
                    loggingEvent.Fix = FixFlags.All;
                    var logObject = loggingEvent.MessageObject as LogObject;
                    if (logObject == null)
                    {
                        throw new ArgumentException($"Please use LogObject when write to the log - MessageObject='{JsonConvert.SerializeObject(loggingEvent.MessageObject)}'");
                    }
                    var logdata = loggingEvent.GetLoggingEventData();
                    logdata.Message = GetLogData(loggingEvent);
                    loggingEvent = new LoggingEvent(loggingEvent.GetType(), loggingEvent.Repository, logdata.LoggerName,
                        logdata.Level, logdata.Message, null) {Fix = FixFlags.All};
                    base.Append(loggingEvent);
                }
            }
            catch (Exception exception)
            {
                lock (Obj)
                {
                    string path = Config.Log4Net.ErrorPath;
                    var folder = Path.GetDirectoryName(path);
                    if (folder != null && !Directory.Exists(folder))
                    {
                        Directory.CreateDirectory(folder);
                    }
                    System.IO.File.AppendAllText(path, exception.ToString());
                }
            }
        }

        protected string GetLogData(LoggingEvent logEvent)
        {
            IDictionary<string, object> logData = new Dictionary<string, object>();
            var headers = Log.LogProperty.Value;
            logData.Add("EventLevel", logEvent.Level.ToString());
            logData.Add("Timestamp", logEvent.TimeStamp);
            logData.Add("ThreadId", logEvent.ThreadName);
            logData.Add("HostName", Environment.MachineName);
            if (logEvent.LocationInformation != null)
            {
                var sf = logEvent.LocationInformation.StackFrames[1];
                logData.Add("MethodInfo", sf.ClassName + "." + sf.Method.Name);
            }
            if (headers != null)
            {
                logData.Merge(headers);
            }
            var logObject = logEvent.MessageObject as LogObject;
            if (logObject != null)
            {
                var data = logObject.Data as Dictionary<string, object>;
                if (data != null)
                {
                    if (data.ContainsKey("DurationInMs"))
                    {
                        logData.Add("DurationInMs", data["DurationInMs"]);
                        data.Remove("DurationInMs");
                    }
                }

                logData.Add("EventType", logObject.EventType);
                logData.Add("Data", logObject.Data);
            }
            else
            {
                logData.Add("Data", logEvent.MessageObject);
            }
            if (logEvent.ExceptionObject != null)
            {
                logData.Add("Exception", logEvent.ExceptionObject);
            }
            var logString = JsonConvert.SerializeObject(logData, JsonSettings);
            return logString;
        }

    }
}
