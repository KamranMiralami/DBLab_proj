using Newtonsoft.Json;
using System;
using System.Net.Sockets;
using System.Text;
using TMPro;
using UnityEngine;

public class UnitySocketClient : MonoBehaviour
{
    private TcpClient client;
    private NetworkStream stream;
    public TextMeshProUGUI t;

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
            Debug.Log(jsonString);
            // Deserialize the received JSON message
            var message = JsonUtility.FromJson<Response>(jsonString);

            // Process the received message (e.g., update Unity objects)
            Debug.Log(message);
            Debug.Log("Received: " + message.model);
            MainThreadDispatcher.ExecuteOnMainThread(() =>
            {
                // Update the UI here
                t.text = message.model;
            });
        }
    }

    public void SendMessageToServer(Request message)
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
            var message = new Request
            {
                Q1 = 1,
                Q2 = 1,
                Q3 = 1,
                Q4 = 1,
                Q5 = 1,
                Q6 = 1,
                Q7 = 1,
                Q8 = 1,
                Q9 = 1,
                Q10 = 1
            };
            SendMessageToServer(message);
        }
    }
}

// Example message class
[Serializable]
public class Response
{
    [JsonProperty("model")] public string model ;
}
[Serializable]
public class Request
{
    [JsonProperty("q1")] public int Q1;
    [JsonProperty("q2")] public int Q2;
    [JsonProperty("q3")] public int Q3;
    [JsonProperty("q4")] public int Q4;
    [JsonProperty("q5")] public int Q5;
    [JsonProperty("q6")] public int Q6;
    [JsonProperty("q7")] public int Q7;
    [JsonProperty("q8")] public int Q8;
    [JsonProperty("q9")] public int Q9;
    [JsonProperty("q10")] public int Q10;
}
[Serializable]
public class Message
{
    public string content;
    public string timestamp;
}