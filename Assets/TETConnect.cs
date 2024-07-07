using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Text;
using System.IO;
using UnityEngine;
using System;

public class TETConnect : MonoBehaviour
{
    private Thread thread;
    private ThreadStart ts;
    private bool keepOnRunning = true;
    private TcpClient client;
    private Stream stream;
    private int bufferSize = 4096;
    private int ThreadSleepTime = 10;
    private byte[] buffer;

    public TETConnect() { }

    public void Connect()
    {
        client = new TcpClient("127.0.0.1", 6555);// -> Thee EyeTribe server
        stream = client.GetStream();
        buffer = new byte[bufferSize];
        byte[] writeBuffer = Encoding.ASCII.GetBytes(@"{""enableRawOutput"": true, ""format"": ""Json""}");
        stream.Write(writeBuffer, 0, writeBuffer.Length);
        while (keepOnRunning)
        {
            /////main loop
            ParseData();
            Thread.Sleep(ThreadSleepTime);
        }
        Disconnect();
    }

    private void ParseData()
    {
        if (stream.CanRead)
        {
            try
            {
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string packet = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                StringReader Rdr = new StringReader(packet);

                /////////////
                Debug.Log(packet);
                /////////////

                while (true)
                {
                    string dataLine = Rdr.ReadLine();
                    if (dataLine != null)
                    {
                        ///do something here 

                    }
                    else
                    {
                        break;
                    }
                }
            }
            catch (IOException e)
            {
                Debug.Log("IOException: " + e);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        ts = new ThreadStart(Connect);
        thread = new Thread(ts);
        thread.Start();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnDestroy()
    {
        keepOnRunning = false;
    }
    private void OnDisable()
    {
        keepOnRunning = false;
    }
    private void OnApplicationQuit()
    {
        keepOnRunning = false;
    }
    private void Disconnect()
    {
        Task.Delay(ThreadSleepTime * 2);//just in case 
        stream.Close();
        thread.Abort();//just in case 
    }

}
