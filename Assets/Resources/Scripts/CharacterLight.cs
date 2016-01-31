using UnityEngine;
using System.Collections;

public class CharacterLight : MonoBehaviour {

    private Light spotLight;
    [Range(0, 100)]
    public int level = 13;

    public int smallZ = -5;
    public int bigZ = -21;

    public int smallRange = 10;
    public int bigRange = 41;

    public int smallAngle = 30;
    public int bigAngle = 144;

	// Use this for initialization
	void Start () {
        spotLight = GetComponent<Light>();
        spotLight.intensity = 8;
        UpdateLight();
	}
	
	// Update is called once per frame
	void Update () {
        UpdateLight();
	}

    private void UpdateLight()
    {
        spotLight.spotAngle = smallAngle + (bigAngle - smallAngle) * level / 100;
        spotLight.range = smallRange + (bigRange - smallRange) * level / 100;

        var pos = transform.position;
        pos.z = smallZ + (bigZ - smallZ) * level / 100;
        transform.position = pos;
    }

    public void IncreaseLevel(int amount)
    {
        level += amount;
    }
}
