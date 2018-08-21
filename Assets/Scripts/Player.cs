using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public string Name;
    public string id;

    public int index;

    public float health = 1;
    public float turnmeter = 1;

    private bool current = false;
    private bool target = false;

    public GameObject controller;

    public GameObject HealthBar;
    public GameObject TurnMeterBar;

    public SpriteRenderer Icon;
    public SpriteRenderer Status;

    public Sprite icon;

    public Sprite skill_1;
    //public Sprite skill_2;
    //public Sprite skill_3;
    //public Sprite special_1;
    //public Sprite special_2;
    //public Sprite special_3;

    private void SetUp(string name, string id)
    {

    }

    private void Awake()
    {
        UpdateBars();
    }

    public void UpdateBars()
    {
        Debug.Log(health + ":");
        Debug.Log((float)health / 100.0f);
        HealthBar.transform.localScale = new Vector3((float)health / 100.0f, 0.8F, 1F);
        TurnMeterBar.transform.localScale = new Vector3((float)turnmeter / 100.0f, 0.4F, 1F);
    }

    public void OnMouseDown()
    {
        controller.SendMessage("SelectTarget", index);
    }

}
