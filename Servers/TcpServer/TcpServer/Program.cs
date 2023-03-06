using System.Net;
using System.Net.Sockets;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;
TcpListener tcpServer = new TcpListener(IPAddress.Parse("127.0.0.1"), 8000);
tcpServer.Start();
Console.WriteLine("Ожидание подключения...");
TcpClient tcpClient = tcpServer.AcceptTcpClient();
Console.WriteLine("Подключение установлено, входное сообщение:");
byte[] buffer = new byte[8];
NetworkStream streamTcp = tcpClient.GetStream();
streamTcp.Read(buffer, 0, buffer.Length);
Console.WriteLine(Encoding.UTF8.GetString(buffer));
streamTcp.Close();
tcpClient.Close();
tcpServer.Stop();
Console.WriteLine("Нажмите любую клавишу для завершения...");
Console.ReadKey();

