﻿using UnityEngine;
using System.Collections;

public class VolumeButtons : MonoBehaviour {

	private float effectsVolume = 5.0F;
	
	public Texture2D button1;
	public Texture2D button2;
	
	public TextMesh effectsVolumeText;
	
	public AudioClip menuButton;

	void Start(){
		if (PlayerPrefs.HasKey ("effectsVolume")) {
			effectsVolume = PlayerPrefs.GetInt ("effectsVolume");
		}
		AudioListener.volume = effectsVolume / 10;
	}

	void OnMouseUp(){
		if (this.name == "EffectsUp"){
			GetComponent<GUITexture>().texture = button1;
			GetComponent<AudioSource>().PlayOneShot(menuButton);
			if(effectsVolume < 10)
			{	
				effectsVolume+=1;
				AudioListener.volume += 0.1F;
				PlayerPrefs.SetInt ("effectsVolume", (int)effectsVolume);
			}
			else if(effectsVolume == 10)
			{
				effectsVolume = 10;
				AudioListener.volume = 1.0F;
				PlayerPrefs.SetInt ("effectsVolume", (int)effectsVolume);
			}
		}
		else if (this.name == "EffectsDown"){
			GetComponent<GUITexture>().texture = button1;
			GetComponent<AudioSource>().PlayOneShot(menuButton);
			if(effectsVolume > 0)
			{
				effectsVolume-=1;
				AudioListener.volume -= 0.1F;
				PlayerPrefs.SetInt ("effectsVolume", (int)effectsVolume);
			}
			else if(effectsVolume == 0)
			{
				effectsVolume = 0;
				AudioListener.volume = 0.0F;
				PlayerPrefs.SetInt ("effectsVolume", (int)effectsVolume);
			}
		}
	}
	
	void OnMouseDown(){
		if (GetComponent<GUITexture>().name == "EffectsUp")
			GetComponent<GUITexture>().texture = button2;
		else if (GetComponent<GUITexture>().name == "EffectsDown")
			GetComponent<GUITexture>().texture = button2;
	}
	
	void Update()
	{
		if (PlayerPrefs.HasKey ("effectsVolume")) {
			effectsVolume = PlayerPrefs.GetInt ("effectsVolume");
		}
		effectsVolumeText.text = effectsVolume.ToString ();
	}
}
