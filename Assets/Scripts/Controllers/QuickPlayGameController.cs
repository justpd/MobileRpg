using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using System.Linq;

public class QuickPlayGameController : MonoBehaviour
{

    private UserSession[] userSessions = new UserSession[2];

    public Player[] players = new Player[6];
    public Player current;
    public Player target;
    public string lastTarget;

    public bool firstPlayer;

    public Text myName;
    public Text myRating;

    public Text oppName;
    public Text oppRating;

    public int tauntLeft;
    public int tauntRight;

   
    private QuickPlaySessionData quickPlaySessionDataObject;

    public Sprite targetStatus;
    public Sprite currentStatus;
    public Sprite usualStatus;

    public string SimulateTarget;
    private int[] secondPlayerIndexes = {3,4,5,0,1,2};
    private string[] myChars = new string[3];
    private string[] opponentChars = new string[3];

    public GameObject skillBar;

    private bool myMove;

    private void Awake()
    {

        firstPlayer = Data.quickPlaySessionInfo.firstPlayer;

        Debug.Log(JsonConvert.SerializeObject(Data.quickPlaySessionInfo));

        targetStatus = Resources.Load<Sprite>("Platforms/red_platform");
        currentStatus = Resources.Load<Sprite>("Platforms/blue_platform");
        usualStatus = Resources.Load<Sprite>("Platforms/gray_platform");

        players[0].id = Data.quickPlaySessionInfo.myCharIds[0];
        players[1].id = Data.quickPlaySessionInfo.myCharIds[1];
        players[2].id = Data.quickPlaySessionInfo.myCharIds[2];

        myChars[0] = Data.quickPlaySessionInfo.myCharIds[0];
        myChars[1] = Data.quickPlaySessionInfo.myCharIds[1];
        myChars[2] = Data.quickPlaySessionInfo.myCharIds[2];

        players[3].id = Data.quickPlaySessionInfo.opponentCharIds[0];
        players[4].id = Data.quickPlaySessionInfo.opponentCharIds[1];
        players[5].id = Data.quickPlaySessionInfo.opponentCharIds[2];

        opponentChars[0] = Data.quickPlaySessionInfo.opponentCharIds[0];
        opponentChars[1] = Data.quickPlaySessionInfo.opponentCharIds[1];
        opponentChars[2] = Data.quickPlaySessionInfo.opponentCharIds[2];

        players[0].name = Data.quickPlaySessionInfo.myCharNames[0];
        players[1].name = Data.quickPlaySessionInfo.myCharNames[1];
        players[2].name = Data.quickPlaySessionInfo.myCharNames[2];

        players[3].name = Data.quickPlaySessionInfo.opponentCharNames[0];
        players[4].name = Data.quickPlaySessionInfo.opponentCharNames[1];
        players[5].name = Data.quickPlaySessionInfo.opponentCharNames[2];

        myName.text = Data.userSession.login;
        myRating.text = Data.userSession.rating.ToString();
        oppName.text = Data.quickPlaySessionInfo.opponentName;
        oppRating.text = Data.quickPlaySessionInfo.opponentRating.ToString();
    }

    private void OnQuickGameData(QuickPlaySessionData quickPlaySessionData)
    {
        quickPlaySessionDataObject = quickPlaySessionData;
        myMove = myChars.Contains(quickPlaySessionDataObject.currentCharId);
        if (myMove)
        {
            skillBar.SetActive(true);
        }
        else
        {
            skillBar.SetActive(false);
        }
        CheckTargets();
        UpdateUI();
    }

    private void CheckTargets()
    {
        for (int i = 0; i < 6; i++)
        {
            if (players[i].health == 0 && players[i].id == lastTarget)
            {
                lastTarget = null;
                players[i].Status.sprite = usualStatus;
            }
            else if (!myMove && players[i].id == lastTarget)
            {
                players[i].Status.sprite = usualStatus;
            }
            else if (myMove && players[i].id == lastTarget)
            {
                SelectTarget(i);
            }
        }
    }

    private void UpdateUI()
    {
        for (int i = 0; i < 6; i++)
        {
            if (firstPlayer)
            {
                players[i].health = quickPlaySessionDataObject.HealthData[i];
                players[i].turnmeter = quickPlaySessionDataObject.TurnMeterData[i];
            }
            else
            {
                players[i].health = quickPlaySessionDataObject.HealthData[secondPlayerIndexes[i]];
                players[i].turnmeter = quickPlaySessionDataObject.TurnMeterData[secondPlayerIndexes[i]];
            }
            players[i].UpdateBars();

            Debug.Log(players[i].id + " | " + quickPlaySessionDataObject.currentCharId);
            if (players[i].id == quickPlaySessionDataObject.currentCharId)
            {
                players[i].Status.sprite = currentStatus;
            }
            else if (players[i].id == SimulateTarget)
            {
                players[i].Status.sprite = targetStatus;
            }
            else
            {
                players[i].Status.sprite = usualStatus;
            }

            if (players[i].health == 0)
            {
                players[i].Icon.color = new Color(players[i].Icon.color.r, players[i].Icon.color.g, players[i].Icon.color.b, 0.5F);
            }
        }
    }

    public void SelectTarget(int i)
    {
        if (opponentChars.Contains(players[i].id) && myMove && players[i].health > 0)
        {
            SimulateTarget = players[i].id;
            UpdateUI();
        }
    }

    public void SimulateMove()
    {
        if (myMove && SimulateTarget != null)
        {
            QuickPlayMoveData data = new QuickPlayMoveData
            {
                roomName = Data.quickPlaySessionInfo.roomName,
                roomId = Data.quickPlaySessionInfo.roomId,
                currentId = quickPlaySessionDataObject.currentCharId,
                targetId = SimulateTarget,
                skill = 1
            };
            ClientTCP.Send_QuickPlayMoveData(data);

            lastTarget = data.targetId;
            SimulateTarget = null;
            myMove = false;
            skillBar.SetActive(false);
            CheckTargets();
        }
    }

}
