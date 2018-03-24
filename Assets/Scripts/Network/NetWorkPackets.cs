using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Server -> Client
public enum ServerPackets
{
    S_ConfirmConnection = 606001,
    S_ConfirmUserLogin = 606002,
    S_AbortUserLogin = 606003,
    S_ConfirmUserRegistration = 606004,
    S_AbortUserRegistration = 606005,
    S_SendQuickPlaySessionInfo = 606006,
    S_SendQuickPlaySessionData = 606007,
    S_UpdateUserSessionData = 606008
}

// Client -> Server
public enum ClientPackets
{
    C_ConfirmConnection = 505001,
    C_RequestUserLogin = 505002,
    C_RequestUserRegistration = 505003,
    C_RequestUserAccountDataUpdate = 505004,
    C_RequestEnterQuickPlay = 505005,
    C_QuickPlayMoveData = 505006,
    C_RequestUserLogout = 505000,
}
