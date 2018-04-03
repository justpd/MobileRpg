using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using System.Linq;
using UnityEngine.SceneManagement;

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

    private QuickPlaySessionInfo quickPlaySessionInfoObject;
    private QuickPlaySessionData quickPlaySessionDataObject;

    public Sprite targetStatus;
    public Sprite currentStatus;
    public Sprite usualStatus;

    public string SimulateTarget;
    private int[] secondPlayerIndexes = { 3, 4, 5, 0, 1, 2 };
    private string[] myChars = new string[3];
    private string[] opponentChars = new string[3];

    public GameObject skillBar;

    private bool myMove;

    public Text winnerText;
    public GameObject gameOverBar;

    public Image myIcon;
    public Image opponentIcon;

    private void Awake()
    {
        gameOverBar.SetActive(false);
        quickPlaySessionInfoObject = JsonConvert.DeserializeObject<QuickPlaySessionInfo>(PlayerPrefs.GetString("QuickPlaySessionInfo", ""));

        firstPlayer = quickPlaySessionInfoObject.firstPlayer;

        Debug.Log(JsonConvert.SerializeObject(quickPlaySessionInfoObject));

        targetStatus = Resources.Load<Sprite>("Platforms/red_platform");
        currentStatus = Resources.Load<Sprite>("Platforms/blue_platform");
        usualStatus = Resources.Load<Sprite>("Platforms/gray_platform");

        players[0].id = quickPlaySessionInfoObject.myCharIds[0];
        players[1].id = quickPlaySessionInfoObject.myCharIds[1];
        players[2].id = quickPlaySessionInfoObject.myCharIds[2];

        myChars[0] = quickPlaySessionInfoObject.myCharIds[0];
        myChars[1] = quickPlaySessionInfoObject.myCharIds[1];
        myChars[2] = quickPlaySessionInfoObject.myCharIds[2];

        players[3].id = quickPlaySessionInfoObject.opponentCharIds[0];
        players[4].id = quickPlaySessionInfoObject.opponentCharIds[1];
        players[5].id = quickPlaySessionInfoObject.opponentCharIds[2];

        opponentChars[0] = quickPlaySessionInfoObject.opponentCharIds[0];
        opponentChars[1] = quickPlaySessionInfoObject.opponentCharIds[1];
        opponentChars[2] = quickPlaySessionInfoObject.opponentCharIds[2];

        players[0].name = quickPlaySessionInfoObject.myCharNames[0];
        players[1].name = quickPlaySessionInfoObject.myCharNames[1];
        players[2].name = quickPlaySessionInfoObject.myCharNames[2];

        players[3].name = quickPlaySessionInfoObject.opponentCharNames[0];
        players[4].name = quickPlaySessionInfoObject.opponentCharNames[1];
        players[5].name = quickPlaySessionInfoObject.opponentCharNames[2];

        myName.text = Data.userSession.login;
        myRating.text = Data.userSession.rating.ToString();
        oppName.text = quickPlaySessionInfoObject.opponentName;
        oppRating.text = quickPlaySessionInfoObject.opponentRating.ToString();

        myIcon.sprite = Sprite.Create(Data.userImageTexture, new Rect(0.0f, 0.0f, Data.userImageTexture.width, Data.userImageTexture.height), new Vector2(0.5f, 0.5f), 100.0f);
        opponentIcon.sprite = DecodeImage(quickPlaySessionInfoObject.enemyImage, quickPlaySessionInfoObject.enemyImageScale);
    }

    private Sprite DecodeImage(string b64str, int scale)
    {
        byte[] b64_bytes = System.Convert.FromBase64String(b64str);
        Texture2D texture = new Texture2D(scale, scale)
        {
            filterMode = FilterMode.Point,
        };
        texture.LoadImage(b64_bytes);
        return Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
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
        if (quickPlaySessionData.gameOver)
        {
            gameOverBar.SetActive(true);
            winnerText.text = quickPlaySessionData.winner + " won!";
        }

        foreach (MoveLog log in quickPlaySessionData.moveLogs)
        {
            Debug.Log(JsonConvert.SerializeObject(log));
        }

        Debug.Log(quickPlaySessionData.moveInfo.skillCount);
        Debug.Log(quickPlaySessionData.moveInfo.classID);

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
                roomName = quickPlaySessionInfoObject.roomName,
                roomId = quickPlaySessionInfoObject.roomId,
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

    public void ExitSession()
    {
        SceneManager.LoadScene(0);
    }

}
