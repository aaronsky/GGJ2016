using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Subway : MonoBehaviour {

    private Character player;
    private int ticksToNextStop;
    private int ticksPassed;

    public List<string> stops;
    public List<int> ticks;
    private int currentStop;

    private int direction;
    private bool stopped = false;

	// Use this for initialization
	void Start () {
        var go = GameObject.Find("Character");
        player = go.GetComponent<Character>();
        player.Sit(5, -2);
        direction = StateManager.SubwayDirection;
        if (direction > 0)
        {
            ticksToNextStop = ticks[0];
            currentStop = -1;
        }
        else
        {
            ticksToNextStop = ticks[ticks.Count - 1];
            currentStop = stops.Count;
        }
        Timer.Subscribe(SubwayStop);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SubwayStop(int hour, int minute)
    {
        if (!stopped)
        {
            ticksToNextStop--;
            if (ticksToNextStop == 0)
            {
                stopped = true;
                currentStop+= direction;
                if (currentStop < stops.Count && currentStop > -1)
                {
                    ticksToNextStop = ticks[currentStop + 1];
                    QueueStop();
                }
                else
                {
                    ExitSubway();
                }
            }
        }
        else
        {
            stopped = false;
        }
    }

    private void QueueStop()
    {
        Debug.Log(stops[currentStop]);
    }

    public void ExitSubway()
    {
        //arrived at apartment
        if (currentStop == -1)
        {
            StateManager.SaveState();
            Application.LoadLevel("Main");
        }
        //arrived at office
        else if (currentStop == stops.Count)
        {
            StateManager.SaveState();
            Application.LoadLevel("Third");
        }
        //getting off at stop
        else
        {
            Debug.Log(stops[currentStop]);
        }
    }
}
