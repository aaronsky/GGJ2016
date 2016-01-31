using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SceneObject : MonoBehaviour {

    public static float maxDistToPlayer;
    public string text;
    public List<string> unlocks;
    public int ID;

    public bool startsActive;

    void Awake()
    {
        var width = transform.localScale.x / GetComponent<SpriteRenderer>().sprite.bounds.size.x;
        maxDistToPlayer = width / 2 + 0.3f;
    }

    // Use this for initialization
    void Start()
    {
        if (!StateManager.cleanLevels.Contains(Application.loadedLevelName))
        {
            if (ID == 1)
            {
                if (!startsActive)
                {
                    gameObject.GetComponent<Renderer>().enabled = false;
                    enabled = false;
                }
            }
            else
                enabled = false;
        }
        
    }

    public void SetData(string imagePath, Vector2 position, string clickText)
    {
        /*spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        Texture2D tex = Resources.Load<Texture2D>(imagePath);
        spriteRenderer.sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);
        transform.position.Set(position.x, position.y, 0);
        text = clickText;*/
    }

    public void Unlock()
    {
        if (!enabled)
        {
            var sceneObjects = GetComponents<SceneObject>();
            bool blockUnlock = false;
            foreach (var so in sceneObjects)
            {
                if (so.GetComponent<Renderer>().enabled && so.ID > ID)
                    blockUnlock = true;

            }
            if (!blockUnlock)
            {
                foreach (var so in sceneObjects)
                {
                    if (so.GetComponent<Renderer>().enabled && so.ID < ID)
                        so.GetComponent<Renderer>().enabled = false;
                }
                enabled = true;
                if (!gameObject.GetComponent<Renderer>().enabled)
                {
                    gameObject.GetComponent<Renderer>().enabled = true;
                }
            }
        }
    }
	
    public void OnMouseDown()
    {
        if (!enabled)
            return;
		GenerateTextBox ("Hello world!");
        var player = GameObject.Find("Character");
        if (player != null)
        {
            if (Vector3.Distance(transform.position, player.transform.position) > maxDistToPlayer)
            {
                //too far
            }
            else
            {
                var controller = player.GetComponent<Character>();
                if (controller != null)
                {
                    controller.TurnAround(true);
                    Debug.Log(text);
                }
                foreach (string unlock in unlocks)
                {
                    var split = unlock.Split('-');
                    GameObject go = GameObject.Find(split[0]);
                    if (go != null)
                    {
                        var sceneObjects = go.GetComponents<SceneObject>();
                        foreach (SceneObject so in sceneObjects)
                        {
                            int identifier = 0;
                            int.TryParse(split[1], out identifier);
                            if (so.ID == identifier)
                                so.Unlock();
                        }
                    }
                }
            }
        }
    }

	public void GenerateTextBox (string s)
	{
		/*var textBox = GameObject.Find("TextBox");
		var writer = textBox.GetComponentInChildren<Writer> ();

		if (textBox != null && writer != null) {
			writer.SetText(s);

			Renderer boxRend = textBox.GetComponent<Renderer> ();
			boxRend.enabled = true;
		
			Renderer textRend = writer.GetComponent<Renderer> ();
			textRend.enabled = true;


		} else {
			Debug.Log("Didn't find TextBox GameObject!");
		}
        */

	}

	// Update is called once per frame
	void Update () {
	
	}
}
