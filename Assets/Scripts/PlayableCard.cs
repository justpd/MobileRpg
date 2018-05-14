using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

public class PlayableCard : MonoBehaviour
{
    public bool selected;

    public string value = "";
    public string position = "";

    public Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void OnMouseDown()
    {
        if (position == "" && value != "")
        {
            if (!Data.selectedCard)
            {
                Data.selectedCard = this;
                image.color = new Color32(255, 255, 255, 100);
                selected = true;
            }
            else if (Data.selectedCard == this)
            {
                Data.selectedCard.image.color = new Color32(255, 255, 255, 255);
                Data.selectedCard.selected = false;
            }
            else if (Data.selectedCard)
            {
                Data.selectedCard.image.color = new Color32(255, 255, 255, 255);
                Data.selectedCard.selected = false;

                Data.selectedCard = this;
                image.color = new Color32(255, 255, 255, 100);
                selected = true;
            }
        }
    }
}