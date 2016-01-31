using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ClockComponent : MonoBehaviour {
    
	// Use this for initialization
    void Awake()
    {
        Timer.Subscribe(UpdateUI);
    }

    void UpdateUI (int hour, int minute)
    {
        var uiText = GetComponent<Text>();
        uiText.text = BuildTimeString(hour, minute);
    }

    string BuildTimeString(int hour, int minute)
    {
        return string.Format(hour + ":" + (minute < 10 ? "0" : "") + minute);
    }


}
