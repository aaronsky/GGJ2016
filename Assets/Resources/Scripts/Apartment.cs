using UnityEngine;
using System.Collections;

public class Apartment : MonoBehaviour {

    public bool playerHasMoved = false;
    private Character player;
    private float destination;
    private float chairPosition;
    
    // Use this for initialization
	void Start () {
        Timer.Subscribe(LeaveOnTime, 7, 30);
        Timer.Subscribe(LeaveLastMinute, 8, 0);
        var go = GameObject.Find("Character");
        player = go.GetComponent<Character>();
        chairPosition = GameObject.Find("Chair").transform.position.x;
        destination = chairPosition;

        StateManager.SubwayDirection = 1;
    }
	
	// Update is called once per frame
	void Update () {
        if ((!player.hasBrokenOut || destination == 10) && !player.sitting && !StateManager.textOnScreen)
        {
            var xpos = player.transform.position.x;
            var move = player.moveSpeed * Time.deltaTime;
            //player is within move distance of destination
            if (Mathf.Abs(xpos - destination) < move)
            {
                var pos = player.transform.position;
                pos.x = destination;
                player.transform.position = pos;
                if (destination == chairPosition)
                {
                    player.Sit(chairPosition + 0.5f, GameObject.Find("Chair").transform.position.y + 1);
                }
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
        }
        if (player.transform.position.x == 10)
        {
            StateManager.SaveState();
            Timer.Reset();
            Application.LoadLevel("Second");
        }
    }

    public void LeaveOnTime(int hour, int minute)
    {
        if (!player.hasBrokenOut)
        {
            destination = 10;
            player.Stand();
			SceneManager.GenerateTextBox("Time to go to work.");
        }
    }

    public void LeaveLastMinute(int hour, int minute)
    {
        player.Stand();
        player.inputEnabled = false;
        destination = 10;
		SceneManager.GenerateTextBox("Oh no, you’re gonna be late!");
    }
}
