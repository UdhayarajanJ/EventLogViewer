using System.Configuration;
using System.Threading.Tasks;
using System.Diagnostics;
namespace EventLogViewer
{
    public class EventLogUtilities
    {
        private readonly string _eventLogName = string.Empty;
        private readonly string _eventSoruceName = string.Empty;
        private EventLog _eventLog = null;

        public EventLog eventLog
        {
            get
            {
                if (_eventLog is null)
                {
                    _eventLog = new EventLog();
                    _eventLog.Source = _eventSoruceName;
                    _eventLog.Log = _eventLogName;
                }
                return _eventLog;
            }
        }

        public EventLogUtilities()
        {
            _eventLogName = ConfigurationManager.AppSettings["EventLog"].ToString();
            _eventSoruceName = ConfigurationManager.AppSettings["EventSource"].ToString();
        }

        private bool isExistsEventLog_And_Source() => EventLog.SourceExists(_eventSoruceName) && EventLog.Exists(_eventLogName);
        private bool isConfigurationDefined() => !string.IsNullOrEmpty(_eventSoruceName) && !string.IsNullOrEmpty(_eventLogName);

        public Task<bool> createEventLogAndSource()
        {
            bool result = false;
            if (!isExistsEventLog_And_Source() && isConfigurationDefined())
            {
                EventLog.CreateEventSource(new EventSourceCreationData(_eventSoruceName, _eventLogName));
                result = true;
            }
            else
                result = true;
            return Task.FromResult(result);          
        }


        public async Task DeleteEventSource()
        {
            if (await createEventLogAndSource())
                EventLog.Delete(_eventLogName);
        }

    }
}
