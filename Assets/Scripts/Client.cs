using UnityEngine;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;


public class TCPClient : MonoBehaviour
{
    string serverHost = "127.0.0.1";
    int serverPort = 65432;
    public PlayerControl playerControl;
    private int weaponIndex = 1;

    TcpClient client;
    NetworkStream stream;

    async void Start()
    {
        client = new TcpClient();
        try
        {
            await client.ConnectAsync(serverHost, serverPort); // Connect to the server
            Debug.Log($"Connected to {serverHost}:{serverPort}");
            stream = client.GetStream();

            ReceiveMessages();
        }
        catch (SocketException e)
        {
            Debug.LogError($"SocketException: {e.Message}");
        }
    }

    async void ReceiveMessages()
    {
        byte[] buffer = new byte[1024];
        int numberOfBytesRead;

        while (client.Connected)
        {
            numberOfBytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
            if (numberOfBytesRead <= 0)
            {
                Debug.Log("Server closed the connection.");
                break;
            }

            string receivedMessage = Encoding.ASCII.GetString(buffer, 0, numberOfBytesRead);
            Debug.Log($"Received from server: {receivedMessage}");
            if (receivedMessage == "1")
            {
                playerControl.activeWeaponSwitching.SetSelectedWeapon(weaponIndex);
                weaponIndex= (weaponIndex+1)%3;
            }
            
        }
    }

    void OnApplicationQuit()
    {
        stream.Close();
        client.Close();
    }
}
