using UnityEngine;
using System.Collections;

public class Office : MonoBehaviour {

    public Character player;
    private float destination;
    private float chairPosition;

	// Use this for initialization
	void Start () {
        Timer.Subscribe(Leave, 17, 0);
        var go = GameObject.Find("Character");
        player = go.GetComponent<Character>();
        destination = 0;
	}
	
	// Update is called once per frame
	void Update () {

        var xpos = player.transform.position.x;
        var move = player.moveSpeed * Time.deltaTime;
        //player is within move distance of destination
        if (Mathf.Abs(xpos - destination) < move)
        {
            var pos = player.transform.position;
            pos.x = destination;
            player.transform.position = pos;
            /*if (destination == chairPosition)
            {
                player.Sit(chairPosition + 0.5f, GameObject.Find("Chair").transform.position.y + 1);
            }*/
        }
        //player is to the left of destination
        else if (xpos < destination - move)
        {
            player.MoveRight();
        }
        //player is to the right of destination
        else
        {
            player.MoveLeft();
        }

        if (player.transform.position.x == -10)
        {
            StateManager.SubwayDirection = -1;
            StateManager.SaveState();
            Application.LoadLevel("Second");
        }
    }

    public void Leave(int hours, int minutes)
    {
        destination = -10;
        player.Stand();
        player.inputEnabled = false;
    }
}
