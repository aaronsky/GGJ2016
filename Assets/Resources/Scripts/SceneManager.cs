using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.S))
        {
            StateManager.SaveState();
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            Application.LoadLevel("Main");
            StateManager.RestoreState();
        }
	}
}
