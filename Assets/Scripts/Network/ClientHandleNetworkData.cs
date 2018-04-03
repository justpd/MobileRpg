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

    private static bool EventUserSessionUpdate = false;
    private static bool EventUserLogin = false;
    private static bool EventUserRegisttration = false;
    private static bool EventQuickPlayInfo = false;
    private static bool EventQuickPlayData = false;
    private static bool EventImageUpdate = false;
    //###addboolevent###

    private static UserSession activeUserSession;
    private static QuickPlaySessionInfo activeQuickPlaySessionInfo;
    private static QuickPlaySessionData activeQuickPlaySessionData;
    private static UserImageData userImageData;
    //###addobjectevent###

    // Use this for initialization
    void Awake () {
        InitializeNetworkPackages ();
    }

    // Update is called once per frame
    void Update () {
        if (EventUserLogin)
        {
            ClientTCP.UpdateController();
            ClientTCP.ClientController.SendMessage("OnUserLogin", activeUserSession);
            EventUserLogin = false;
        }
        else if (EventUserSessionUpdate)
        {
            ClientTCP.UpdateController();
            ClientTCP.ClientController.SendMessage("OnUserSessionUpdate", activeUserSession);
            EventUserSessionUpdate = false;
        }
        else if (EventUserRegisttration)
        {
            ClientTCP.UpdateController();
            ClientTCP.ClientController.SendMessage("OnUserRegister");
            EventUserRegisttration = false;
        }
        else if (EventQuickPlayInfo)
        {
            ClientTCP.UpdateController();
            ClientTCP.ClientController.SendMessage("OnQuickGameInfo", activeQuickPlaySessionInfo);
            EventQuickPlayInfo = false;
        }
        else if (EventQuickPlayData)
        {
            ClientTCP.UpdateController();
            ClientTCP.ClientController.SendMessage("OnQuickGameData", activeQuickPlaySessionData);
            EventQuickPlayData = false;
        }
        else if (EventImageUpdate)
        {
            ClientTCP.UpdateController();
            ClientTCP.ClientController.SendMessage("OnImageUpdate", userImageData);
            EventImageUpdate = false;
        }
        //###addeventupdate###
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
            },
            {
            (int) ServerPackets.S_UpdateUserSessionData,
            Handle_UserAcountDataUpdate
            },
            {
            (int) ServerPackets.S_UpdateUserImage,
            Handle_ImageUpdate
            },
            //###inithandler###
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

    private static void Handle_ImageUpdate (byte[] data) {
        PacketBuffer buffer = new PacketBuffer ();
        buffer.WriteBytes (data);
        int packetNum = buffer.ReadInteger ();
        string msg = buffer.ReadString ();
        buffer.Dispose ();

        userImageData = JsonConvert.DeserializeObject<UserImageData>(msg);
        EventImageUpdate = true;
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

    public static void Handle_UserAcountDataUpdate(byte[] data)
    {
        PacketBuffer buffer = new PacketBuffer();
        buffer.WriteBytes(data);
        int packetNum = buffer.ReadInteger();
        string msg = buffer.ReadString();
        buffer.Dispose();

        //add your code you want to execute here;
        Debug.Log(msg);
        activeUserSession = JsonConvert.DeserializeObject<UserSession>(msg);
        EventUserSessionUpdate = true;
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
    //###addhandler###

}