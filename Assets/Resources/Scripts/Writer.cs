using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Writer : MonoBehaviour {

	public string displayText;
	public TextMesh content;
	Queue<string> displays = new Queue<string>();

	// Use this for initialization
	void Start () {
		//displayText = "42";
		//displays = new Queue<string>();
		content = GetComponent<TextMesh>();
		content.text = displayText;
        GetComponent<Renderer>().sortingOrder = 7;
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void SetText (string s) {
        StateManager.textOnScreen = true;
        Timer.clockIsRunning = false;

		string temp = "";

		foreach (char c in s) {
			if(temp.Length < 103)
			{
				if(temp.Length == 51)
					temp += '\n';
					temp += c;
			}
			else
			{
				displays.Enqueue(temp);
				temp = "";

			}
		}

		displays.Enqueue(temp);
		UpdateText (displays.Dequeue());
	}

	public void UpdateText (string display) {
		displayText = display;
		content.text = displayText;
	}

	public void ShowText () {
		//Queue 
	}

	public void OnClick()
	{
		if(displays.Count != 0) 
			UpdateText (displays.Dequeue());
		else
			CloseTextBox ();
	}

	public void CloseTextBox(){
		Renderer[] renderers = gameObject.GetComponentsInParent<Renderer> ();
		
		foreach (Renderer r in renderers)
			r.enabled = false;
        StateManager.textOnScreen = false;
        Timer.clockIsRunning = true;
    }
}
