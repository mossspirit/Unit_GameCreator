using System;
using System.Text;
using System.IO.Ports;
using UnityEngine;
using UnityEngine.SceneManagement; // コレ重要
using UniRx;

public class SerialController : MonoBehaviour
{
    public delegate void SerialDataReceivedEventHandler(string message);
    public event SerialDataReceivedEventHandler OnDataReceived;

    string portName;
    SerialPort serial;
    bool isLoop = true;
    bool isStart = false;
    string msg;

    [NonSerialized]
    public string[] messages;
    [NonSerialized]
    public float[] data = new float[3];

    private void Awake()
    {
        if (SceneManager.GetActiveScene().name == "Title")
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
    private void Start()
    {
        
    }
    public void Connect(string com)
    {
        Debug.Log("go");

        this.serial = new SerialPort(com, 115200, Parity.None, 8, StopBits.One);

        try
        {
            this.serial.Open();
            Scheduler.ThreadPool.Schedule(() => ReadData()).AddTo(this);
        }
        catch (Exception e)
        {
            Debug.Log("can not open serial port");
        }
    }
    private void ReadData()
    {
        while (this.isLoop && this.serial != null && this.serial.IsOpen)
        {
            try
            {
                msg = this.serial.ReadLine();
                messages = msg.Split(
                new string[] { " " }, System.StringSplitOptions.None);
                Debug.Log(messages[2]);
                
            }
            catch (System.Exception e)
            {
                Debug.LogWarning(e.Message);
            }
        }
    }
    public void Write(byte[] buffer)
    {
        this.serial.Write(buffer, 0, 1);
    }

    void OnDestroy()
    {
        this.isLoop = false;
        this.serial.Close();
    }
}