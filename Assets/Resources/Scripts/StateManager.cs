using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StateManager : MonoBehaviour {

    public static Dictionary<string, Dictionary<string, bool>> activeList = new Dictionary<string, Dictionary<string, bool>>();
    public static List<string> cleanLevels = new List<string>();
    public static int SubwayDirection;
    public static bool textOnScreen = false;
    public static List<string> queuedUnlocks = new List<string>();
	public static int radius = 13;
	
	public static bool hasBedEnd = false;

    public static bool hasCalledGrandma = false;
    public static bool grandmaEnding = false;

    public static int tardies = 0;
    public static bool isLate = false;

    public static List<bool> unlockedEndings = new List<bool>()
    {
        false,
        false,
        false
    };

	// Use this for initialization
	void Start () {
	    
	}

    public static void Late()
    {
        StateManager.tardies++;
        if (StateManager.tardies >= 3)
        {
            StateManager.isLate = true;
        }
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    /// <summary>
    /// Restores state stored in list to all SceneObjects present in scene
    /// </summary>
    public static void RestoreState()
    {
        if (GameObject.Find("Apartment") != null && StateManager.hasCalledGrandma)
        {
            grandmaEnding = true;
            SceneManager.GenerateTextBox("The phone rings. Your grandmother left you a voicemail. 'It was very nice to hear your voice yesterday. Have a wonderful day and I love you very much.'");
        }
        foreach (string key in activeList.Keys)
        {
            var go = GameObject.Find(key);
            if (go != null)
            {
                var dict = activeList[key];
                var sceneObjects = go.GetComponents<SceneObject>();
                foreach (var so in sceneObjects)
                {
                    if (dict.ContainsKey(so.ID.ToString()))
                    {
                        so.enabled = dict[so.ID.ToString()];
                    }
                }
                if (dict.ContainsKey("Renderer"))
                {
                    go.GetComponent<Renderer>().enabled = dict["Renderer"];
                }
                if (dict.ContainsKey("Collider"))
                {
                    go.GetComponent<Collider2D>().enabled = dict["Collider"];
                }
            }
        }
        CheckQueuedUnlocks();
        Debug.Log("Restored state.");
    }

    private static void CheckQueuedUnlocks()
    {
        for (int i = 0; i < queuedUnlocks.Count; i++)
        {
            var tryUnlock = SceneManager.UnlockSceneObject(queuedUnlocks[i]);
            if (tryUnlock)
            {
                queuedUnlocks.RemoveAt(i);
                i--;
            }
        }
    }

    /// <summary>
    /// Saves the state of all SceneObjects currently in the scene to static list
    /// </summary>
    public static void SaveState()
    {
        var interactiveObjects = FindObjectsOfType<SceneObject>();
        activeList = null;
        activeList = new Dictionary<string, Dictionary<string, bool>>();
        foreach (var so in interactiveObjects)
        {
            //if gameobject is already in dictionary, add this component's data
            if (activeList.ContainsKey(so.gameObject.name))
            {
                var data = activeList[so.gameObject.name];
                data.Add(so.ID.ToString(), so.enabled);
            }
            //if gameobject is not in dictionary, add render and collision data, then this component's data
            else
            {
                var data = new Dictionary<string, bool>();
                var renderer = so.GetComponent<Renderer>();
                if (renderer != null)
                    data.Add("Renderer", renderer.enabled);
                var collider = so.GetComponent<Collider2D>();
                if (collider != null)
                    data.Add("Collider", collider.enabled);
                data.Add(so.ID.ToString(), so.enabled);
                activeList.Add(so.gameObject.name, data);
            }
        }
        if (!cleanLevels.Contains(Application.loadedLevelName))
        {
            cleanLevels.Add(Application.loadedLevelName);
        }
        Debug.Log("Saved state.");
    }

    public static void End(int i)
    {
        unlockedEndings[i] = true;
        activeList = new Dictionary<string, Dictionary<string, bool>>();
        cleanLevels = new List<string>();
        queuedUnlocks = new List<string>();

        StateManager.radius = 13;

        StateManager.hasBedEnd = false;

        StateManager.hasCalledGrandma = false;
        StateManager.grandmaEnding = false;

        StateManager.tardies = 0;
        StateManager.isLate = false;

        Application.LoadLevel("Endings");
    }
}
