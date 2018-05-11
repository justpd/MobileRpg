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

    public GameObject SkillBar;

    public Image myIcon;
    public Image opponentIcon;

    private void Awake()
    {
        gameOverBar.SetActive(false);
        quickPlaySessionInfoObject = JsonConvert.DeserializeObject<QuickPlaySessionInfo>(PlayerPrefs.GetString("QuickPlaySessionInfo", ""));


        Debug.Log(JsonConvert.SerializeObject(quickPlaySessionInfoObject));

        targetStatus = Resources.Load<Sprite>("Platforms/red_platform");
        currentStatus = Resources.Load<Sprite>("Platforms/blue_platform");
        usualStatus = Resources.Load<Sprite>("Platforms/gray_platform");


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

    public void SimulateMove(int skill)
    {

    }

    public void ExitSession()
    {
        SceneManager.LoadScene(0);
    }

}
