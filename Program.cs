using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace EventLogViewer
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("\n 1.Write Information Log \n 2.Write Warning Log \n 3.Write Error Log \n 4.Read Logs Entries \n 5.Clear Log Data \n 6.Delete Log");

            //Console.Write("\n\n Enter your options : ");

           // bool isConverted = byte.TryParse(Console.ReadLine(), out byte optionValue);
            bool isConverted = byte.TryParse("1", out byte optionValue);

            if (!isConverted || !(optionValue >= 1 && optionValue <= 6))
            {
                Console.WriteLine("\n\nPlease enter valid option here.");
                return;
            }

            Task.WaitAll(WriteLogData(optionValue));
            //Console.Read();
        }

        private static async Task WriteLogData(byte optionValue)
        {
            WriteLogs writeLogs = new WriteLogs();
            Console.Write("\n\n Enter your message : ");
            //string message = Console.ReadLine();
            string message = "Log data in self.";
            writeLogs.message = message;
            switch (optionValue)
            {
                case 1:
                    await writeLogs.WriteInformationLog();
                    break;
                case 2:
                    await writeLogs.WriteWarningLog();
                    break;
                case 3:
                    await writeLogs.WriteErrorLog();
                    break;
                case 4:
                    await writeLogs.ReadTheLogEntries();
                    break;
                case 5:
                    await writeLogs.ClearLogInformation();
                    break;
                case 6:
                    await writeLogs.DeleteEventSource();
                    break;
            }
        }
    }
}
