using UnityEngine;
using System.Collections;

public class TextManager : MonoBehaviour {

    private Writer writer;

	// Use this for initialization
	void Start () {
        writer = GetComponentInChildren<Writer>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnMouseDown()
    {
        writer.OnClick();
    }
}
