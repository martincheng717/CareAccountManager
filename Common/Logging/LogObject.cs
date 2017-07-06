using System;
using System.Diagnostics.CodeAnalysis;

namespace Gdot.Care.Common.Logging
{
    [ExcludeFromCodeCoverage]
    [Serializable]
    public class LogObject
    {
        public string EventType { get; set; }
        public object Data { get; set; }

        public LogObject(string eventType, object data)
        {
            EventType = eventType;
            Data = data;
        }
    }
    
}
