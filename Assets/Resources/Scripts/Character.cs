using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKey(KeyCode.RightArrow))
        {
            gameObject.transform.Translate(5 * Time.deltaTime, 0, 0);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            gameObject.transform.Translate(-5 * Time.deltaTime, 0, 0);
        }
        if (gameObject.transform.position.x > 10)
            gameObject.transform.position = new Vector3(10, gameObject.transform.position.y);
	}
}
