using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DigNBuildServer
{
     class Responce
     {
          public enum ResponceType : int { False = 0, True = 1, Disconnect = 2}
          private byte[] bytes = new byte[Constants.MaxPaketSize];
          private Socket client;

          public Responce(ResponceType rt,Socket skt, ClientDisconnectMessage cdm = null)
          {
               bytes[0] = (byte)rt;
               client = skt;
               if (rt == ResponceType.Disconnect)
               {
                    byte[] textBytes = Encoding.UTF8.GetBytes(cdm.Title + "#" + cdm.Reason + "#" + cdm.Solution);
                    for (int i = 0; i < textBytes.Length; i++)
                         bytes[i + 1] = textBytes[i];
               }
               
          }

          public void Send()
          {
               client.Send(bytes);
          }
     }
}
