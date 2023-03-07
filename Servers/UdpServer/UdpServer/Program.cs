using System.Net;
using System.Net.Sockets;
using System.Text;

byte[] buffer = new byte[8];
IPEndPoint ipEndpoint = new (IPAddress.Any, 8000);
Socket serverSock = new (AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
serverSock.Bind(ipEndpoint);
Console.WriteLine("Waitinf for UDP protocol connection ...");
serverSock.Receive(buffer);

Console.WriteLine("Sending data UDP:");
Console.WriteLine(Encoding.UTF8.GetString(buffer));
serverSock.Close();
Console.WriteLine("Press any key to finish...");
Console.ReadKey();