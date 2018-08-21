using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameSession {

    public static string[] playerList = new string[6];

    public static List<UserChar> players = new List<UserChar>();
    public static UserChar[] positions = new UserChar[6];

    public static UserChar current;
    public static UserChar target;
    public static UserChar lastTarget_1;
    public static UserChar lastTarget_2;

    public static int tauntLeft;
    public static int tauntRight;

    public static GameController gameController;

}
