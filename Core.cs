using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

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
               BlackList.LoadList();

          }

          public static ClientDisconnectMessage isClientAcceptable(string name, string ip)
          {
               if (BlackList.isOnList(name))
                    return new CDMs.BannedIP();
               if (BlackList.isOnList(System.Net.IPAddress.Parse(ip)))
                    return new CDMs.BannedIP();
               return null;
          }
     }
}
