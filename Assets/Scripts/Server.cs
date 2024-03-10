using UnityEngine;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class UnityTCPServer : MonoBehaviour
{
    private Thread serverThread;
    private bool isRunning = true;

    void Start()
    {
        serverThread = new Thread(new ThreadStart(RunServer));
        serverThread.IsBackground = true;
        serverThread.Start();
    }

    void RunServer()
    {
        const int port = 65432;
        var localEndPoint = new IPEndPoint(IPAddress.Any, port);

        try
        {
            using (var listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                listener.Bind(localEndPoint);
                listener.Listen(10); // Set backlog to 10
                
                Debug.Log($"Server is running on port {port}. Waiting for a connection...");
                
                while (isRunning)
                {
                    if (listener.Poll(1000, SelectMode.SelectRead))
                    {
                        Socket handler = listener.Accept();
                        Debug.Log($"Connected by {handler.RemoteEndPoint.ToString()}");

                        // Example message
                        string message = "Hello from Unity TCP Server!";
                        byte[] msg = Encoding.ASCII.GetBytes(message);
                        handler.Send(msg);
                        
                        handler.Shutdown(SocketShutdown.Both);
                        handler.Close();
                    }
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e.ToString());
        }
    }

    void OnDisable()
    {
        isRunning = false;
        if (serverThread != null)
        {
            serverThread.Abort();
        }
    }
}
