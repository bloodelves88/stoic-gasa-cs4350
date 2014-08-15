using UnityEngine;
using System.Collections;

public class EnemyShoot : MonoBehaviour
{
	public GameObject m_PrefabBullet; // Drag the prefab "EnemyBullet" here.
	public GameObject player; // Drag the character prefab here
	
	// Ignore this comment below. Just some notes.
	// Cannot drag the prefab "Character" here. Do this: For each target (not prefab), drag the "Character" game object here.

	[SerializeField]
	// Speed of the bullet. 
	protected float m_Speed = 10.0f;
	[SerializeField]
	// Rate of fire for the enemy
	protected float fireRate = 0.5F;
	protected float nextFire = 0.5F;
	
	// Audio
	public AudioClip bearShoot;

	/*
	void Start(){
		Debug.Log ("Player position = " + player.transform.position.x + " " + player.transform.position.y + " "+ player.transform.position.z);
		
	}
	*/
	// Update is called once per frame
	void Update ()
	{
		GameObject enemy = GameObject.FindWithTag("Enemy");
		player = GameObject.FindWithTag ("MainCharacter");

		Enemy e = enemy.GetComponent<Enemy>();
		//only attacks when enemy is in attack state 
		//if(e.current == Enemy.States.Attack && e.positionOriginal >= e.transform.position.y)
		if(e.current == Enemy.States.Attack && !(e.animation.isPlaying))
		{

			if(Time.time - nextFire > fireRate){
				nextFire = Time.time + fireRate;
				GameObject clone;
				// Create a clone of the 'Bullet' prefab. We have multiple offsets because the bears are of different sizes in different scenes.
				if(Application.loadedLevelName == "DiningHall"){
					clone = Instantiate(m_PrefabBullet, transform.position+new Vector3(-0.8F,3.5F,-1F), transform.rotation) as GameObject;
				}
				else if(Application.loadedLevelName == "MainHall"){
					clone = Instantiate(m_PrefabBullet, transform.position+new Vector3(7F,11F,6F), transform.rotation) as GameObject;
				}
				else{
					clone = Instantiate(m_PrefabBullet, transform.position+new Vector3(5F,7F,-4F), transform.rotation) as GameObject;
				}
				audio.PlayOneShot(bearShoot);
				//Debug.Log ("Bullet position = " + clone.transform.position.x + " " + clone.transform.position.y + " "+ clone.transform.position.z);
				//Debug.Log ("Target position = " + (player.transform.position - transform.position).x + " " + (player.transform.position - transform.position).y + " "+ (player.transform.position - transform.position).z);
				
				float hitOrNot = Random.Range(0.0F, 1.0F);
				float offsetValueX = Random.Range (-4.0F, 4.0F);
				float offsetValueY = Random.Range (-4.0F, 4.0F);
				
				// Exclude values where offset is between -1 and 1.
				while(offsetValueX < -2.0F && offsetValueX > 2.0F){
					offsetValueX = Random.Range (-4.0F, 4.0F);
				}
				while(offsetValueY < -2.0F && offsetValueY > 2.0F){
					offsetValueY = Random.Range (-4.0F, 4.0F);
				}
				
				Vector3 randomOffset;
				if(hitOrNot < 0.08F){ // hit
					Debug.Log ("Hit");
					if(Application.loadedLevelName == "DiningHall"){
						randomOffset = new Vector3(0.8F,-3.5F,1F);
					}
					else if(Application.loadedLevelName == "MainHall"){
						randomOffset = new Vector3(-7F,-11F,-6F);
					}
					else{
						randomOffset = new Vector3(-5F,-7F,4F);
					}
				}
				else{ // no hit
					if(Application.loadedLevelName == "DiningHall"){
						randomOffset = new Vector3(offsetValueX+0.8F, offsetValueY-3.5F, offsetValueY+1F);
					}
					else if(Application.loadedLevelName == "MainHall"){
						randomOffset = new Vector3(offsetValueX-7F, offsetValueY-11F, offsetValueY-6F);
					}
					else{
						randomOffset = new Vector3(offsetValueX-5F, offsetValueY-7F, offsetValueY+4F);
					}
					// randomOffset = new Vector3(offsetValueX-2.5F, offsetValueY-7F, offsetValueY-6F);
				}

				// Adds a force to the bullet so it can move
				clone.rigidbody.velocity = ((player.transform.position + randomOffset - transform.position));
			}
		}
	}
}



