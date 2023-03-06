using System.Net;
using System.Net.Sockets;

// Array that will be sorted by client.
byte[] byteArrayToSort = new byte[10] {28, 11, 23, 1, 100, 123, 88,  10, 99, 8};

// Creating TCP listener on port 8000 using Linux vm address.
TcpListener tcpServer = new TcpListener(IPAddress.Parse("192.168.0.204"), 8000);

// Starting listening incoming requests.
tcpServer.Start();
Console.WriteLine("Waiting for connections...");

// Creating separate thread to serve multiple clients.
Thread serverListenerThread = new (() =>
{
    // List to store clients.
    List<TcpClient> clients = new();
    while(true)
    {
        // Accepting incoming request.
        TcpClient tcpClient = tcpServer.AcceptTcpClient();
        Console.WriteLine("Client connected");

        // Getting network stream.
        NetworkStream streamTcp = tcpClient.GetStream();

        Console.WriteLine($"Sending array to client: {string.Join(", ", byteArrayToSort)}");

        // Sending array to sort to client.
        streamTcp.Write(byteArrayToSort, 0, byteArrayToSort.Length);

        // Array to store client response.
        byte[] clientResponse = new byte[byteArrayToSort.Length];

        // Reading response from client.
        streamTcp.Read(clientResponse, 0, clientResponse.Length);

        Console.WriteLine($"Received array from client: {string.Join(", ", clientResponse)}");

        // Closing network stream.
        streamTcp.Close();

        // UNCOMMENT for maximum allowed connections testing.
        // Adding client to array to make sure it is not disposed.
        // clients.Add(tcpClient);

        // COMMENT for maximum allowed connections testing.
        // Closing connection with client.
        tcpClient.Close();
    }
});

// Starting the thread.
serverListenerThread.Start();

// Waiting the thread to finish for 20 minutes
serverListenerThread.Join(1200000);

// Stopping the server
tcpServer.Stop();

