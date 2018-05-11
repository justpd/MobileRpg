using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;

public class ClientTCP : MonoBehaviour {

    public string IP_ADRESS;
    public int PORT;
    public static GameObject ClientController;

    private bool connected;
    private bool connectionError = false;

    private static Socket _clientSock = new Socket (AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    private byte[] _assyncBuffer = new byte[1024];

    public static ClientTCP instance;

    private void Awake () {
        if (instance == null) {
            instance = this;
        } else {
            Destroy (gameObject);
            return;
        }
        DontDestroyOnLoad (gameObject);
    }

    // Use this for initialization
    void Start () {
        Debug.Log ("Connecting to a server...");
        _clientSock.BeginConnect (IP_ADRESS, PORT, new AsyncCallback (ConnectCallBack), _clientSock);
        StartCoroutine (ConnectionCheck (15));
        ClientController = GameObject.FindGameObjectWithTag("ClientController");
    }

    public static void UpdateController()
    {
        ClientController = GameObject.FindGameObjectWithTag("ClientController");
    }

    private void OnApplicationQuit () {
        connected = false;
        _clientSock.Close ();
    }

    // Update is called once per frame
    void Update () {
        if (connectionError) {
            StartCoroutine (Reconnect (5));
            connectionError = false;
        }
    }

    private void ConnectCallBack (IAsyncResult asyncResult) {
        _clientSock.EndConnect (asyncResult);
        connected = true;
        Debug.Log ("Connected.");
        while (connected) {
            OnRecieve ();
        }
    }

    IEnumerator ConnectionCheck (float delayTime) {
        yield return new WaitForSeconds (delayTime);
        if (!connected) {
            Debug.Log ("Can't connect to a server.");
        }
    }

    IEnumerator Reconnect (float delayTime) {
        yield return new WaitForSeconds (delayTime);
        if (!connected) {
            Debug.Log ("Trying to Recconect...");

            _clientSock = new Socket (AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _clientSock.BeginConnect (IP_ADRESS, PORT, new AsyncCallback (ConnectCallBack), _clientSock);

            StartCoroutine (Reconnect (5));
        }

    }

    private void OnRecieve () {
        byte[] sizeInfo = new byte[4];
        byte[] recievedBuffer = new byte[1024];

        int totalRead = 0, curentRead = 0;

        try {
            curentRead = totalRead = _clientSock.Receive (sizeInfo);
            if (totalRead <= 0) {
                HandleConnectionError (new Exception ("Empty package."));
            } else {
                while (totalRead < sizeInfo.Length && curentRead > 0) {
                    curentRead = _clientSock.Receive (sizeInfo, totalRead, sizeInfo.Length - totalRead, SocketFlags.None);
                    totalRead += curentRead;
                }

                // x |= y means x = x | y (bitwise Or)
                int messageSize = 0;
                messageSize |= sizeInfo[0];
                messageSize |= (sizeInfo[1] << 8);
                messageSize |= (sizeInfo[2] << 16);
                messageSize |= (sizeInfo[3] << 24);

                byte[] data = new byte[messageSize];
                totalRead = 0;
                curentRead = totalRead = _clientSock.Receive (data, totalRead, data.Length - totalRead, SocketFlags.None);

                while (totalRead < messageSize && curentRead > 0) {
                    curentRead = _clientSock.Receive (data, totalRead, data.Length - totalRead, SocketFlags.None);
                    totalRead += curentRead;
                }

                ClientHandleNetworkData.HandleNetworkInfo (data);
            }
        } catch (System.Exception ex) {
            HandleConnectionError (ex);
        }
    }

    private void HandleConnectionError (System.Exception ex) {
        Debug.Log ("Connection error. Server is not responding! " + ex);
        connected = false;
        _clientSock.Close ();
        connectionError = true;
    }

    public static void SendData (byte[] data) {

        byte[] sizeinfo = new byte[4];
        sizeinfo[0] = (byte)data.Length;
        sizeinfo[1] = (byte)(data.Length >> 8);
        sizeinfo[2] = (byte)(data.Length >> 16);
        sizeinfo[3] = (byte)(data.Length >> 24);

        _clientSock.Send (data);

    }

    // Main Senders

    public static void Send_Base64Image (string b64str, int scale, string login) {
        PacketBuffer buffer = new PacketBuffer ();
        buffer.WriteInt ((int) ClientPackets.C_RequestUpdateImage);
        UserImageData userImageData = new UserImageData {
            b64str = b64str,
            scale = scale,
            login = login
        };
        string json = JsonConvert.SerializeObject (userImageData);
        buffer.WriteString (json);
        SendData (buffer.ToArray ());
        buffer.Dispose ();
    }

    public static void Send_RequestUserLogin (string login, string password) {
        PacketBuffer buffer = new PacketBuffer ();
        buffer.WriteInt ((int) ClientPackets.C_RequestUserLogin);
        UserLoginData userData = new UserLoginData {
            login = login,
            password = password
        };
        string json = JsonConvert.SerializeObject (userData);
        buffer.WriteString (json);
        SendData (buffer.ToArray ());
        buffer.Dispose ();
    }

    public static void Send_UserLogout()
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInt((int)ClientPackets.C_RequestUserLogout);
        string json = JsonConvert.SerializeObject(new { login = Data.userSession.login });
        buffer.WriteString(json);
        SendData(buffer.ToArray());
        buffer.Dispose();
    }

    public static void Send_RequestUserRegistration (string login, string password, string email) {
        PacketBuffer buffer = new PacketBuffer ();
        buffer.WriteInt ((int) ClientPackets.C_RequestUserRegistration);
        UserRegistrationData userData = new UserRegistrationData {
            login = login,
            password = password,
            email = email
        };
        string json = JsonConvert.SerializeObject (userData);
        buffer.WriteString (json);
        SendData (buffer.ToArray ());
        buffer.Dispose ();
    }

    public static void Send_RequestUserAccountDataUpdate (string login) {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInt((int)ClientPackets.C_RequestUserAccountDataUpdate);
        Debug.Log("Updating userInfo");
        string json = JsonConvert.SerializeObject(new { login = login });
        buffer.WriteString(json);
        SendData(buffer.ToArray());
        buffer.Dispose();
    }

    public static void Send_RequestEnterQuickPlay(UserSession userSession)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInt((int)ClientPackets.C_RequestEnterQuickPlay);
        string json = JsonConvert.SerializeObject(new { login = userSession.login });
        buffer.WriteString(json);
        SendData(buffer.ToArray());
        buffer.Dispose();
    }

    public static void Send_QuickPlayMoveData(QuickPlaySessionData quickPlaySessionData)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteInt((int)ClientPackets.C_QuickPlayMoveData);
        string json = JsonConvert.SerializeObject(quickPlaySessionData);
        buffer.WriteString(json);
        SendData(buffer.ToArray());
        buffer.Dispose();
    }
    //###senddata###

} 