using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DealerScript : MonoBehaviour {

    public Sprite card;

    public void OnRotate()
    {
        GetComponent<Image>().sprite = card;
    }
}
