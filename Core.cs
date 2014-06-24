using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigNBuildServer
{
     static class Core
     {
          public static string IPAddress { get; private set; }

          public static void Run(bool HTTP, bool VMS, bool FileLogging, string IP, int port)
          {
               if (FileLogging)
                    Log.FileLogging = true;
               Log.LogLine(Log.LogLevel.Debug, "Starting server at " + IP + ":" + port.ToString() + "...");
               IPAddress = IP;
               ClientHeandler.OpenPort(port);
               System.Threading.Thread.Sleep(3000);
               ClientHeandler.ClosePort();
          }
     }
}
