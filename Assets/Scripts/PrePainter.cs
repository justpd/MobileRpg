using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public class PrePainter : MonoBehaviour {

	Texture2D texture;

	public int scale;
    private byte[] bytes;
    private string b64str;

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
		FromBase64Image(ToBase64Image());
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
    
    public string ToBase64Image()
    {

        bytes = texture.EncodeToPNG();

        string b64str = System.Convert.ToBase64String(bytes);
        return b64str;

    }

    public void FromBase64Image(string b64str)
    {
        byte[] b64_bytes = System.Convert.FromBase64String(b64str);
        texture.LoadImage(b64_bytes);
        GetComponent<Image>().sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
    }

}
