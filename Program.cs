using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigNBuildServer
{
     class Program
     {
          static void Main(string[] args)
          {
               Core.Run(HTTP(args), VMS(args), FL(args), IP(args), Port(args));
          }

          static bool HTTP(string[] args)
          {
               foreach (string s in args)
                    if (s.ToUpper() == "-HTTP")
                         return true;
               return false;
          }

          static bool VMS(string[] args)
          {
               foreach (string s in args)
                    if (s.ToUpper() == "-VMS")
                         return true;
               return false;
          }

          static bool FL(string[] args)
          {
               foreach (string s in args)
                    if (s.ToUpper() == "-FL")
                         return true;
               return false;
          }

          static string IP(string[] args)
          {
               bool next = false;
               foreach (string s in args)
                    if (s.ToUpper() == "-IP")
                         next = true;
                    else if (next)
                         return s;
               return "192.168.1.158";
          }

          static int Port(string[] args)
          {
               bool next = false;
               foreach (string s in args)
                    if (s.ToUpper() == "-PORT")
                         next = true;
                    else if (next)
                         return int.Parse(s);
               return 2586;
          }
     }
}
