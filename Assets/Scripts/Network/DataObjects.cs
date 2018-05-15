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

public class QuickPlaySessionNewRound
{
    public int roomId;
    public bool dealer;
    public int myScore;
    public int point;
    public string winner;
    public bool myBroken;
    public bool oppBroken;
    public bool myFantasy;
    public bool oppFantasy;
    public string hand;
    public string[] oppHandStr;
}

public class QuickPlaySessionInfo
{
    public int roomId;

    public string opponentName;
    public int opponentRating;

    public string enemyImage;
    public int enemyImageScale;

    public string myDealerCard;
    public string oppDealerCard;

    public bool dealer;

    public string name;
    public int point;
    public int chips;
}

public class QuickPlaySessionData
{
    public int roomId;

    public bool firstHand;

    public bool myTurn;

    public string hand;

    public string opponentHand;

    public string position;

    public string[] myHandStr;
    public int[] myHandRanks;

    public string[] oppHandStr;
    public int[] oppHandRanks;

    //public List<MoveLog> moveLogs = new List<MoveLog>();
}

public class QuickPlayMoveData
{
    public int roomId;

    public string position;

    public string login;
}

public class MoveLog
{
    public string name;
    public Card[] hand = new Card[3];
    public Card lost;
    public Deck deck;
}

public class Card
{
    public string value;
}

public class Deck
{
    public Card[] low = new Card[3];
    public Card[] mid = new Card[5];
    public Card[] top = new Card[5];

    public string lowHand;
    public string midHand;
    public string topHand;
}

public class UserSession
{
    public string id;
    public string login;
    public int gold;
    public int experience;
    public int energy;
    public int rating;   
}

public class UserImageData
{
    public string b64str;
    public int scale;
    public string login;
}