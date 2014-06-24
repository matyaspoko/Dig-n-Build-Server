using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace DigNBuildServer
{
     static class ClientHeandler
     {
          public static int Port { get; private set; }
          static bool PortOpened = false;
          static TcpListener listener;

          public static void StartAccepting()
          {

          }

          public static void OpenPort(int port = -1)
          {
               if (port == -1)
                    port = Port;

               Log.LogLine(Log.LogLevel.Debug, "Opening port " + port + "...");
               if (listener != null)
                    listener.Stop();
               try
               {
                    listener = new TcpListener(IPAddress.Parse(Core.IPAddress), port);
               }
               catch (Exception ex)
               {
                    Log.Error(ex);
               }
               PortOpened = true;
               Log.LogLine(Log.LogLevel.Debug, "Port is open!");
          }

          public static void ClosePort()
          {
               Log.LogLine(Log.LogLevel.Warning, "Closing port...");
               if (listener != null)
                    listener.Stop();
               PortOpened = false;
               Log.LogLine(Log.LogLevel.Warning, "Port is closed...");
          }
     }
}
