﻿using UnityEngine;
using System.Collections;

public class LifeCounter : MonoBehaviour {

	public GUITexture life1;
	public GUITexture life2;
	public GUITexture life3;
	public GUITexture life4;
	public int playerHealth;
	public InGameScoreScript script;
	private int loadedHealth;
	
	// Sound variables
	public AudioClip heartBeat;
	private bool playedHeartBeat = false; // to ensure the clip does not play on each frame
	public AudioClip takeDamage;
	public int playedTakeDamage; // to ensure the clip does not play on each frame
	public AudioClip die;
	private bool playedDie = false; // to ensure the clip does not play on each frame
	
	// Use this for initialization
	void Start () {
		// initialize to 3 if we are on the first level
		if(Application.loadedLevelName == "MainHall"){
			playerHealth = 4;
			playedTakeDamage = 0;
			loadedHealth = 4;
		}
		// else, load the current health
		else{
			playerHealth = PlayerPrefs.GetInt("playerHealth");
			loadedHealth = PlayerPrefs.GetInt("playerLoadedHealth"); // we need this so that when the player reloads the level, he will not "OUCH"
			playedTakeDamage = PlayerPrefs.GetInt("playedTakeDamage");
			if(loadedHealth == 3){
				playedTakeDamage = 2;
			}
			else if(loadedHealth == 2){
				playedTakeDamage = 3;
			}
			else if(loadedHealth == 1){
				playedTakeDamage = 4;
			}
		}
		PlayerPrefs.SetInt ("playerHealth", loadedHealth);
	}

	void Update () 
	{
		if (PlayerPrefs.HasKey ("playerHealth")) {
			playerHealth = PlayerPrefs.GetInt ("playerHealth");
		}
		// playerHealth = 3; // god mode
		// If player has 4 lives
		if(playerHealth >= 4)
		{
			life4.enabled = true;
			life3.enabled = true;
			life2.enabled = true;
			life1.enabled = true;
		}
		// If player has 3 lives
		else if(playerHealth == 3)
		{
			life4.enabled = false;
			life3.enabled = true;
			life2.enabled = true;
			life1.enabled = true;
			if(playedTakeDamage == 0 && loadedHealth != 3){
				StartCoroutine(PlayOuch());
				playedTakeDamage = 2;
				Handheld.Vibrate();
			}
		}
		// If player has 2 lives
		else if(playerHealth == 2)
		{
			life4.enabled = false;
			life3.enabled = false;
			life2.enabled = true;
			life1.enabled = true;
			if(playedTakeDamage <= 2 && loadedHealth != 2){
				StartCoroutine(PlayOuch());
				playedTakeDamage = 3;
				Handheld.Vibrate();
			}
		}
		// Else if player has 1 lives
		else if(playerHealth == 1)
		{
			life4.enabled = false;
			life3.enabled = false;
			life2.enabled = false;
			life1.enabled = true;
			if(playedTakeDamage <= 3 && loadedHealth != 1){
				StartCoroutine(PlayOuch());
				playedTakeDamage = 4;
				Handheld.Vibrate();
			}
			if(playedHeartBeat == false){
				audio.PlayOneShot(heartBeat);
				playedHeartBeat = true;
			}
		}
		// Else if player has 0 life
		else if(playerHealth < 1)
		{
			life4.enabled = false;
			life3.enabled = false;
			life2.enabled = false;
			life1.enabled = false;
			if(playedDie == false){
				playedDie = true;
				Handheld.Vibrate();
				StartCoroutine(PlayDie ());
			}

			// Update the highscore if it is higher
			if(script.highScore < script.currentScore){
				PlayerPrefs.SetInt ("highScore", (int)script.currentScore);
			}
			PlayerPrefs.SetInt ("currentScore", (int)script.currentScore);
		}
	}
	
	IEnumerator PlayDie(){
		audio.PlayOneShot(die);
		yield return new WaitForSeconds(1.0F);
		Application.LoadLevel ("GameOver");
		yield break;
	}
	
	IEnumerator PlayOuch(){
		yield return new WaitForSeconds(0.5F);
		audio.PlayOneShot(takeDamage);
		yield return new WaitForSeconds(0.5F);
		yield break;
	}
}