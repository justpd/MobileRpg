using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;

public class ClientHandleNetworkData : MonoBehaviour {

    private delegate void Packet_ (byte[] data);
    private static Dictionary<int, Packet_> Packets;

    private static bool EventUserLogin = false;
    private static bool EventUserRegisttration = false;
    private static bool EventQuickPlay = false;
    private static bool EventQuickPlayInfo = false;
    private static bool EventQuickPlayData = false;

    private static UserSession activeUserSession;
    private static QuickPlaySessionInfo activeQuickPlaySessionInfo;
    private static QuickPlaySessionData activeQuickPlaySessionData;

    // Use this for initialization
    void Awake () {
        InitializeNetworkPackages ();
    }

    // Update is called once per frame
    void Update () {
        if (EventUserLogin)
        {
            ClientTCP.ClientController.SendMessage("OnUserLogin", activeUserSession);
            EventUserLogin = false;
        }
        else if (EventUserRegisttration)
        {
            ClientTCP.ClientController.SendMessage("OnUserRegister");
            EventUserRegisttration = false;
        }
        else if (EventQuickPlayInfo)
        {
            ClientTCP.ClientController.SendMessage("OnQuickGameInfo", activeQuickPlaySessionInfo);
            EventQuickPlayInfo = false;
        }
        else if (EventQuickPlayData)
        {
            ClientTCP.UpdateController();
            ClientTCP.ClientController.SendMessage("OnQuickGameData", activeQuickPlaySessionData);
            EventQuickPlayData = false;
        }
    }

    public static void InitializeNetworkPackages () {
        Packets = new Dictionary<int, Packet_> {
            {
            (int) ServerPackets.S_ConfirmConnection, Handle_ConfirmConnection
            },
            {
            (int) ServerPackets.S_ConfirmUserLogin,
            Handle_ConfirmUserLogin
            },
            {
            (int) ServerPackets.S_AbortUserLogin,
            Handle_AbortUserLogin
            },
            {
            (int) ServerPackets.S_ConfirmUserRegistration,
            Handle_ConfirmUserRegistration
            },
            {
            (int) ServerPackets.S_AbortUserRegistration,
            Handle_AbortUserRegistration
            },
            {
            (int) ServerPackets.S_SendQuickPlaySessionInfo,
            Handle_QuickPlaySessionInfo
            },
            {
            (int) ServerPackets.S_SendQuickPlaySessionData,
            Handle_QuickPlaySessionData
            }
        };

    }

    public static void HandleNetworkInfo (byte[] data) {
        PacketBuffer buffer = new PacketBuffer ();
        buffer.WriteBytes (data);
        int packetNum = buffer.ReadInteger ();
        buffer.Dispose ();
        Packet_ Packet;
        if (Packets.TryGetValue (packetNum, out Packet)) {
            Packet.Invoke (data);
        }
    }

    // Main Handlers

    private static void Handle_ConfirmConnection (byte[] data) {
        PacketBuffer buffer = new PacketBuffer ();
        buffer.WriteBytes (data);
        int packetNum = buffer.ReadInteger ();
        string msg = buffer.ReadString ();
        buffer.Dispose ();

        //add your code you want to execute here;
        Debug.Log ("Sever: " + msg);

        ClientTCP.Send_ConfirmConnection ();
    }

    public static void Handle_ConfirmUserLogin (byte[] data) {
        PacketBuffer buffer = new PacketBuffer ();
        buffer.WriteBytes (data);
        int packetNum = buffer.ReadInteger ();
        string msg = buffer.ReadString ();
        buffer.Dispose ();

        //add your code you want to execute here;
        Debug.Log (msg);
        activeUserSession = JsonConvert.DeserializeObject<UserSession> (msg);
        EventUserLogin = true;
    }

    public static void Handle_AbortUserLogin (byte[] data) {
        PacketBuffer buffer = new PacketBuffer ();
        buffer.WriteBytes (data);
        int packetNum = buffer.ReadInteger ();
        string msg = buffer.ReadString ();
        buffer.Dispose ();

        //add your code you want to execute here;
        Debug.Log (msg);
    }

    public static void Handle_ConfirmUserRegistration (byte[] data) {
        PacketBuffer buffer = new PacketBuffer ();
        buffer.WriteBytes (data);
        int packetNum = buffer.ReadInteger ();
        string msg = buffer.ReadString ();
        buffer.Dispose ();

        //add your code you want to execute here;
        Debug.Log (msg);
        EventUserRegisttration = true;

    }

    public static void Handle_AbortUserRegistration (byte[] data) {
        PacketBuffer buffer = new PacketBuffer ();
        buffer.WriteBytes (data);
        int packetNum = buffer.ReadInteger ();
        string msg = buffer.ReadString ();
        buffer.Dispose ();

        //add your code you want to execute here;
        Debug.Log (msg);
    }
    public static void Handle_QuickPlaySessionInfo(byte[] data)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteBytes(data);
        int packetNum = buffer.ReadInteger();
        string msg = buffer.ReadString();
        buffer.Dispose();

        //add your code you want to execute here;
        Debug.Log(msg);
        activeQuickPlaySessionInfo = JsonConvert.DeserializeObject<QuickPlaySessionInfo>(msg);
        EventQuickPlayInfo = true;
    }
    public static void Handle_QuickPlaySessionData(byte[] data)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteBytes(data);
        int packetNum = buffer.ReadInteger();
        string msg = buffer.ReadString();
        buffer.Dispose();

        //add your code you want to execute here;
        Debug.Log(msg);
        activeQuickPlaySessionData = JsonConvert.DeserializeObject<QuickPlaySessionData>(msg);
        EventQuickPlayData = true;
    }

}