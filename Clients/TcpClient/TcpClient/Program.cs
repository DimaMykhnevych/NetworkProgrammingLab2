using System.Net;
using System.Net.Sockets;

/// <summary>
/// Implementation of reaquired task using TCP.
/// </summary>
static void Task7()
{
    Console.WriteLine("Connecting to server");

    // Creating new TcpClient.
    TcpClient clientTcp = new();

    // Connecting to the server using its IP address and port.
    clientTcp.Connect(IPAddress.Parse("192.168.0.204"), 8000);

    // Getting network stream.
    NetworkStream streamTcp = clientTcp.GetStream();

    // Array to store server response.
    byte[] serverResponse = new byte[10];

    // Waiting for the server to send the array of numbers.
    streamTcp.Read(serverResponse, 0, 10);

    Console.WriteLine($"Received array from server: {string.Join(", ", serverResponse)}");

    // Sorting the received array.
    byte[] result = serverResponse.OrderBy(x => x).ToArray();

    Console.WriteLine($"Sending array to server: {string.Join(", ", result)}");

    // Sending sorted array back to the server.
    streamTcp.Write(result, 0, result.Length);

    // Closing network stream.
    streamTcp.Close();

    // Closing connection with server.
    clientTcp.Close();
    Console.WriteLine("Done. Press any key to finish...");
    Console.ReadKey();
}

/// <summary>
/// TCP server load testing.
/// </summary>
static void TcpServerTesting()
{
    // List to store clients.
    List<TcpClient> clients = new();

    int i = 0;
    Console.WriteLine("Testing started");
    try
    {
        for (i = 0; i < 65538; i++)
        {
            // Creating new TcpClient.
            TcpClient clientTcp = new();

            // Connecting to the server using its IP address and port.
            clientTcp.Connect(IPAddress.Parse("192.168.0.204"), 8000);

            // Getting network stream.
            NetworkStream streamTcp = clientTcp.GetStream();

            // Array to store server response.
            byte[] serverResponse = new byte[10];

            // Waiting for the server to send the array of numbers.
            streamTcp.Read(serverResponse, 0, 10);

            // Sorting the received array.
            byte[] result = serverResponse.OrderBy(x => x).ToArray();

            // Sending sorted array back to the server.
            streamTcp.Write(result, 0, result.Length);

            // Closing network stream.
            streamTcp.Close();

            // Adding client to array to make sure it is not disposed.
            clients.Add(clientTcp);
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Failed to add {i} connection");
        Console.WriteLine(ex.ToString());
    }
}

Task7();
//TcpServerTesting();