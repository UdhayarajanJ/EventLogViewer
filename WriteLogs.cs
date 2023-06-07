using System;
using System.Threading.Tasks;
using System.Diagnostics;

namespace EventLogViewer
{
    public class WriteLogs
    {
        private EventLogUtilities _eventLogUtilities = null;
        private string _message = string.Empty;
        public string message
        {
            get => _message; 
            set => _message = value;
        }
        public WriteLogs() => _eventLogUtilities = new EventLogUtilities();

        public async Task WriteInformationLog()
        {
            if (await _eventLogUtilities.createEventLogAndSource())
                _eventLogUtilities.eventLog.WriteEntry($"{DateTime.Now} - {message}", EventLogEntryType.Information);
            disposeEventViewer();
        }

        public async Task WriteWarningLog()
        {
            if (await _eventLogUtilities.createEventLogAndSource())
                _eventLogUtilities.eventLog.WriteEntry($"{DateTime.Now} - {message}", EventLogEntryType.Warning);
            disposeEventViewer();
        }

        public async Task WriteErrorLog()
        {
            if (await _eventLogUtilities.createEventLogAndSource())
                _eventLogUtilities.eventLog.WriteEntry($"{DateTime.Now} - {message}", EventLogEntryType.Error);
            disposeEventViewer();
        }

        public async Task ReadTheLogEntries()
        {
            if (await _eventLogUtilities.createEventLogAndSource())
                foreach (EventLogEntry item in _eventLogUtilities.eventLog.Entries)
                    Console.WriteLine($"{item.Message}    -    {item.Source}    -    {item.InstanceId}");
            disposeEventViewer();
        }

        public async Task ClearLogInformation()
        {
            if (await _eventLogUtilities.createEventLogAndSource())
                _eventLogUtilities.eventLog.Clear();
            disposeEventViewer();
        }
        public async Task DeleteEventSource() => await _eventLogUtilities.DeleteEventSource();
        private void disposeEventViewer() => _eventLogUtilities.eventLog.Close();
    }
}
