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
    public int myChipCount;

    public Text oppName;
    public Text oppChips;
    public int oppChipCount;

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
    public int point;

    public int myTrunkNum = 0;
    public int oppTrunkNum = 0;

    public GameObject[] myCombinations = new GameObject[3];
    public GameObject[] oppCombinations = new GameObject[3];

    public GameObject[] myRoyalties = new GameObject[3];
    public GameObject[] oppRoyalties = new GameObject[3];

    public GameObject myScore;
    public GameObject oppScore;
    public GameObject gameOver;
    public Image winner;
    public bool newHand = false;

    private void Awake()
    {
        gameOver.SetActive(false);
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
        myChipCount = quickPlaySessionInfoObject.chips;
        myChips.text = myChipCount.ToString();
        oppName.text = quickPlaySessionInfoObject.opponentName;
        oppChipCount = quickPlaySessionInfoObject.chips;
        oppChips.text = oppChipCount.ToString();
        point = quickPlaySessionInfoObject.point;
        Name.text = quickPlaySessionInfoObject.name + " Куш: " + point.ToString();

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
        Debug.Log("STARTED RESETING");
        Confirm.interactable = false;

        foreach (DeckZone card in myTop)
        {
            card.image.sprite = Blank;
            card.image.color = new Color32(255, 255, 255, 255);
            card.value = "";
            card.lastCard = null;
        }
        foreach (DeckZone card in myMid)
        {
            card.image.sprite = Blank;
            card.image.color = new Color32(255, 255, 255, 255);
            card.value = "";
            card.lastCard = null;
        }
        foreach (DeckZone card in myBot)
        {
            card.image.sprite = Blank;
            card.image.color = new Color32(255, 255, 255, 255);
            card.value = "";
            card.lastCard = null;
        }
        foreach (Image card in oppTop)
        {
            card.sprite = Blank;
            card.color = new Color32(255, 255, 255, 255);
        }
        foreach (Image card in oppMid)
        {
            card.sprite = Blank;
            card.color = new Color32(255, 255, 255, 255);
        }
        foreach (Image card in oppBot)
        {
            card.sprite = Blank;
            card.color = new Color32(255, 255, 255, 255);
        }
        foreach (PlayableCard card in myHand)
        {
            card.image.sprite = None;
            card.value = "";
            card.position = "";
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
        foreach (GameObject combination in myRoyalties)
        {
            combination.SetActive(false);
        }
        foreach (GameObject combination in oppRoyalties)
        {
            combination.SetActive(false);
        }

        myScore.SetActive(false);
        oppScore.SetActive(false);

        myTrunkNum = 0;
        oppTrunkNum = 0;
        myDealerCard.sprite = None;
        oppDealerCard.sprite = None;

        Debug.Log("FINISHED RESETING");
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

    private void OnQuickGameNewRound(QuickPlaySessionNewRound quickPlaySessionNewRound)
    {
        if (quickPlaySessionNewRound.myBroken)
        {
            foreach (DeckZone card in myTop)
            {
                card.image.color = new Color32(255, 148, 148, 255);
            }
            foreach (DeckZone card in myMid)
            {
                card.image.color = new Color32(255, 148, 148, 255);
            }
            foreach (DeckZone card in myBot)
            {
                card.image.color = new Color32(255, 148, 148, 255);
            }
            foreach (GameObject combination in myRoyalties)
            {
                combination.SetActive(false);
            }
        }
        if (quickPlaySessionNewRound.oppBroken)
        {
            foreach (Image card in oppTop)
            {
                card.color = new Color32(255,148, 148, 255);
            }
            foreach (Image card in oppMid)
            {
                card.color = new Color32(255, 148, 148, 255);
            }
            foreach (Image card in oppBot)
            {
                card.color = new Color32(255, 148, 148, 255);
            }
            foreach (GameObject combination in oppRoyalties)
            {
                combination.SetActive(false);
            }
        }
        newHand = true;
        myChipCount += quickPlaySessionNewRound.myScore * point;
        oppChipCount -= quickPlaySessionNewRound.myScore * point;
        myChips.text = myChipCount.ToString();
        oppChips.text = oppChipCount.ToString();
        myScore.SetActive(true);
        oppScore.SetActive(true);
        if (quickPlaySessionNewRound.myScore > 0)
            myScore.GetComponentInChildren<Text>().text = "+" + Mathf.Abs(quickPlaySessionNewRound.myScore).ToString();
        else if (quickPlaySessionNewRound.myScore < 0)
            myScore.GetComponentInChildren<Text>().text = "-" + Mathf.Abs(quickPlaySessionNewRound.myScore).ToString();
        else
            myScore.GetComponentInChildren<Text>().text = "0";

        if (quickPlaySessionNewRound.myScore > 0)
            oppScore.GetComponentInChildren<Text>().text = "-" + Mathf.Abs(quickPlaySessionNewRound.myScore).ToString();
        else if (quickPlaySessionNewRound.myScore < 0)
            oppScore.GetComponentInChildren<Text>().text = "+" + Mathf.Abs(quickPlaySessionNewRound.myScore).ToString();
        else
            oppScore.GetComponentInChildren<Text>().text = "0";

        point = quickPlaySessionNewRound.point;
        Name.text = quickPlaySessionInfoObject.name + " Куш: " + point.ToString();
        dealer = quickPlaySessionNewRound.dealer;

        if (quickPlaySessionNewRound.winner != "")
        {
            gameOver.SetActive(true);
            if (quickPlaySessionNewRound.winner == Data.userSession.login)
            {
                winner.sprite = Sprite.Create(Data.userImageTexture, new Rect(0.0f, 0.0f, Data.userImageTexture.width, Data.userImageTexture.height), new Vector2(0.5f, 0.5f), 100.0f);
                gameOver.GetComponentInChildren<Text>().text = quickPlaySessionNewRound.winner + " won!";
            }
            else
            {
                winner.sprite = DecodeImage(quickPlaySessionInfoObject.enemyImage, quickPlaySessionInfoObject.enemyImageScale);
                gameOver.GetComponentInChildren<Text>().text = quickPlaySessionNewRound.winner + " won!";
            }
        }
        else
        {
            ClientTCP.Send_RequestQuickPlayNewRound(roomID);
        }
    }

    private void OnQuickGameData(QuickPlaySessionData quickPlaySessionData)
    {
        quickPlaySessionDataObject = quickPlaySessionData;

        Debug.Log("Working ?");

        if (newHand)
        {
            newHand = false;
            ResetCards();
        }

        if (!quickPlaySessionData.myTurn)
        {
            Debug.Log("OPPONENT's TURN");
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

            foreach  (PlayableCard card in myHand)
            {
                card.GetComponent<Button>().interactable = false;
            }
        }
        else
        {
            Debug.Log("MyTurn");
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
            }

            if (quickPlaySessionData.firstHand)
            {
                foreach (PlayableCard card in myHand)
                {
                    card.GetComponent<Button>().interactable = true;
                }
            }
            else
            {
                for (int i = 0; i < 3; i++)
                {
                    myHand[i].GetComponent<Button>().interactable = true;
                }
            }
        }

        for (int i = 0; i < 3; i++)
        {
            if (quickPlaySessionDataObject.myHandStr[i] != "")
            {
                myCombinations[i].SetActive(true);
                string combination = quickPlaySessionDataObject.myHandStr[i].Split('.')[0];
                int royalty = 0;
                if (quickPlaySessionDataObject.myHandStr[i][quickPlaySessionDataObject.myHandStr[i].Length - 1] != '.')
                {
                    royalty = int.Parse(quickPlaySessionDataObject.myHandStr[i].Split('.')[1]);
                    myRoyalties[i].SetActive(true);
                    myRoyalties[i].GetComponentInChildren<Text>().text = royalty.ToString();

                }
                myCombinations[i].GetComponentInChildren<Text>().text = combination;
            }
        }

        for (int i = 0; i < 3; i++)
        {
            if (quickPlaySessionDataObject.oppHandStr[i] != "")
            {
                oppCombinations[i].SetActive(true);
                string combination = quickPlaySessionDataObject.oppHandStr[i].Split('.')[0];
                int royalty = 0;
                if (quickPlaySessionDataObject.oppHandStr[i][quickPlaySessionDataObject.oppHandStr[i].Length - 1] != '.')
                {
                    royalty = int.Parse(quickPlaySessionDataObject.oppHandStr[i].Split('.')[1]);
                    oppRoyalties[i].SetActive(true);
                    oppRoyalties[i].GetComponentInChildren<Text>().text = royalty.ToString();

                }
                oppCombinations[i].GetComponentInChildren<Text>().text = combination;
            }
        }

        Confirm.interactable = false;
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

        if (myDealerCard.sprite != None)
        {
            myDealerCard.sprite = None;
            oppDealerCard.sprite = None;
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
