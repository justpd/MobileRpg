using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

public class DeckZone : MonoBehaviour
{
    public string value = "";
    public string position = "";

    public Image image;

    public PlayableCard lastCard;

    QuickPlayGameController controller;

    private void Awake()
    {
        image = GetComponent<Image>();
        controller = FindObjectOfType<QuickPlayGameController>();
    }

    public void OnMouseDown()
    {
        if (Data.selectedCard && value == "")
        {
            Data.selectedCard.position = position;
            value = Data.selectedCard.value;
            lastCard = Data.selectedCard;
            controller.DrawCard(value, image);
            Data.selectedCard.image.color = new Color32(255, 255, 255, 255);
            Data.selectedCard.image.sprite = controller.Blank;
            Data.selectedCard = null;

            controller.EnableConfirm();
        }
        else if (!Data.selectedCard && value != "" && lastCard)
        {
            lastCard.position = "";
            controller.DrawCard(value, lastCard.image);
            value = "";
            image.sprite = controller.Blank;
            lastCard = null;
            controller.EnableConfirm();
        }
    }
}