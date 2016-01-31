using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndingSetupComponent : MonoBehaviour {
    
    public Sprite[] endingSprites = new Sprite[3];
    public string[] endingLines = new string[3];
    public Image[] endingImages = new Image[3];
    public Text[] endingTexts = new Text[3];

    void Awake()
    {
        for (int index = 0; index < StateManager.unlockedEndings.Count; index++)
        {
            if (StateManager.unlockedEndings[index])
            {
                endingImages[index].sprite = endingSprites[index];
                endingTexts[index].text = endingLines[index];
            }
        }
    }

    void Update ()
    {
        if (Input.GetMouseButton(0))
        {
            Application.LoadLevel("Start");
        }
    }
}
