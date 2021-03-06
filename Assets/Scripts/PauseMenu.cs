﻿using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {

	[SerializeField] private GUITexture menuBackground;
	[SerializeField] private GUITexture playButton;
	[SerializeField] private GUITexture menuButton;
	[SerializeField] private GUITexture replayButton;
	
	[SerializeField] private Texture2D playButtonYellow; 
	[SerializeField] private Texture2D playButtonGreen; 
	[SerializeField] private Texture2D menuButtonYellow; 
	[SerializeField] private Texture2D menuButtonGreen; 
	[SerializeField] private Texture2D replayButtonYellow; 
	[SerializeField] private Texture2D replayButtonGreen; 
	
	private bool playPressed;
	private bool replayPressed;
	private bool menuPressed;
	
	[SerializeField] private AudioClip buttonSound;

	// Use this for initialization
	void Start () {
		playButton.GetComponent<GUITexture>().texture = playButtonGreen;
		menuButton.GetComponent<GUITexture>().texture = menuButtonGreen;
		replayButton.GetComponent<GUITexture>().texture = replayButtonGreen;
	
		menuBackground.enabled = false;
		playButton.enabled = false;
		menuButton.enabled = false;
		replayButton.enabled = false;
		
		playPressed = false;
		replayPressed = false;
		menuPressed = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.timeScale == 0) { // game paused
			menuBackground.enabled = true;
			playButton.enabled = true;
			menuButton.enabled = true;
			replayButton.enabled = true;
		}
		
		else if (Time.timeScale == 1) { // game not paused
			menuBackground.enabled = false;		
			playButton.enabled = false;
			menuButton.enabled = false;
			replayButton.enabled = false;	
		}
		
		if (menuBackground.enabled == true) {
		// Mouse down functions
		// ----------------
			if (Input.GetMouseButtonDown(0) && playButton.HitTest(Input.mousePosition)) {
				playButton.GetComponent<GUITexture>().texture = playButtonYellow;
				GetComponent<AudioSource>().PlayOneShot(buttonSound);
				playPressed = true;
			}
			else if (Input.GetMouseButtonDown(0) && menuButton.HitTest(Input.mousePosition)) {
				menuButton.GetComponent<GUITexture>().texture = menuButtonYellow;
				GetComponent<AudioSource>().PlayOneShot(buttonSound);
				menuPressed = true;
			}
			else if (Input.GetMouseButtonDown(0) && replayButton.HitTest(Input.mousePosition)) {
				replayButton.GetComponent<GUITexture>().texture = replayButtonYellow;
				GetComponent<AudioSource>().PlayOneShot(buttonSound);
				replayPressed = true;
			}
			// ----------------
			
			// Mouse up functions
			// !Input.GetMouseButton(0): when left mouse button is not held down
			// ----------------
			if (playPressed && !Input.GetMouseButton(0)) {
				Time.timeScale = 1;
				playButton.GetComponent<GUITexture>().texture = playButtonGreen;
				playPressed = false;
			}
			else if (menuPressed && !Input.GetMouseButton(0)) {
				Time.timeScale = 1;
				Application.LoadLevel("mainMenu");
				menuButton.GetComponent<GUITexture>().texture = menuButtonGreen;
				menuPressed = false;
			}
			else if (replayPressed && !Input.GetMouseButton(0)) {
				Time.timeScale = 1;
				menuButton.GetComponent<GUITexture>().texture = menuButtonGreen;
				replayPressed = false;
				
				if (Application.loadedLevelName == "MainHall") {
					Application.LoadLevel("MainHall");
				}
				else if (Application.loadedLevelName == "MainHall_") {
					Application.LoadLevel("MainHall_");
				}
				else if (Application.loadedLevelName == "DiningHall") {
					Application.LoadLevel("DiningHall");
				}
				else if (Application.loadedLevelName == "BossRoom") {
					Application.LoadLevel("BossRoom");
				}
				else if (Application.loadedLevelName == "ActualBossRoom") {
					Application.LoadLevel("ActualBossRoom");
				}
				
			}
			// ----------------
		}
	}
}
