using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DigNBuildServer
{
     static class Log
     {
          public enum LogLevel { Info, Debug, Error, Warning }
          public static bool FileLogging = false;
          static string logPath = "";

          public static void LogLine(LogLevel ll,string message)
          {
               System.Diagnostics.Debug.WriteLine("[ {0} ] {1}",ll.ToString(),message);
               Console.WriteLine("[ {0} ] {1}", ll.ToString(), message);
               if (FileLogging)
               {
                    if (!Directory.Exists("Logs"))
                         Directory.CreateDirectory("Logs");
                    if (string.IsNullOrEmpty(logPath))
                         logPath = "Logs\\" + DateTime.Now.ToString("dd-MM-yyyy_H-mm-ss") + ".Log";
                    StreamWriter sw = File.CreateText(logPath);
                    sw.WriteLine("[ {0} ] {1}", ll.ToString(), message);
                    sw.Flush();
                    sw.Close();
               }
          }

          public static void Error(Exception ex)
          {
               if (FileLogging)
                    LogLine(LogLevel.Error, "Server crushed! :( More info in log file...");
               else
                    LogLine(LogLevel.Error, "Server crushed! :(");

               if (string.IsNullOrEmpty(logPath))
                    logPath = "Logs\\" + DateTime.Now.ToString("dd-MM-yyyy_H-mm-ss") + ".Log";
               StreamWriter sw = File.CreateText(logPath);
               sw.WriteLine(ex.ToString());
               sw.Flush();
               sw.Close();
               Console.WriteLine("Press any key...");
               Console.ReadKey();
               Environment.Exit(0);
          }
     }
}
