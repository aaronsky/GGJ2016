using UnityEngine;
using System.Collections;

public class AudioTriggers : MonoBehaviour {

	public enum AudioScene
	{
		Music1,
		Music2,
		Bedroom,
		Subway,
		Office
	};


	public AudioClip[] ac;
	public AudioScene currentScene;
	public AudioLowPassFilter lpf;

	private AudioSource source;

	// Use this for initialization
	void Awake () {
		source = this.gameObject.GetComponent<AudioSource>();
		lpf = this.gameObject.GetComponent<AudioLowPassFilter>();

		if(currentScene == AudioScene.Music1)
		{
			source.clip = ac[0];
		}
		else if(currentScene == AudioScene.Music2)
		{
			source.clip = ac[1];
		}
		else if(currentScene == AudioScene.Bedroom)
		{
			source.clip = ac[2];
		}
		else if(currentScene == AudioScene.Subway)
		{
			source.clip = ac[3];
		}
		else if(currentScene == AudioScene.Office)
		{
			source.clip = ac[4];
		}
				

	
	}

	void Start() {
		source.Play();
	}

	// Update is called once per frame
	void Update () {
		updateSoundSource();
	
	}

	public void startSubwaySound()
	{
		source.PlayOneShot(ac[5],1.0f);
	}

	public void finishSubwaySound()
	{
		source.PlayOneShot(ac[6], 1.0f);
	}

	public void updateSoundSource()
	{
		if(currentScene == AudioScene.Music1)
		{
			source.clip = ac[0];
		}
		else if(currentScene == AudioScene.Music2)
		{
			source.clip = ac[1];
		}
		else if(currentScene == AudioScene.Bedroom)
		{
			source.clip = ac[2];
		}
		else if(currentScene == AudioScene.Subway)
		{
			source.clip = ac[3];
		}
		else if(currentScene == AudioScene.Office)
		{
			source.clip = ac[4];
		}
	}

	public void setLowPass(int level)
	{
		switch(level)
		{
		case 1:
			GetComponent<AudioLowPassFilter>().cutoffFrequency = 500.0f;
			break;
		case 2:
			GetComponent<AudioLowPassFilter>().cutoffFrequency = 1000.0f;
			break;
		case 3:
			GetComponent<AudioLowPassFilter>().cutoffFrequency = 2000.0f;
			break;
		case 4:
			GetComponent<AudioLowPassFilter>().cutoffFrequency = 4000.0f;
			break;
		case 5:
			GetComponent<AudioLowPassFilter>().cutoffFrequency = 8000.0f;
			break;
		case 6:
			GetComponent<AudioLowPassFilter>().cutoffFrequency = 16000.0f;
			break;
		case 7:
			GetComponent<AudioLowPassFilter>().cutoffFrequency = 22000.0f;
			break;
		}
			

	}
}
