﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace DigNBuildServer
{
     static class ClientHeandler
     {
          public static int Port { get; private set; }
          static bool PortOpened = false;
          static TcpListener listener;
          static Task tcpAccepter = new Task(Accepter);
          static bool KillAccepter = false;
          static Dictionary<string, Socket> Clients = new Dictionary<string, Socket>();

          public static void StartAccepting()
          {
               if (!PortOpened)
                    OpenPort();
               if (tcpAccepter.Status != TaskStatus.Created)
                    tcpAccepter = new Task(Accepter);
               tcpAccepter.Start();
          }

          static void Accepter()
          {
               while (!KillAccepter)
               {
                    Socket client = listener.AcceptSocket();
                    Request rqst = Receive(client);
                    if (rqst.myType == Request.RequestType.UserNameInfo)
                    {
                         string name = rqst.text;
                         ClientDisconnectMessage cds = Core.isClientAcceptable(name, getSocketIP(client).ToString());
                         if (cds == null)
                         {
                              Log.LogLine(Log.LogLevel.Info, "Player " + name + " joined game!");
                              Clients.Add(name,client);
                         }
                         else
                              Disconnect(client, cds);
                    }
                    else
                    {
 
                    }
               }

               KillAccepter = false;
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

          public static void Disconnect(Socket skt, ClientDisconnectMessage cdm)
          {
               Responce resp = new Responce(Responce.ResponceType.Disconnect, skt, cdm);
               resp.Send();
               skt.Disconnect(false);
               skt.Close();
          }

          private static Request Receive(Socket skt)
          {
               byte[] buffer = new byte[Constants.MaxPaketSize];
               skt.Receive(buffer);
               return new Request(buffer);
          }

          private static IPAddress getSocketIP(Socket skt)
          {
               return ((IPEndPoint)skt.RemoteEndPoint).Address;
          }
     }

     static class BlackList
     {
          static Dictionary<string, IPAddress> list = new Dictionary<string, IPAddress>();

          const string BAN_LIST_FILE = "BlackList.txt";

          public static void LoadList()
          {
               Log.LogLine(Log.LogLevel.Debug, "Loading black list...");
               if (!File.Exists(BAN_LIST_FILE))
               {
                    InitList();
               }
               else
               {
                    foreach (string s in File.ReadAllLines(BAN_LIST_FILE))
                         if (!s.StartsWith("#"))
                         {
                              string[] split = s.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                              list.Add(split[1].Trim(),IPAddress.Parse(split[0].Trim()));
                         }
               }
               Log.LogLine(Log.LogLevel.Debug, "Black list loaded!");
          }

          public static void AddToList(string name, string IP)
          {
               list.Add(name,IPAddress.Parse(IP));
               SaveList();
               Log.LogLine(Log.LogLevel.Warning, name + " added to black list!");
          }

          public static void RemoveFromList(string name)
          {
               list.Remove(name);
               SaveList();
               Log.LogLine(Log.LogLevel.Warning, "Removed from black list!");
          }

          static void SaveList()
          {
               if (File.Exists(BAN_LIST_FILE))
                    File.Delete(BAN_LIST_FILE);
               InitList();
               StreamWriter sw = File.CreateText(BAN_LIST_FILE);
               foreach (KeyValuePair<string, IPAddress> kvp in list)
                    sw.WriteLine(kvp.Key + "|" + kvp.Value.ToString());
               sw.Flush();
               sw.Close();
          }

          static void InitList()
          {
               StreamWriter sw = File.CreateText(BAN_LIST_FILE);
               sw.WriteLine("# IP | NAME");
               sw.Flush();
               sw.Close();
          }

          public static bool isOnList(string name)
          {
               foreach (KeyValuePair<string, IPAddress> kvp in list)
                    if (kvp.Key == name)
                         return true;
               return false;
          }

          public static bool isOnList(IPAddress ip)
          {
               foreach (KeyValuePair<string, IPAddress> kvp in list)
                    if (kvp.Value.ToString() == ip.ToString())
                         return true;
               return false;
          }
     }
}
