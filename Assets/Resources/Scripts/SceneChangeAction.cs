using UnityEngine;
using System.Collections;

public class SceneChangeAction : MonoBehaviour {

	// Use this for initialization
	public void ChangeSceneTo(string scene)
    {
        Application.LoadLevel(scene);
    }
}
