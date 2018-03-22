using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PicCreator : MonoBehaviour {

    private Color color;
    
    public void SetCurrentColor(Image image)
    {
        color = image.color;
        Debug.Log(color);
    }

    public void Paint(Image pixel)
    {
        pixel.color = color;
        Debug.Log(this);
    }
	void Start () {
		
	}
	
	
	void Update () {
		
	}
}
