﻿using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour {

	// Gun variables
	// -------------
	public GameObject bullethole;
	private int ammoCount;
	public GunDisplay gunDisplayScript;
	// Rate of fire for the enemy
	[SerializeField] protected float fireRateHMG = 0.05F;
	protected float nextFireHMG = 0.5F;
	[SerializeField] protected float fireRateShotgun = 0.1F;
	protected float nextFireShotgun = 0.5F;
	private bool shotgunShooting = false;
	// -------------

	// Shield variables
	// -------------
	public Shield shieldScript;
	// -------------

	// Score variables
	// -------------
	public InGameScoreScript scoreScript;
	public GameObject plus10;
	public GameObject plus20;
	public GameObject plus30;
	// -------------
	
	// Sound variables
	// -------------
	public AudioClip pistolShoot;
	public AudioClip HMGShoot;
	public AudioClip shotgunShoot;
	// -------------
	
	[SerializeField] private GUITexture pauseButton;
	
	// Update is called once per frame
	void Update () {
	
		Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		// Multiple raycasts for shotgun
		// ----------
		Ray myRay2 = Camera.main.ScreenPointToRay(Input.mousePosition - new Vector3(40,40,40));
		RaycastHit hit2;

		Ray myRay3 = Camera.main.ScreenPointToRay(Input.mousePosition + new Vector3(-40,40,-40));
		RaycastHit hit3;

		Ray myRay4 = Camera.main.ScreenPointToRay(Input.mousePosition + new Vector3(40,0,40));
		RaycastHit hit4;

		Ray myRay5 = Camera.main.ScreenPointToRay(Input.mousePosition + new Vector3(0,40,40));
		RaycastHit hit5;
		// ----------

		if(Time.timeScale > 0){ // can only shoot if not paused
			// if gun is pistol
			if(gunDisplayScript.currentSelection == "Pistol")
			{
				if(!pauseButton.HitTest(Input.mousePosition) && Input.GetMouseButtonDown(0) && GUIUtility.hotControl == 0)
				{
					if(Physics.Raycast(myRay,out hit) && shieldScript.reloading == false) {
						if(gunDisplayScript.ammoCountPistol > 0 && hit.transform.gameObject.tag != "Shield" && hit.transform.gameObject.tag != "EnemyBullet"){ // prevent shooting the shield or bullet
							Instantiate(bullethole, hit.point, Quaternion.identity);		
							Debug.DrawRay(myRay.origin, myRay.direction*hit.distance, Color.red);
							gunDisplayScript.ammoCountPistol--; // decrease ammo count
							audio.PlayOneShot(pistolShoot);
			
							hitDetection(hit);
						}
					}
				}
			}
			
			// if gun is HMG
			if(gunDisplayScript.currentSelection == "HMG")
			{
				if(!pauseButton.HitTest(Input.mousePosition) && Input.GetMouseButton(0) && Time.time - nextFireHMG > fireRateHMG && GUIUtility.hotControl == 0)
				{

					if(Physics.Raycast(myRay,out hit) && shieldScript.reloading == false) {
						if(gunDisplayScript.ammoCountHMG > 0 && hit.transform.gameObject.tag != "Shield" && hit.transform.gameObject.tag != "EnemyBullet"){ // prevent shooting the shield or bullet
							Instantiate(bullethole, hit.point, Quaternion.identity);		
							Debug.DrawRay(myRay.origin, myRay.direction*hit.distance, Color.red);
							audio.PlayOneShot(HMGShoot);
			
							hitDetection(hit);

							gunDisplayScript.ammoCountHMG--; // decrease ammo count
							nextFireHMG = Time.time + fireRateHMG; // shooting delay
						}
					}
				}
			}
			// If gun is shotgun
			if(gunDisplayScript.currentSelection == "Shotgun")
			{
				if(!pauseButton.HitTest(Input.mousePosition) && Input.GetMouseButtonDown(0) && shotgunShooting == false && shieldScript.reloading == false && GUIUtility.hotControl == 0)
				{
									
					shotgunShooting = true; // let the script know that we are shooting with the shotgun
					StartCoroutine(ShotgunShooting()); // call this method

					// Bullet/raycast 1
					if(Physics.Raycast(myRay,out hit) && shieldScript.reloading == false) {
						if(gunDisplayScript.ammoCountShotgun > 0 && hit.transform.gameObject.tag != "Shield" && hit.transform.gameObject.tag != "EnemyBullet"){ // prevent shooting the shield or bullet
							gunDisplayScript.ammoCountShotgun--; // decrease ammo count 
							Instantiate(bullethole, hit.point, Quaternion.identity);		
							Debug.DrawRay(myRay.origin, myRay.direction*hit.distance, Color.red);
							audio.PlayOneShot(shotgunShoot);
			
							hitDetection(hit);
						}
					}

					// Bullet/raycast 2
					if(Physics.Raycast(myRay2,out hit2) && shieldScript.reloading == false) {
						if(gunDisplayScript.ammoCountShotgun > 0 && hit.transform.gameObject.tag != "Shield" && hit.transform.gameObject.tag != "EnemyBullet"){ // prevent shooting the shield or bullet
							Instantiate(bullethole, hit2.point, Quaternion.identity);		
							Debug.DrawRay(myRay2.origin, myRay2.direction*hit2.distance, Color.red);
							audio.PlayOneShot(shotgunShoot);
							
							hitDetection(hit2);
						}
					}

					// Bullet/raycast 3
					if(Physics.Raycast(myRay3, out hit3) && shieldScript.reloading == false) {
						if(gunDisplayScript.ammoCountShotgun > 0 && hit.transform.gameObject.tag != "Shield" && hit.transform.gameObject.tag != "EnemyBullet"){ // prevent shooting the shield or bullet
							Instantiate(bullethole, hit3.point, Quaternion.identity);		
							Debug.DrawRay(myRay3.origin, myRay3.direction*hit3.distance, Color.red);
							audio.PlayOneShot(shotgunShoot);
							
							hitDetection(hit3);
						}
					}

					// Bullet/raycast 4
					if(Physics.Raycast(myRay4, out hit4) && shieldScript.reloading == false) {
						if(gunDisplayScript.ammoCountShotgun > 0 && hit.transform.gameObject.tag != "Shield" && hit.transform.gameObject.tag != "EnemyBullet"){ // prevent shooting the shield or bullet
							Instantiate(bullethole, hit4.point, Quaternion.identity);		
							Debug.DrawRay(myRay4.origin, myRay4.direction*hit4.distance, Color.red);
							
							hitDetection(hit4);
						}
					}

					// Bullet/raycast 5
					if(Physics.Raycast(myRay5,out hit5) && shieldScript.reloading == false) {
						if(gunDisplayScript.ammoCountShotgun > 0 && hit.transform.gameObject.tag != "Shield" && hit.transform.gameObject.tag != "EnemyBullet"){ // prevent shooting the shield or bullet
							Instantiate(bullethole, hit5.point, Quaternion.identity);		
							Debug.DrawRay(myRay5.origin, myRay5.direction*hit5.distance, Color.red);
							
							hitDetection(hit5);
						}
					}
				}
			}
		}
	}

	// Delay for shooting with a shotgun
	IEnumerator ShotgunShooting(){
		yield return new WaitForSeconds(0.5F);
		shotgunShooting = false;
		yield break;
	}
	
	IEnumerator Plus10(GameObject thingHit){
		if(thingHit.tag == "Enemy" && Application.loadedLevelName == "mainHall"){
			Instantiate(plus10, new Vector3(thingHit.transform.position.x,thingHit.transform.position.y+15f,thingHit.transform.position.z), thingHit.transform.rotation); 
		}
		else{
			Instantiate(plus10, new Vector3(thingHit.transform.position.x,thingHit.transform.position.y+5f,thingHit.transform.position.z), thingHit.transform.rotation); 
		}
		scoreScript.currentScore += 10;
		yield break;
	}
	
	IEnumerator Plus20(GameObject thingHit){
		Instantiate(plus20, thingHit.transform.position, thingHit.transform.rotation); 
		scoreScript.currentScore += 20;
		yield break;
	}
	
	IEnumerator Plus30(GameObject thingHit){
		Instantiate(plus30, thingHit.transform.position, thingHit.transform.rotation); 
		scoreScript.currentScore += 30;
		yield break;
	}
	
	void hitDetection(RaycastHit theHit){
		if(theHit.transform.gameObject.tag == "Enemy") {
			GameObject target = theHit.collider.gameObject;
			StartCoroutine(Plus10(target));
			Enemy script = target.GetComponent<Enemy>();
			script.StartAnim();
		}
		else if(theHit.transform.gameObject.tag == "EnemyHead") {
			GameObject target = theHit.collider.gameObject;
			StartCoroutine(Plus20(target));
			Enemy script = target.GetComponent<Enemy>();
			script.StartAnim();
		}
		else if(theHit.transform.gameObject.tag == "EnemyLollipop") {
			GameObject target = theHit.collider.gameObject;
			StartCoroutine(Plus20(target));
			EnemyLollipop script = target.GetComponent<EnemyLollipop>();
			script.StartAnim();
		}
		else if(theHit.transform.gameObject.tag == "EnemyEgg") {
			GameObject target = theHit.collider.gameObject;
			StartCoroutine(Plus30(target));
			EnemyEgg script = target.GetComponent<EnemyEgg>();
			script.StartAnim();
		}
	}
}
