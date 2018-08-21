// cSpell:ignore Behaviour

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : MonoBehaviour {

    public bool isDebuff;
    public string description;
    public float value;
    public string type;
    public int duration;

    private SpriteRenderer sprite;

    private void Awake()
    {

    }

    private void OnMouseDown()
    {
        Debug.Log(duration);
    }

}
