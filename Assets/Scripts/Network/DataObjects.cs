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

    public string hand;

    //public List<MoveLog> moveLogs = new List<MoveLog>();
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