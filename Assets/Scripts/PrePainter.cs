using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrePainter : MonoBehaviour {

	Texture2D texture;

	public int scale = 8;

	// Use this for initialization
	void Start () {
		//Создаем новую текстуру
		texture = new Texture2D(scale, scale) {
		filterMode = FilterMode.Point
		};

		for (int y = 0; y < texture.height; y++) 
		{
			for (int x = 0; x < texture.width; x++) 
			{
				texture.SetPixel(x, y, Color.white);
			}
		}
		texture.Apply();
		Sprite sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
		GetComponent<Image>().sprite = sprite;

	}

	public void Draw(int x, int y, Color color)
	{
		texture.SetPixel(x, y, color);
		texture.Apply();
		Sprite sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
		GetComponent<Image>().sprite = sprite;
	}
	
	public void Resize(int scale)
	{
		this.scale = scale;
		texture = new Texture2D(scale, scale) {
		filterMode = FilterMode.Point
		};

		for (int y = 0; y < texture.height; y++) 
		{
			for (int x = 0; x < texture.width; x++) 
			{
				texture.SetPixel(x, y, Color.white);
			}
		}
		texture.Apply();
		Sprite sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
		GetComponent<Image>().sprite = sprite;
	}
	
	public void ResetCanvas()
	{
		for (int y = 0; y < texture.height; y++) 
		{
			for (int x = 0; x < texture.width; x++) 
			{
				texture.SetPixel(x, y, Color.white);
			}
		}
		texture.Apply();
		Sprite sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
		GetComponent<Image>().sprite = sprite;
	}
}
