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

    public bool dealer;

    public Text myName;
    public Text myChips;

    public Text oppName;
    public Text oppChips;

    private QuickPlaySessionInfo quickPlaySessionInfoObject;
    private QuickPlaySessionData quickPlaySessionDataObject;

    private bool myMove;

    //public Text winnerText;
    //public GameObject gameOverBar;

    public Image myIcon;
    public Image opponentIcon;

    public Image myDealerCard;
    public Image oppDealerCard;

    public Image[] myTop = new Image[3];
    public Image[] myMid = new Image[5];
    public Image[] myBot = new Image[5];
    public Image[] myHand = new Image[5];
    public Image[] myTrunk = new Image[4];

    public Image[] oppTop = new Image[3];
    public Image[] oppMid = new Image[5];
    public Image[] oppBot = new Image[5];
    public Image[] oppHand = new Image[5];

    public Image[] oppTrunk = new Image[4];

    public Sprite Blank;
    public Sprite Back;
    public Sprite None;

    public Sprite tempCard;

    public Button Confirm;
    public Text Name;

    private void Awake()
    {
        Blank = Resources.Load<Sprite>("Sprites/blank");
        Back = Resources.Load<Sprite>("Sprites/gray_back");
        None = Resources.Load<Sprite>("Sprites/none");
        ResetCards();
        //gameOverBar.SetActive(false);
        quickPlaySessionInfoObject = JsonConvert.DeserializeObject<QuickPlaySessionInfo>(PlayerPrefs.GetString("QuickPlaySessionInfo", ""));


        Debug.Log(JsonConvert.SerializeObject(quickPlaySessionInfoObject));

        DrawCard(quickPlaySessionInfoObject.myDealerCard, myDealerCard);
        DrawCard(quickPlaySessionInfoObject.oppDealerCard, oppDealerCard);


        myName.text = Data.userSession.login;
        myChips.text = quickPlaySessionInfoObject.chips.ToString();
        oppName.text = quickPlaySessionInfoObject.opponentName;
        oppChips.text = quickPlaySessionInfoObject.chips.ToString();

        Name.text = quickPlaySessionInfoObject.name + " Куш: " + quickPlaySessionInfoObject.point.ToString();

        myIcon.sprite = Sprite.Create(Data.userImageTexture, new Rect(0.0f, 0.0f, Data.userImageTexture.width, Data.userImageTexture.height), new Vector2(0.5f, 0.5f), 100.0f);
        opponentIcon.sprite = DecodeImage(quickPlaySessionInfoObject.enemyImage, quickPlaySessionInfoObject.enemyImageScale);

    }

    private void DrawCard(string card, Image image)
    {
        tempCard = Resources.Load<Sprite>("Sprites/" + card);
        image.sprite = tempCard;
        tempCard = null;
    }

    private void ResetCards()
    {
        foreach (Image card in myTop)
        {
            card.sprite = Blank;
        }
        foreach (Image card in myMid)
        {
            card.sprite = Blank;
        }
        foreach (Image card in myBot)
        {
            card.sprite = Blank;
        }
        foreach (Image card in oppTop)
        {
            card.sprite = Blank;
        }
        foreach (Image card in oppMid)
        {
            card.sprite = Blank;
        }
        foreach (Image card in oppBot)
        {
            card.sprite = Blank;
        }
        foreach (Image card in myHand)
        {
            card.sprite = None;
        }
        foreach (Image card in myTrunk)
        {
            card.sprite = None;
        }
        foreach (Image card in oppHand)
        {
            card.sprite = None;
        }
        foreach (Image card in oppTrunk)
        {
            card.sprite = None;
        }

        myDealerCard.sprite = None;
        oppDealerCard.sprite = None;
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

    }


    public void ExitSession()
    {
        SceneManager.LoadScene(0);
    }

}
