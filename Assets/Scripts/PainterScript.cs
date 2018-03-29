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
			canvas.Draw(x, y, curColor);
			//Debug.Log("huy1");
		}

		




/*		Vector3[] corners = new Vector3[4];
		image.rectTransform.GetWorldCorners(corners);
		Rect newRect = new Rect(corners[0], corners[2]-corners[0]);
		Debug.Log("newRect pixel coordinates: " + Camera.main.WorldToScreenPoint(corners[0]));
		ЭТО ПРИДУМАЛ ЕБАНЫЙ ДАУН СУКА Из-зА НЕГО МЫ ПРОЕБАЛИ 20 МИНУТ ПРИМЕРНО НАХУЙ
		*/

		if (Input.GetMouseButtonDown(0)) 
		{
			drawActive = true;
		} 
		else if (Input.GetMouseButtonUp(0)) 
		{
			drawActive = false;
		} 
		
	}
}
