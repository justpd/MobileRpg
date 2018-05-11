using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PainterScript : MonoBehaviour 
{

	public Color curColor;
	public bool drawActive;
	public Image image;
	public int x;
	public int y;
	public PrePainter canvas;

	Vector3 mousePos;
	Vector3 relativePos;

	float posX;
	float posY;

	int mode = 0;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z);
		transform.position = mousePos;

		relativePos = mousePos - image.transform.position;

		posX = relativePos.x / image.rectTransform.sizeDelta.x + 0.5f;
		posY = relativePos.y / image.rectTransform.sizeDelta.y + 0.5f;

		x = (int)Mathf.Ceil(posX * canvas.scale) - 1;
		y = (int)Mathf.Ceil(posY * canvas.scale) - 1;

		if(x >= 0 && x < canvas.scale && y >= 0 && y < canvas.scale && drawActive)
		{
			switch (mode)
			{
				case 0:
					canvas.Draw(x,y,curColor);
					break;
				case 1:
					canvas.Fill(x,y,curColor);
					break;
			}
			//Debug.Log("huy1");
		}

		if (Input.GetMouseButtonDown(0)) 
		{
			drawActive = true;
		} 
		else if (Input.GetMouseButtonUp(0)) 
		{
			drawActive = false;
		} 
		
	}

    public void SetCurrentColor(Image image)
    {
        curColor = image.color;
    }

	public void SetCurrentMode (int mode)
	{
		this.mode = mode;
	}
}
