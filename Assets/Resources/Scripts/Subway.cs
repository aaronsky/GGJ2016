using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Subway : MonoBehaviour {

    private Character player;
    private int ticksToNextStop;
    private int ticksPassed;

    private List<string> stops = new List<string>()
    {
        "Main Street",
        "South Street",
        "Park Street"
    };
    private List<string> stopText = new List<string>()
    {
        "You get off at Main Street Station. You see a young woman taking six dogs for a walk. You pet at least one dog. You walk the rest of the way to the office.",
        "You get off at South Street Station. You see a flower shop with an extensive selection of carnations. You buy a carnation, and the shopkeeper assures you it’s a good one. You walk the rest of the way to the office.",
        "You get off at Park Street Station. Coincidentally, there’s a large park here. The sun is shining, the wind is perfect, ducks are in ponds, and children are playing in the grass. You take a pleasant walk to work."
    };
    public static List<bool> haveGottenOff = new List<bool>()
    {
        false,
        false,
        false
    };
    public List<int> ticks;
    private int currentStop;

    private int direction;
    private bool stopped = false;
    private bool gotOffSomewhere = false;

	// Use this for initialization
	void Start () {
        var go = GameObject.Find("Character");
        player = go.GetComponent<Character>();
        player.Sit(6, 1);
        player.Flip();
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
        gotOffSomewhere = false;
    }
	
	// Update is called once per frame
	void Update () {
	    if (gotOffSomewhere && !StateManager.textOnScreen)
        {
            if (!Subway.haveGottenOff[currentStop])
            {
                Subway.haveGottenOff[currentStop] = true;
                player.IncreaseView();
            }
            StateManager.SaveState();
            if (direction > 0)
            {
                Application.LoadLevel("Third");
            }
            else
            {
                Application.LoadLevel("Main");
            }
        }
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
            if (!gotOffSomewhere)
            {
                ticksToNextStop--;
                if (player.transform.position.x >= 10)
                {
                    ExitSubway();
                }
                if (ticksToNextStop == 0)
                {
                    stopped = false;
                    SceneManager.HideText();
                    ticksToNextStop = 7;
                }
            }
        }
    }

    private void QueueStop()
    {
        SceneManager.GenerateTextBox(stops[currentStop]);
        ticksToNextStop = 5;
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
            SceneManager.GenerateTextBox(stopText[currentStop]);
            GameObject.Find("Spotlight").GetComponent<Light>().intensity = 0;
            gotOffSomewhere = true;
        }
    }
}
