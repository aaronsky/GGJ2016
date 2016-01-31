using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Area : MonoBehaviour {

    public SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start () {
        StateManager.RestoreState();
	}

    public void Init(string backgroundImage)
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        if (spriteRenderer == null) { 
            spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
            Texture2D tex = Resources.Load<Texture2D>(backgroundImage);
            spriteRenderer.sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);
        }
        
        
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    struct ObjectData
    {
        string image;
        Vector2 position;
        Vector2 size;
        string text;

    }
}
