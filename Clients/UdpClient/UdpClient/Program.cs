using System.Net;
using System.Net.Sockets;

/// <summary>
/// Implementation of reaquired task using UDP.
/// </summary>
static void Task7Udp()
{
    // IP endpoint of the server.
    IPEndPoint ipEndpoint = new(IPAddress.Parse("192.168.0.204"), 8000);

    // Creating UDP client.
    UdpClient udpClient = new();

    // Connecting to the server using its IP address and port.
    udpClient.Connect(ipEndpoint);

    // Sending 'Hi' message to server.
    udpClient.Send(Array.Empty<byte>(), 0);

    // Receiving unordered array from the server.
    UdpReceiveResult serverResponse = udpClient.ReceiveAsync().GetAwaiter().GetResult();
    Console.WriteLine($"Received array from server: {string.Join(", ", serverResponse.Buffer)}");

    // Sorting the received array.
    byte[] result = serverResponse.Buffer.OrderBy(x => x).ToArray();
    Console.WriteLine($"Sending array to server: {string.Join(", ", result)}");

    // Sending sorted array to the server.
    udpClient.Send(result, result.Length);

    Console.WriteLine("Done. Press any key to finish...");
}

/// <summary>
/// UDP server load testing.
/// </summary>
static void UdpServerTesting()
{
    int i = 0;
    Console.WriteLine("Testing started");
    try
    {
        // IP endpoint of the server.
        IPEndPoint ipEndpoint = new(IPAddress.Parse("192.168.0.204"), 8000);
        List<UdpClient> udpClients = new ();
        for (i = 0; i < 65538; i++)
        {
            // Creating UDP client.
            UdpClient udpClient = new();

            // Connecting to the server using its IP address and port.
            udpClient.Connect(ipEndpoint);

            // Sending 'Hi' message to server.
            udpClient.Send(Array.Empty<byte>(), 0);

            // Receiving unordered array from the server.
            UdpReceiveResult serverResponse = udpClient.ReceiveAsync().GetAwaiter().GetResult();

            // Sorting the received array.
            byte[] result = serverResponse.Buffer.OrderBy(x => x).ToArray();

            // Sending sorted array to the server.
            udpClient.Send(result, result.Length);

            // Adding client to array to make sure it is not disposed.
            udpClients.Add(udpClient);
        }

        Console.WriteLine("Testing finished");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Failed to add {i} connection");
        Console.WriteLine(ex.ToString());
    }
}

Task7Udp();
//UdpServerTesting();
Console.ReadKey();