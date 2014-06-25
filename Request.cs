using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DigNBuildServer
{
     class Request
     {
          public enum RequestType : int { UserNameInfo = 1, KeyPress = 2 , Chat = 3, ModInfo = 4 }
          public RequestType myType = RequestType.UserNameInfo;
          public string text = "";
          public string mod = "";
          public Version modVer = new Version();
          public Keys key;

          public Request(byte[] bytes)
          {
               myType = (RequestType)BitConverter.ToInt32(new byte[1] { bytes[0] }, 0);

               switch (myType)
               {
                    case RequestType.UserNameInfo:
                         text = BitConverter.ToString(bytes, 1);
                         break;
                    case RequestType.KeyPress:
                         KeysConverter kc = new KeysConverter();
                         key = (Keys)BitConverter.ToInt32(bytes, 1);
                         break;
                    case RequestType.Chat:
                         text = BitConverter.ToString(bytes, 1);
                         break;
                    case RequestType.ModInfo:
                         modVer = new Version(BitConverter.ToInt16(bytes, 1), BitConverter.ToInt16(bytes, 3));
                         mod = BitConverter.ToString(bytes, 5);
                         break;
                    default:
                         break;
               }
          }
     }
}
