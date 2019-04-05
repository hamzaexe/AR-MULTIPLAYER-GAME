using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BgAudio : MonoBehaviour {
	public Slider Volume;
	public AudioSource myMusic;

	void Awake()
	{
		DontDestroyOnLoad (transform.gameObject);
	}
	void Update()
	{
		myMusic.volume = Volume.value;
	}
}
