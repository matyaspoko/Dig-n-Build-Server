using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigNBuildServer.CDMs
{
     class BannedIP : ClientDisconnectMessage
     {
          public BannedIP()
               : base("Your IP is banned!", "This server have your IP on server black list!")
          {
 
          }
     }
}
