using System;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class SocketConnection : MonoBehaviour
{
    private TcpClient client;
    private NetworkStream stream;

    private void Start()
    {
        // Connect to the server
        client = new TcpClient("localhost", 1234);
        stream = client.GetStream();

        // Start a background thread to receive messages
        var receiveThread = new System.Threading.Thread(ReceiveMessages);
        receiveThread.Start();
    }

    private void ReceiveMessages()
    {
        var buffer = new byte[1024];
        while (true)
        {
            // Read data from the server
            var bytesRead = stream.Read(buffer, 0, buffer.Length);
            var jsonString = Encoding.ASCII.GetString(buffer, 0, bytesRead);

            // Deserialize the received JSON message
            var message = JsonUtility.FromJson<Message>(jsonString);

            // Process the received message (e.g., update Unity objects)
            Debug.Log("Received: " + message.content);
        }
    }

    private void SendMessageToServer(Message message)
    {
        // Convert the message to JSON
        var jsonString = JsonUtility.ToJson(message);

        // Convert the JSON string to bytes
        var bytes = Encoding.ASCII.GetBytes(jsonString);

        // Send the message to the server
        stream.Write(bytes, 0, bytes.Length);
    }

    private void OnDestroy()
    {
        // Clean up the resources
        stream.Close();
        client.Close();
    }

    // Example usage to send a message to the server
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var message = new Message
            {
                content = "Hello from Unity!",
                timestamp = DateTime.Now.ToString()
            };
            SendMessageToServer(message);
        }
    }
}

// Example message class
[Serializable]
public class Message
{
    public string content;
    public string timestamp;
}
