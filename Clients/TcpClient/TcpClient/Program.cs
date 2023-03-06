using System.Net;
using System.Net.Sockets;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;
TcpClient clientTcp = new TcpClient();
clientTcp.Connect(IPAddress.Parse("127.0.0.1"), 8000);
NetworkStream streamTcp = clientTcp.GetStream();
byte[] buffer = new byte[8];
buffer = Encoding.UTF8.GetBytes("Hello");
streamTcp.Write(buffer, 0, buffer.Length);
streamTcp.Close();
clientTcp.Close();
Console.WriteLine("Передача выполнена. Нажмите любую клавишу для завершения...");
Console.ReadKey();