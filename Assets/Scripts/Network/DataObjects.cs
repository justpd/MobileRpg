using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class UserLoginData
{
    public string login;
    public string password;
}

public class UserRegistrationData
{
    public string login;
    public string password;
    public string email;
}

public class QuickPlaySessionInfo
{
    public string roomName;
    public int roomId;
    public bool firstPlayer;
    public string opponentName;
    public int opponentRating;

    public string[] myCharNames = new string[3];
    public string[] opponentCharNames = new string[3];

    public string[] myCharIds = new string[3];
    public string[] opponentCharIds = new string[3];
}

public class QuickPlaySessionData
{
    public string roomName;
    public int roomId;

    public List<float> HealthData = new List<float>();
    public List<float> TurnMeterData = new List<float>();


    public string currentCharId;
}

public class QuickPlayMoveData
{
    public string roomName;
    public int roomId;

    public string currentId;
    public string targetId;

    public int skill;
}

public class UserSession
{
    public string login;
    public string id;

    public int exp;
    public int gold;
    public int energy;
    public int rating;
    public int faction;

    public UserChar[] mainTeam = new UserChar[3];
    public string[] mainTeamNames = new string[3];
}


