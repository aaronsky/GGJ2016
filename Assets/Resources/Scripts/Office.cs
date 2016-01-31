using UnityEngine;
using System.Collections;

public class Office : MonoBehaviour {

    public Character player;

	// Use this for initialization
	void Start () {
        var go = GameObject.Find("Character");
        player = go.GetComponent<Character>();
	}
	
	// Update is called once per frame
	void Update () {
        if (player.transform.position.x == -10)
        {
            StateManager.SubwayDirection = -1;
            StateManager.SaveState();
            Application.LoadLevel("Second");
        }
    }
}
