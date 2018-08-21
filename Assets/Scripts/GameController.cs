using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public List<UserChar> players = new List<UserChar>(6);
    public List<GameObject> hpStatus = new List<GameObject>(6);
    public List<GameObject> tmStatus = new List<GameObject>(6);

    public List<GameObject> logs = new List<GameObject>();

    public int moveNumber = 1;

    private int logsAmount = 0;

    private int fullTurns;
    private bool isMoving = false;

    private bool winner_1 = false;
    private bool winner_2 = false;

    private bool inGame = true;

    private Yoda yoda;
    private Luke luke;
    private Han han;

    private List<Transform> positions = new List<Transform>();

    private GameObject GameLogList;
    private GameObject log;

    private Text MoveNumber;

    private Text Current_skill_2Cd;
    private Text Current_skill_3Cd;
    private Text Target_skill_2Cd;
    private Text Target_skill_3Cd;

    private Text Target_Name_Status;
    private Text Current_Name_Status;

    private Text Target_Player_Name;
    private Text Current_Player_Name;

    private Text Target_Hp_Text;
    private Text Target_Tm_Text;
    private Text Current_Hp_Text;
    private Text Current_Tm_Text;

    private Image Current_Icon_Status;
    private Image Target_Icon_Status;
    private Image Current_Skill_1;
    private Image Current_Skill_2;
    private Image Current_Skill_3;
    private Image Target_Skill_1;
    private Image Target_Skill_2;
    private Image Target_Skill_3;

    private SpriteRenderer Ability_1;
    private SpriteRenderer Ability_2;
    private SpriteRenderer Ability_3;
    private SpriteRenderer Ability_4;
    private SpriteRenderer Ability_5;
    private SpriteRenderer Ability_6;

    private Image Current_Special;
    private Image Target_Special;

    private GameObject Current_Hp_Status;
    private GameObject Current_Tm_Status;

    private GameObject Target_Hp_Status;
    private GameObject Target_Tm_Status;

    public GameObject Buffs_1;
    public GameObject Buffs_2;
    public GameObject Buffs_3;
    public GameObject Buffs_4;
    public GameObject Buffs_5;
    public GameObject Buffs_6;

    public Sprite healthDebuffSprite;
    public Sprite physicalDamageDebuffSprite;
    public Sprite speedDebuffSprite;
    public Sprite potencyDebuffSprite;
    public Sprite tenacityDebuffSprite;
    public Sprite criticalDamageDebuffSprite;
    public Sprite criticalChanceDebuffSprite;
    public Sprite healthStealDebuffSprite;
    public Sprite armorDebuffSprite;
    public Sprite evadeChanceDebuffSprite;
    public Sprite noHealDebuffSprite;
    public Sprite abilityBlockDebuffSprite;
    public Sprite healthBuffSprite;
    public Sprite physicalDamageBuffSprite;
    public Sprite speedBuffSprite;
    public Sprite potencyBuffSprite;
    public Sprite tenacityBuffSprite;
    public Sprite criticalDamageBuffSprite;
    public Sprite criticalChanceBuffSprite;
    public Sprite healthStealBuffSprite;
    public Sprite armorBuffSprite;
    public Sprite evadeChanceBuffSprite;

    public Buff buff;

    private float abilityRefresh = 1.0F;

    private static int SortByTurnMeter(UserChar a, UserChar b)
    {
        return a.Turnmeter.CompareTo(b.Turnmeter);
    }

    void Update()
    {
        SetStatus();
        if (inGame && isMoving)
        {
            if (Input.GetKeyDown(KeyCode.Keypad1) && (GameSession.current && GameSession.target) && GameSession.current.GamePosition > 3)
            {
                StartOfTheTurn();
                GameSession.current.Skill_1();
                switch (GameSession.current.GamePosition)
                {
                    case 1:
                        Ability_1.sprite = GameSession.current.Skill_1_Sprite;
                        StartCoroutine(ResetAbility(Ability_1, abilityRefresh));
                        break;
                    case 2:
                        Ability_2.sprite = GameSession.current.Skill_1_Sprite;
                        StartCoroutine(ResetAbility(Ability_2, abilityRefresh));
                        break;
                    case 3:
                        Ability_3.sprite = GameSession.current.Skill_1_Sprite;
                        StartCoroutine(ResetAbility(Ability_3, abilityRefresh));
                        break;
                    case 4:
                        Ability_4.sprite = GameSession.current.Skill_1_Sprite;
                        StartCoroutine(ResetAbility(Ability_4, abilityRefresh));
                        break;
                    case 5:
                        Ability_5.sprite = GameSession.current.Skill_1_Sprite;
                        StartCoroutine(ResetAbility(Ability_5, abilityRefresh));
                        break;
                    case 6:
                        Ability_6.sprite = GameSession.current.Skill_1_Sprite;
                        StartCoroutine(ResetAbility(Ability_6, abilityRefresh));
                        break;
                }
                EndOfTheTurn();
            }
            else if (Input.GetKeyDown(KeyCode.Keypad2) && (GameSession.current && GameSession.target) && GameSession.current.skill_2CD == 0 && !GameSession.current.abilityBlockDebuffActive && GameSession.current.GamePosition > 3)
            {
                StartOfTheTurn();
                GameSession.current.Skill_2();
                switch (GameSession.current.GamePosition)
                {
                    case 1:
                        Ability_1.sprite = GameSession.current.Skill_2_Sprite;
                        StartCoroutine(ResetAbility(Ability_1, abilityRefresh));
                        break;
                    case 2:
                        Ability_2.sprite = GameSession.current.Skill_2_Sprite;
                        StartCoroutine(ResetAbility(Ability_2, abilityRefresh));
                        break;
                    case 3:
                        Ability_3.sprite = GameSession.current.Skill_2_Sprite;
                        StartCoroutine(ResetAbility(Ability_3, abilityRefresh));
                        break;
                    case 4:
                        Ability_4.sprite = GameSession.current.Skill_2_Sprite;
                        StartCoroutine(ResetAbility(Ability_4, abilityRefresh));
                        break;
                    case 5:
                        Ability_5.sprite = GameSession.current.Skill_2_Sprite;
                        StartCoroutine(ResetAbility(Ability_5, abilityRefresh));
                        break;
                    case 6:
                        Ability_6.sprite = GameSession.current.Skill_2_Sprite;
                        StartCoroutine(ResetAbility(Ability_6, abilityRefresh));
                        break;
                }
                EndOfTheTurn();
            }
            else if (Input.GetKeyDown(KeyCode.Keypad3) && (GameSession.current && GameSession.target) && GameSession.current.isThirdSkill && GameSession.current.skill_3CD == 0 && !GameSession.current.abilityBlockDebuffActive && GameSession.current.GamePosition > 3)
            {
                StartOfTheTurn();
                GameSession.current.Skill_3();
                switch (GameSession.current.GamePosition)
                {
                    case 1:
                        Ability_1.sprite = GameSession.current.Skill_3_Sprite;
                        StartCoroutine(ResetAbility(Ability_1, abilityRefresh));
                        break;
                    case 2:
                        Ability_2.sprite = GameSession.current.Skill_3_Sprite;
                        StartCoroutine(ResetAbility(Ability_2, abilityRefresh));
                        break;
                    case 3:
                        Ability_3.sprite = GameSession.current.Skill_3_Sprite;
                        StartCoroutine(ResetAbility(Ability_3, abilityRefresh));
                        break;
                    case 4:
                        Ability_4.sprite = GameSession.current.Skill_3_Sprite;
                        StartCoroutine(ResetAbility(Ability_4, abilityRefresh));
                        break;
                    case 5:
                        Ability_5.sprite = GameSession.current.Skill_3_Sprite;
                        StartCoroutine(ResetAbility(Ability_5, abilityRefresh));
                        break;
                    case 6:
                        Ability_6.sprite = GameSession.current.Skill_3_Sprite;
                        StartCoroutine(ResetAbility(Ability_6, abilityRefresh));
                        break;
                }
                EndOfTheTurn();
                Invoke("ResetAbility", 3.0F);
            }

            else if (Input.GetKeyDown(KeyCode.Alpha1) && (GameSession.current && GameSession.target) && GameSession.current.GamePosition <= 3)
            {
                StartOfTheTurn();
                GameSession.current.Skill_1();
                switch (GameSession.current.GamePosition)
                {
                    case 1:
                        Ability_1.sprite = GameSession.current.Skill_1_Sprite;
                        StartCoroutine(ResetAbility(Ability_1, abilityRefresh));
                        break;
                    case 2:
                        Ability_2.sprite = GameSession.current.Skill_1_Sprite;
                        StartCoroutine(ResetAbility(Ability_2, abilityRefresh));
                        break;
                    case 3:
                        Ability_3.sprite = GameSession.current.Skill_1_Sprite;
                        StartCoroutine(ResetAbility(Ability_3, abilityRefresh));
                        break;
                    case 4:
                        Ability_4.sprite = GameSession.current.Skill_1_Sprite;
                        StartCoroutine(ResetAbility(Ability_4, abilityRefresh));
                        break;
                    case 5:
                        Ability_5.sprite = GameSession.current.Skill_1_Sprite;
                        StartCoroutine(ResetAbility(Ability_5, abilityRefresh));
                        break;
                    case 6:
                        Ability_6.sprite = GameSession.current.Skill_1_Sprite;
                        StartCoroutine(ResetAbility(Ability_6, abilityRefresh));
                        break;
                }
                EndOfTheTurn();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2) && (GameSession.current && GameSession.target) && GameSession.current.skill_2CD == 0 && !GameSession.current.abilityBlockDebuffActive && GameSession.current.GamePosition <= 3)
            {
                StartOfTheTurn();
                GameSession.current.Skill_2();
                switch (GameSession.current.GamePosition)
                {
                    case 1:
                        Ability_1.sprite = GameSession.current.Skill_2_Sprite;
                        StartCoroutine(ResetAbility(Ability_1, abilityRefresh));
                        break;
                    case 2:
                        Ability_2.sprite = GameSession.current.Skill_2_Sprite;
                        StartCoroutine(ResetAbility(Ability_2, abilityRefresh));
                        break;
                    case 3:
                        Ability_3.sprite = GameSession.current.Skill_2_Sprite;
                        StartCoroutine(ResetAbility(Ability_3, abilityRefresh));
                        break;
                    case 4:
                        Ability_4.sprite = GameSession.current.Skill_2_Sprite;
                        StartCoroutine(ResetAbility(Ability_4, abilityRefresh));
                        break;
                    case 5:
                        Ability_5.sprite = GameSession.current.Skill_2_Sprite;
                        StartCoroutine(ResetAbility(Ability_5, abilityRefresh));
                        break;
                    case 6:
                        Ability_6.sprite = GameSession.current.Skill_2_Sprite;
                        StartCoroutine(ResetAbility(Ability_6, abilityRefresh));
                        break;
                }
                EndOfTheTurn();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3) && (GameSession.current && GameSession.target) && GameSession.current.isThirdSkill && GameSession.current.skill_3CD == 0 && !GameSession.current.abilityBlockDebuffActive && GameSession.current.GamePosition <= 3)
            {
                StartOfTheTurn();
                GameSession.current.Skill_3();
                switch (GameSession.current.GamePosition)
                {
                    case 1:
                        Ability_1.sprite = GameSession.current.Skill_3_Sprite;
                        StartCoroutine(ResetAbility(Ability_1, abilityRefresh));
                        break;
                    case 2:
                        Ability_2.sprite = GameSession.current.Skill_3_Sprite;
                        StartCoroutine(ResetAbility(Ability_2, abilityRefresh));
                        break;
                    case 3:
                        Ability_3.sprite = GameSession.current.Skill_3_Sprite;
                        StartCoroutine(ResetAbility(Ability_3, abilityRefresh));
                        break;
                    case 4:
                        Ability_4.sprite = GameSession.current.Skill_3_Sprite;
                        StartCoroutine(ResetAbility(Ability_4, abilityRefresh));
                        break;
                    case 5:
                        Ability_5.sprite = GameSession.current.Skill_3_Sprite;
                        StartCoroutine(ResetAbility(Ability_5, abilityRefresh));
                        break;
                    case 6:
                        Ability_6.sprite = GameSession.current.Skill_3_Sprite;
                        StartCoroutine(ResetAbility(Ability_6, abilityRefresh));
                        break;
                }
                EndOfTheTurn();
                Invoke("ResetAbility", 3.0F);
            }

            else if (Input.GetKeyDown(KeyCode.Tab) && (GameSession.current && GameSession.target) && GameSession.current.GamePosition <= 3)
            {
                GameSession.current.ChangeTarget();
            }
            else if (Input.GetKeyDown(KeyCode.KeypadEnter) && (GameSession.current && GameSession.target) && GameSession.current.GamePosition > 3)
            {
                GameSession.current.ChangeTarget();
            }
        }
    }

    IEnumerator ResetAbility(SpriteRenderer ability, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        ability.sprite = null;
    }

    void EndOfTheTurn()
    {
        GameSession.current.CheckBuffs();
        GameSession.current.Turnmeter = GameSession.current.speed / 10;
        isMoving = false;
        fullTurns = 0;
        if (GameSession.current.SideLeft)
            GameSession.lastTarget_1 = GameSession.target;
        else
            GameSession.lastTarget_2 = GameSession.target;
        foreach (UserChar player in players)
        {
            player.UpdateStats();
        }
        if (GameSession.target.tauntActive && GameSession.target.health <= 0)
        {
            GameSession.target.taunt = 0;
            if (GameSession.target.tauntActive)
            {
                GameSession.target.tauntActive = false;
                if (GameSession.target.SideLeft)
                    GameSession.tauntLeft -= 1;
                else
                    GameSession.tauntRight -= 1;
            }
        }
        GameSession.current = null;
        GameSession.target = null;
        Log("Turn №" + moveNumber + ":", new Color(255, 255, 255, 255));
        moveNumber += 1;
        ChangeStatus();
        Check();
    }

    void StartOfTheTurn()
    {
        GameSession.current.ResetCD();
        GameSession.current.StartCheckBuffs();
    }

    public void Log(string text, Color32 color)
    {
        Vector3 position = GameLogList.transform.position;
        position.y -= 5F;
        foreach (GameObject logString in logs)
        {
            logString.transform.position = new Vector3(logString.transform.position.x, logString.transform.position.y - 30, logString.transform.position.z);
        }
        GameObject logObject = Instantiate(log, position, new Quaternion(0, 0, 0, 1), GameLogList.transform) as GameObject;
        logObject.GetComponent<Text>().text = text;
        logObject.GetComponent<Text>().color = color;
        logs.Add(logObject);
        logsAmount++;
        GameLogList.GetComponent<RectTransform>().sizeDelta = new Vector2(150, GameLogList.GetComponent<RectTransform>().rect.height + 30);
    }

    void SetStatus()
    {
        if (GameSession.current.SideLeft)
        {
            Current_skill_2Cd.text = GameSession.current.skill_2CD + "";
            Current_skill_3Cd.text = GameSession.current.skill_3CD + "";
            Target_skill_2Cd.text = GameSession.target.skill_2CD + "";
            Target_skill_3Cd.text = GameSession.target.skill_3CD + "";

            Current_Name_Status.text = GameSession.current.FullName;
            Target_Name_Status.text = GameSession.target.FullName;

            Current_Icon_Status.sprite = GameSession.current.SpriteIcon;
            Target_Icon_Status.sprite = GameSession.target.SpriteIcon;

            Current_Skill_1.sprite = GameSession.current.Skill_1_Sprite;
            Current_Skill_2.sprite = GameSession.current.Skill_2_Sprite;
            Current_Skill_3.sprite = GameSession.current.Skill_3_Sprite;
            Current_Special.sprite = GameSession.current.Special_Sprite;

            Target_Skill_1.sprite = GameSession.target.Skill_1_Sprite;
            Target_Skill_2.sprite = GameSession.target.Skill_2_Sprite;
            Target_Skill_3.sprite = GameSession.target.Skill_3_Sprite;
            Target_Special.sprite = GameSession.target.Special_Sprite;

            Target_Player_Name.text = (GameSession.target.SideLeft ? "UserChar №1" : "UserChar №2");
            Current_Player_Name.text = (GameSession.current.SideLeft ? "UserChar №1" : "UserChar №2");

            if (GameSession.current.skill_2CD == 0)
            {
                Current_skill_2Cd.text = "";
                Current_Skill_2.color = new Color32(255, 255, 255, 255);
            }
            else
            {
                Current_Skill_2.color = new Color32(82, 82, 82, 255);
            }

            if (GameSession.current.skill_3CD == 0)
            {
                Current_skill_3Cd.text = "";
                Current_Skill_3.color = new Color32(255, 255, 255, 255);
            }
            else
            {
                Current_Skill_3.color = new Color32(82, 82, 82, 255);
            }

            if (GameSession.target.skill_2CD == 0)
            {
                Target_skill_2Cd.text = "";
                Target_Skill_2.color = new Color32(255, 255, 255, 255);
            }
            else
            {
                Target_Skill_2.color = new Color32(82, 82, 82, 255);
            }

            if (GameSession.target.skill_3CD == 0)
            {
                Target_skill_3Cd.text = "";
                Target_Skill_3.color = new Color32(255, 255, 255, 255);
            }
            else
            {
                Target_Skill_3.color = new Color32(82, 82, 82, 255);
            }

            if (GameSession.current.abilityBlockDebuffActive)
            {
                Current_Skill_2.sprite = abilityBlockDebuffSprite;
                Current_Skill_3.sprite = abilityBlockDebuffSprite;
            }
            else
            {
                Current_Skill_2.sprite = GameSession.current.Skill_2_Sprite;
                Current_Skill_3.sprite = GameSession.current.Skill_3_Sprite;
            }

            if (GameSession.target.abilityBlockDebuffActive)
            {
                Target_Skill_2.sprite = abilityBlockDebuffSprite;
                Target_Skill_3.sprite = abilityBlockDebuffSprite;
            }
            else
            {
                Target_Skill_2.sprite = GameSession.target.Skill_2_Sprite;
                Target_Skill_3.sprite = GameSession.target.Skill_3_Sprite;
            }

            if (!GameSession.current.isThirdSkill)
            {
                Current_Skill_3.sprite = abilityBlockDebuffSprite;
                Current_Skill_3.color = new Color32(52, 52, 52, 255);
            }

            if (!GameSession.target.isThirdSkill)
            {
                Target_Skill_3.sprite = abilityBlockDebuffSprite;
                Target_Skill_3.color = new Color32(52, 52, 52, 255);
            }

            Current_Hp_Status.transform.localScale = new Vector3(((float)GameSession.current.health / (float)GameSession.current.Health) * 1.4F, 1.4F, 1F);
            Current_Tm_Status.transform.localScale = new Vector3(GameSession.current.Turnmeter / 100 * 1.4F, 1.4F, 1F);

            Target_Hp_Status.transform.localScale = new Vector3(((float)GameSession.target.health / (float)GameSession.target.Health) * 1.4F, 1.4F, 1F);
            Target_Tm_Status.transform.localScale = new Vector3(GameSession.target.Turnmeter / 100 * 1.4F, 1.4F, 1F);

            Target_Hp_Text.text = GameSession.target.health + "";
            Target_Tm_Text.text = GameSession.target.Turnmeter + "%";
            Current_Hp_Text.text = GameSession.current.health + "";
            Current_Tm_Text.text = GameSession.current.Turnmeter + "%";
        }
        else
        {
            Target_skill_2Cd.text = GameSession.current.skill_2CD + "";
            Target_skill_3Cd.text = GameSession.current.skill_3CD + "";
            Current_skill_2Cd.text = GameSession.target.skill_2CD + "";
            Current_skill_3Cd.text = GameSession.target.skill_3CD + "";

            Target_Name_Status.text = GameSession.current.FullName;
            Current_Name_Status.text = GameSession.target.FullName;

            Target_Icon_Status.sprite = GameSession.current.SpriteIcon;
            Current_Icon_Status.sprite = GameSession.target.SpriteIcon;

            Target_Skill_1.sprite = GameSession.current.Skill_1_Sprite;
            Target_Skill_2.sprite = GameSession.current.Skill_2_Sprite;
            Target_Skill_3.sprite = GameSession.current.Skill_3_Sprite;
            Target_Special.sprite = GameSession.current.Special_Sprite;

            Current_Skill_1.sprite = GameSession.target.Skill_1_Sprite;
            Current_Skill_2.sprite = GameSession.target.Skill_2_Sprite;
            Current_Skill_3.sprite = GameSession.target.Skill_3_Sprite;
            Current_Special.sprite = GameSession.target.Special_Sprite;

            Current_Player_Name.text = (GameSession.target.SideLeft ? "UserChar №1" : "UserChar №2");
            Target_Player_Name.text = (GameSession.current.SideLeft ? "UserChar №1" : "UserChar №2");

            if (GameSession.current.skill_2CD == 0)
            {
                Target_skill_2Cd.text = "";
                Target_Skill_2.color = new Color32(255, 255, 255, 255);
            }
            else
            {
                Target_Skill_2.color = new Color32(82, 82, 82, 255);
            }

            if (GameSession.current.skill_3CD == 0)
            {
                Target_skill_3Cd.text = "";
                Target_Skill_3.color = new Color32(255, 255, 255, 255);
            }
            else
            {
                Target_Skill_3.color = new Color32(82, 82, 82, 255);
            }

            if (GameSession.target.skill_2CD == 0)
            {
                Current_skill_2Cd.text = "";
                Current_Skill_2.color = new Color32(255, 255, 255, 255);
            }
            else
            {
                Current_Skill_2.color = new Color32(82, 82, 82, 255);
            }

            if (GameSession.target.skill_3CD == 0)
            {
                Current_skill_3Cd.text = "";
                Current_Skill_3.color = new Color32(255, 255, 255, 255);
            }
            else
            {
                Current_Skill_3.color = new Color32(82, 82, 82, 255);
            }

            if (GameSession.current.abilityBlockDebuffActive)
            {
                Target_Skill_2.sprite = abilityBlockDebuffSprite;
                Target_Skill_3.sprite = abilityBlockDebuffSprite;
            }
            else
            {
                Target_Skill_2.sprite = GameSession.current.Skill_2_Sprite;
                Target_Skill_3.sprite = GameSession.current.Skill_3_Sprite;
            }

            if (GameSession.target.abilityBlockDebuffActive)
            {
                Current_Skill_2.sprite = abilityBlockDebuffSprite;
                Current_Skill_3.sprite = abilityBlockDebuffSprite;
            }
            else
            {
                Current_Skill_2.sprite = GameSession.target.Skill_2_Sprite;
                Current_Skill_3.sprite = GameSession.target.Skill_3_Sprite;
            }

            if (!GameSession.current.isThirdSkill)
            {
                Target_Skill_3.sprite = abilityBlockDebuffSprite;
                Target_Skill_3.color = new Color32(52, 52, 52, 255);
            }

            if (!GameSession.target.isThirdSkill)
            {
                Current_Skill_3.sprite = abilityBlockDebuffSprite;
                Current_Skill_3.color = new Color32(52, 52, 52, 255);
            }

            Target_Hp_Status.transform.localScale = new Vector3(((float)GameSession.current.health / (float)GameSession.current.Health) * 1.4F, 1.4F, 1F);
            Target_Tm_Status.transform.localScale = new Vector3(GameSession.current.Turnmeter / 100 * 1.4F, 1.4F, 1F);

            Current_Hp_Status.transform.localScale = new Vector3(((float)GameSession.target.health / (float)GameSession.target.Health) * 1.4F, 1.4F, 1F);
            Current_Tm_Status.transform.localScale = new Vector3(GameSession.target.Turnmeter / 100 * 1.4F, 1.4F, 1F);

            Current_Hp_Text.text = GameSession.target.health + "";
            Current_Tm_Text.text = GameSession.target.Turnmeter + "%";
            Target_Hp_Text.text = GameSession.current.health + "";
            Target_Tm_Text.text = GameSession.current.Turnmeter + "%";
        }

    }

    void Awake()
    {
        GameSession.gameController = this;
        log = Resources.Load<GameObject>("Log");
        buff = Resources.Load<Buff>("Buff");

        healthDebuffSprite = Resources.Load<Sprite>("Sprites/healthDebuff");
        physicalDamageDebuffSprite = Resources.Load<Sprite>("Sprites/physicalDamageDebuff");
        speedDebuffSprite = Resources.Load<Sprite>("Sprites/speedDebuff");
        potencyDebuffSprite = Resources.Load<Sprite>("Sprites/potencyDebuff");
        tenacityDebuffSprite = Resources.Load<Sprite>("Sprites/tenacityDebuff");
        criticalDamageDebuffSprite = Resources.Load<Sprite>("Sprites/criticalDamageDebuff");
        criticalChanceDebuffSprite = Resources.Load<Sprite>("Sprites/criticalChanceDebuff");
        healthStealDebuffSprite = Resources.Load<Sprite>("Sprites/healthStealDebuff");
        armorDebuffSprite = Resources.Load<Sprite>("Sprites/armorDebuff");
        evadeChanceDebuffSprite = Resources.Load<Sprite>("Sprites/evadeChanceDebuff");
        noHealDebuffSprite = Resources.Load<Sprite>("Sprites/noHealDebuff");
        abilityBlockDebuffSprite = Resources.Load<Sprite>("Sprites/abilityBlockDebuff");
        healthBuffSprite = Resources.Load<Sprite>("Sprites/healthBuff");
        physicalDamageBuffSprite = Resources.Load<Sprite>("Sprites/physicalDamageBuff");
        speedBuffSprite = Resources.Load<Sprite>("Sprites/speedBuff");
        potencyBuffSprite = Resources.Load<Sprite>("Sprites/potencyBuff");
        tenacityBuffSprite = Resources.Load<Sprite>("Sprites/tenacityBuff");
        criticalDamageBuffSprite = Resources.Load<Sprite>("Sprites/criticalDamageBuff");
        criticalChanceBuffSprite = Resources.Load<Sprite>("Sprites/criticalChanceBuff");
        healthStealBuffSprite = Resources.Load<Sprite>("Sprites/healthStealBuff");
        armorBuffSprite = Resources.Load<Sprite>("Sprites/armorBuff");
        evadeChanceBuffSprite = Resources.Load<Sprite>("Sprites/evadeChanceBuff");

        GameLogList = GameObject.Find("GameLogList");
        Current_skill_2Cd = GameObject.Find("Current_Skill_2CD").GetComponent<Text>();
        Current_skill_3Cd = GameObject.Find("Current_Skill_3CD").GetComponent<Text>();
        Target_skill_2Cd = GameObject.Find("Target_Skill_2CD").GetComponent<Text>();
        Target_skill_3Cd = GameObject.Find("Target_Skill_3CD").GetComponent<Text>();
        Current_Name_Status = GameObject.Find("Current_Name_Status").GetComponent<Text>();
        Target_Name_Status = GameObject.Find("Target_Name_Status").GetComponent<Text>();

        Target_Hp_Text = GameObject.Find("Target_HP_Text").GetComponent<Text>();
        Target_Tm_Text = GameObject.Find("Target_TM_Text").GetComponent<Text>();
        Current_Hp_Text = GameObject.Find("Current_HP_Text").GetComponent<Text>();
        Current_Tm_Text = GameObject.Find("Current_TM_Text").GetComponent<Text>();

        Target_Player_Name = GameObject.Find("Target_Player_Name").GetComponent<Text>();
        Current_Player_Name = GameObject.Find("Current_Player_Name").GetComponent<Text>();

        MoveNumber = GameObject.Find("MoveNumber").GetComponent<Text>();

        Current_Icon_Status = GameObject.Find("Current_Icon_Status").GetComponent<Image>();
        Target_Icon_Status = GameObject.Find("Target_Icon_Status").GetComponent<Image>();
        Current_Skill_1 = GameObject.Find("Current_Skill_1").GetComponent<Image>();
        Current_Skill_2 = GameObject.Find("Current_Skill_2").GetComponent<Image>();
        Current_Skill_3 = GameObject.Find("Current_Skill_3").GetComponent<Image>();
        Current_Special = GameObject.Find("Current_Special").GetComponent<Image>();

        Target_Skill_1 = GameObject.Find("Target_Skill_1").GetComponent<Image>();
        Target_Skill_2 = GameObject.Find("Target_Skill_2").GetComponent<Image>();
        Target_Skill_3 = GameObject.Find("Target_Skill_3").GetComponent<Image>();
        Target_Special = GameObject.Find("Target_Special").GetComponent<Image>();

        Ability_1 = GameObject.Find("Ability_1").GetComponentInChildren<SpriteRenderer>();
        Ability_2 = GameObject.Find("Ability_2").GetComponentInChildren<SpriteRenderer>();
        Ability_3 = GameObject.Find("Ability_3").GetComponentInChildren<SpriteRenderer>();
        Ability_4 = GameObject.Find("Ability_4").GetComponentInChildren<SpriteRenderer>();
        Ability_5 = GameObject.Find("Ability_5").GetComponentInChildren<SpriteRenderer>();
        Ability_6 = GameObject.Find("Ability_6").GetComponentInChildren<SpriteRenderer>();

        Current_Hp_Status = GameObject.Find("Current_HP_Status");
        Current_Tm_Status = GameObject.Find("Current_TM_Status");
        Target_Hp_Status = GameObject.Find("Target_HP_Status");
        Target_Tm_Status = GameObject.Find("Target_TM_Status");

        Buffs_1 = GameObject.Find("Buffs_1");
        Buffs_2 = GameObject.Find("Buffs_2");
        Buffs_3 = GameObject.Find("Buffs_3");
        Buffs_4 = GameObject.Find("Buffs_4");
        Buffs_5 = GameObject.Find("Buffs_5");
        Buffs_6 = GameObject.Find("Buffs_6");

        for (int i = 0; i < 6; i++)
        {
            positions.Add(null);
            hpStatus.Add(null);
            tmStatus.Add(null);
            GameSession.players.Add(null);
        }

        for (int i = 0; i < 6; i++)
        {
            hpStatus[i] = GameObject.Find("Health_" + (i + 1));
            tmStatus[i] = GameObject.Find("TurnMeter_" + (i + 1));
        }


        GameSession.playerList[0] = "Yoda";
        GameSession.playerList[1] = "Luke";
        GameSession.playerList[2] = "Han";
        GameSession.playerList[3] = "Luke";
        GameSession.playerList[4] = "Han";
        GameSession.playerList[5] = "Yoda";

        positions[0] = GameObject.Find("UserChar (1)").transform;
        positions[1] = GameObject.Find("UserChar (2)").transform;
        positions[2] = GameObject.Find("UserChar (3)").transform;
        positions[3] = GameObject.Find("UserChar (4)").transform;
        positions[4] = GameObject.Find("UserChar (5)").transform;
        positions[5] = GameObject.Find("UserChar (6)").transform;

        yoda = Resources.Load<Yoda>("Yoda");
        luke = Resources.Load<Luke>("Luke");
        han = Resources.Load<Han>("Han");

        for (int i = 0; i < 6; i++)
        {
            switch (GameSession.playerList[i])
            {
                case "Yoda":
                    if (i >= 3)
                    {
                        GameSession.players[i] = Instantiate(yoda, positions[i].transform.position, new Quaternion(0, 1, 0, 0), positions[i]) as Yoda;
                        GameSession.players[i].SideLeft = false;
                        GameSession.players[i].GamePosition = i + 1;
                    }
                    else
                    {
                        GameSession.players[i] = Instantiate(yoda, positions[i].transform.position, new Quaternion(0, 0, 0, 1), positions[i]) as Yoda;
                        GameSession.players[i].SideLeft = true;
                        GameSession.players[i].GamePosition = i + 1;
                    }
                    break;
                case "Luke":
                    if (i >= 3)
                    {
                        GameSession.players[i] = Instantiate(luke, positions[i].transform.position, new Quaternion(0, 1, 0, 0), positions[i]) as Luke;
                        GameSession.players[i].SideLeft = false;
                        GameSession.players[i].GamePosition = i + 1;
                    }
                    else
                    {
                        GameSession.players[i] = Instantiate(luke, positions[i].transform.position, new Quaternion(0, 0, 0, 1), positions[i]) as Luke;
                        GameSession.players[i].SideLeft = true;
                        GameSession.players[i].GamePosition = i + 1;
                    }
                    break;
                case "Han":
                    if (i >= 3)
                    {
                        GameSession.players[i] = Instantiate(han, positions[i].transform.position, new Quaternion(0, 1, 0, 0), positions[i]) as Han;
                        GameSession.players[i].SideLeft = false;
                        GameSession.players[i].GamePosition = i + 1;
                    }
                    else
                    {
                        GameSession.players[i] = Instantiate(han, positions[i].transform.position, new Quaternion(0, 0, 0, 1), positions[i]) as Han;
                        GameSession.players[i].SideLeft = true;
                        GameSession.players[i].GamePosition = i + 1;
                    }
                    break;
            }
            GameSession.players[i].sprite.sortingOrder = 10;

        }
        GameSession.players.CopyTo(GameSession.positions);
        //Debug.Log(GameSession.positions[4]);
        players = GameSession.players;

        for (int i = 0; i < 6; i++)
        {
            GameSession.players[i].Turnmeter = GameSession.players[i].speed / 10;
        }

        Check();
    }

    void Check()
    {
        if (!GameSession.positions[3].isActive && !GameSession.positions[4].isActive && !GameSession.positions[5].isActive)
        {
            winner_1 = true;
            inGame = false;
        }
        else if (!GameSession.positions[0].isActive && !GameSession.positions[1].isActive && !GameSession.positions[2].isActive)
        {
            winner_2 = true;
            inGame = false;
        }
        else
        {
            winner_1 = false;
            winner_2 = false;
            inGame = true;
        }

        Sort();

        for (int i = 0; i < 6; i++)
        {
            if (players[i].Turnmeter == 100.0F)
            {
                fullTurns += 1;
            }
        }

        if (fullTurns == 0)
        {
            foreach (UserChar player in players)
            {
                if (player.isActive)
                {
                    player.Turnmeter += 100 - players[5].Turnmeter;
                }
            }
            foreach (UserChar player in players)
            {
                //Debug.Log(player.Turnmeter);
            }
        }
        ChangeStatus();
        Move();
    }

    void Move()
    {
        if (!inGame && winner_1)
        {
            Debug.Log("Winner 1");
        }
        else if (!inGame && winner_2)
        {
            Debug.Log("Winner 2");
        }
        GameSession.current = players[5];

        if (players[5].SideLeft)
        {
            if (GameSession.lastTarget_1 && GameSession.lastTarget_1.health != 0 && (GameSession.tauntRight <= 0 | GameSession.lastTarget_1.taunt > 0))
            {
                GameSession.target = GameSession.lastTarget_1;
            }
            else
            {
                if (GameSession.positions[4].isActive && (GameSession.tauntRight <= 0 | GameSession.positions[4].taunt > 0))
                    GameSession.target = GameSession.positions[4];
                else if (GameSession.positions[3].isActive && (GameSession.tauntRight <= 0 | GameSession.positions[3].taunt > 0))
                    GameSession.target = GameSession.positions[3];
                else
                    GameSession.target = GameSession.positions[5];
            }

        }
        else
        {
            if (GameSession.lastTarget_2 && GameSession.lastTarget_2.health != 0 && (GameSession.tauntLeft <= 0 | GameSession.lastTarget_2.taunt > 0))
            {
                GameSession.target = GameSession.lastTarget_2;
            }
            else
            {
                if (GameSession.positions[1].isActive && (GameSession.tauntLeft <= 0 | GameSession.positions[1].taunt > 0))
                    GameSession.target = GameSession.positions[1];
                else if (GameSession.positions[0].isActive && (GameSession.tauntLeft <= 0 | GameSession.positions[0].taunt > 0))
                    GameSession.target = GameSession.positions[0];
                else
                    GameSession.target = GameSession.positions[2];
            }
        }
        MoveNumber.text = "Turn №" + moveNumber;
        isMoving = true;
    }

    void Sort()
    {
        players.Sort(SortByTurnMeter);
    }

    void ChangeStatus()
    {
        for (int i = 0; i < 6; i++)
        {
            hpStatus[i].transform.localScale = new Vector3(((float)GameSession.positions[i].health / (float)GameSession.positions[i].Health) * 1.86F, 0.7F, 1F);
            tmStatus[i].transform.localScale = new Vector3(GameSession.positions[i].Turnmeter / 100 * 1.86F, 0.7F, 1F);

        }
    }

}
