using System.Net;
using System.Net.Sockets;

// Array that will be sorted by client.
byte[] byteArrayToSort = new byte[10] {28, 11, 23, 1, 100, 123, 88,  10, 99, 8};

// Creating UDP listener on port 8000.
// Listening for client activity on all network interfaces.
IPEndPoint ipEndpoint = new (IPAddress.Any, 8000);
UdpClient udpServer = new(ipEndpoint);

Console.WriteLine("Waiting for connections...");

// Creating separate thread to serve multiple clients.
Thread serverListenerThread = new(() => 
{
    while(true)
    {
        // Receiving 'Hi' message from client.
        UdpReceiveResult clientMessage = udpServer.ReceiveAsync().GetAwaiter().GetResult();
        Console.WriteLine("UDP client connected");

        Console.WriteLine($"Sending array to client using UDP: {string.Join(", ", byteArrayToSort)}");

        // Sending to the client array to sort using client RemoteEndPoint.
        udpServer.Send(byteArrayToSort, byteArrayToSort.Length, clientMessage.RemoteEndPoint);

        // Receiving message from client with sorted array.
        clientMessage = udpServer.ReceiveAsync().GetAwaiter().GetResult();
        
        // Client response.
        byte[] clientResponse = clientMessage.Buffer;

        Console.WriteLine($"Received array from client using UDP: {string.Join(", ", clientResponse)}");
    }
});

// Starting the thread.
serverListenerThread.Start();

// Waiting the thread to finish for 20 minutes
serverListenerThread.Join(1200000);

// Closing the UDP connection.
udpServer.Close();