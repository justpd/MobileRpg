using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Pixel : MonoBehaviour, IPointerEnterHandler
{

    public PicCreator picCreator;

	// Use this for initialization
	void Awake () {
        picCreator = FindObjectOfType<PicCreator>();
    }
	
	// Update is called once per frame
	public void OnPointerEnter(PointerEventData eventData) {
        Debug.Log(eventData.clickCount);
        if (eventData.clickCount == 2)
        {
            picCreator.Paint(GetComponent<Image>());
            Debug.Log("hui1");
        }
    }

}
