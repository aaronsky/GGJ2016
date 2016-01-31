using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
     
	}

    public static void GenerateTextBox(string s)
    {

        var textBox = GameObject.Find("TextBox");
        var writer = textBox.GetComponentInChildren<Writer>();

        if (textBox != null && writer != null)
        {
            StateManager.textOnScreen = true;
            writer.SetText(s);

            Renderer boxRend = textBox.GetComponent<Renderer>();
            boxRend.enabled = true;

            Renderer textRend = writer.GetComponent<Renderer>();
            textRend.enabled = true;


        }
        else
        {
            Debug.Log("Didn't find TextBox GameObject!");
        }


    }

    public static void HideText()
    {
        var textBox = GameObject.Find("TextBox");
        var writer = textBox.GetComponentInChildren<Writer>();
        writer.CloseTextBox();
    }
}
