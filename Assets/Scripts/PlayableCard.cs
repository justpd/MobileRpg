using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

public class PlayableCard : MonoBehaviour
{
    public bool active;
    public bool selected;

    public string value;

    private void OnMouseDown()
    {
        if (Data.selectedCard)
        {
            value = Data.selectedCard.value;
            Data.selectedCard = null;
        }
    }
}