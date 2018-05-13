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

    public DeckZone[] myTop = new DeckZone[3];
    public DeckZone[] myMid = new DeckZone[5];
    public DeckZone[] myBot = new DeckZone[5];
    public PlayableCard[] myHand = new PlayableCard[5];
    public Image[] myTrunk = new Image[4];

    public Image[] oppTop = new Image[3];
    public Image[] oppMid = new Image[5];
    public Image[] oppBot = new Image[5];
    public Image[] oppHand = new Image[5];

    public List<Image[]> oppDeck = new List<Image[]>();

    public Image[] oppTrunk = new Image[4];

    public Sprite Blank;
    public Sprite Back;
    public Sprite None;

    public Sprite tempCard;

    public Button Confirm;
    public Text Name;

    public int roomID;

    public int myTrunkNum = 0;
    public int oppTrunkNum = 0;

    public GameObject[] myCombinations = new GameObject[3];
    public GameObject[] oppCombinations = new GameObject[3];

    private void Awake()
    {
        Blank = Resources.Load<Sprite>("Sprites/blank");
        Back = Resources.Load<Sprite>("Sprites/gray_back");
        None = Resources.Load<Sprite>("Sprites/none");

        oppDeck.Add(oppTop);
        oppDeck.Add(oppMid);
        oppDeck.Add(oppBot);

        ResetCards();
        //gameOverBar.SetActive(false);
        quickPlaySessionInfoObject = JsonConvert.DeserializeObject<QuickPlaySessionInfo>(PlayerPrefs.GetString("QuickPlaySessionInfo", ""));


        dealer = quickPlaySessionInfoObject.dealer;
        roomID = quickPlaySessionInfoObject.roomId;



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

    public void DrawCard(string card, Image image)
    {
        tempCard = Resources.Load<Sprite>("Sprites/" + card);
        image.sprite = tempCard;
        tempCard = null;
    }

    private void ResetCards()
    {
        Confirm.interactable = false;

        foreach (DeckZone card in myTop)
        {
            card.image.sprite = Blank;
        }
        foreach (DeckZone card in myMid)
        {
            card.image.sprite = Blank;
        }
        foreach (DeckZone card in myBot)
        {
            card.image.sprite = Blank;
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
        foreach (PlayableCard card in myHand)
        {
            card.image.sprite = None;
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

        foreach (GameObject combination in myCombinations)
        {
            combination.SetActive(false);
        }

        foreach (GameObject combination in oppCombinations)
        {
            combination.SetActive(false);
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

        Debug.Log("Working ?");

        if (!quickPlaySessionData.myTurn)
        {
            if (quickPlaySessionData.firstHand)
            {
                // Opponent's Turn
                int i = 0;
                foreach (string card in quickPlaySessionData.hand.Split(','))
                {
                    Debug.Log("Card" + card);
                    DrawCard(card, oppHand[i]);
                    i++;
                }
            }
            else
            {
                if (quickPlaySessionDataObject.oppHandRanks[0] > 0 &&
                    quickPlaySessionDataObject.oppHandRanks[1] > 0 &&
                    quickPlaySessionDataObject.oppHandRanks[2] > 0)
                {
                    // End of Game
                }
                else
                {
                    oppHand[0].sprite = Back;
                    oppHand[1].sprite = Back;
                    oppHand[2].sprite = Back;
                }
            }
            Confirm.interactable = false;
        }
        else
        {
            if (quickPlaySessionData.position != "")
            {
                Debug.Log("Пришла инфа о ходе соперника");
                foreach (Image card in oppHand)
                {
                    card.sprite = None;
                }

                string[] cards = quickPlaySessionData.opponentHand.Split(',');

                Debug.Log("Начали цикл");
                for (int j = 0; j < quickPlaySessionData.position.Length; j += 2)
                {
                    Debug.Log(quickPlaySessionData.position[j]);
                    if (int.Parse(quickPlaySessionData.position[j] + "") == 3)
                    {
                        oppTrunk[oppTrunkNum].sprite = Back;
                        oppTrunkNum += 1;
                    }
                    else
                        DrawCard(cards[j / 2], oppDeck[int.Parse(quickPlaySessionData.position[j] + "")][int.Parse(quickPlaySessionData.position[j + 1] + "")]);
                }
            }

            if (quickPlaySessionData.hand != "")
            {
                oppHand[0].sprite = None;
                oppHand[1].sprite = None;
                oppHand[2].sprite = None;

                Debug.Log("My turn.");
                int i = 0;
                foreach (string card in quickPlaySessionData.hand.Split(','))
                {
                    Debug.Log("Card" + card);
                    DrawCard(card, myHand[i].image);
                    myHand[i].value = card;
                    i++;

                }
                Confirm.interactable = false;
            }
        }

        for (int i = 0; i < 3; i++)
        {
            if (quickPlaySessionDataObject.myHandStr[i] != "")
            {
                myCombinations[i].SetActive(true);
                myCombinations[i].GetComponentInChildren<Text>().text = quickPlaySessionDataObject.myHandStr[i];
            }
        }

        for (int i = 0; i < 3; i++)
        {
            if (quickPlaySessionDataObject.oppHandStr[i] != "")
            {
                oppCombinations[i].SetActive(true);
                oppCombinations[i].GetComponentInChildren<Text>().text = quickPlaySessionDataObject.oppHandStr[i];
            }
        }

        if (myDealerCard.sprite != None)
        {
            myDealerCard.sprite = None;
            oppDealerCard.sprite = None;
        }
    }

    public void ConfirmMove()
    {
        Confirm.interactable = false;
        string result = "";
        for (int i = 0; i < 3; i++)
        {
            if (myHand[i].position == "")
            {
                myHand[i].position = "3" + myTrunkNum.ToString();
                DrawCard(myHand[i].value, myTrunk[myTrunkNum]);
                myTrunkNum += 1;
            }
        }
        foreach (PlayableCard card in myHand)
        {
            if (card.position != "")
            {
                result += card.position;
            }
        }
        foreach (DeckZone card in myTop)
        {
            card.lastCard = null;
        }
        foreach (DeckZone card in myMid)
        {
            card.lastCard = null;
        }
        foreach (DeckZone card in myBot)
        {
            card.lastCard = null;
        }
        Debug.Log(result);
        QuickPlayMoveData moveData = new QuickPlayMoveData
        {
            roomId = roomID,
            position = result
        };

        foreach (PlayableCard card in myHand)
        {
            if (card.position != "")
            {
                card.position = "";
                card.image.sprite = Blank;
            }
        }

        ClientTCP.Send_QuickPlayMoveData(moveData);
    }

    public void ExitSession()
    {
        SceneManager.LoadScene(0);
    }

    public void EnableConfirm()
    {

        int placed = 0;
        foreach (PlayableCard card in myHand)
        {
            if (card.position != "")
            {
                placed++;
            }
        }
        if (placed == 2 && !quickPlaySessionDataObject.firstHand)
        {
            Confirm.interactable = true;
        }
        else if (placed == 5 && quickPlaySessionDataObject.firstHand)
        {
            Confirm.interactable = true;
        }
        else
        {
            Confirm.interactable = false;
        }
    }

}
