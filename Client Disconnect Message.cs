using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigNBuildServer
{
     class ClientDisconnectMessage
     {
          public string Title { get; private set; }
          public string Reason { get; private set; }
          public string Solution { get; private set; }

          public ClientDisconnectMessage(string title, string reason, string solution = "No Solution Known")
          {
               Title = title;
               Reason = reason;
               Solution = solution;
          }
     }
}
