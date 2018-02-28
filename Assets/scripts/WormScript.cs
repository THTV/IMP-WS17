using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormScript : MonoBehaviour {

	bool facingRight = true;				// check which direction the worm is facing

	private Animator enemyAnimator;			// Reference to the enemys's animator component.
	private Rigidbody2D enemyRigidbody;		// And his Rigidbody

	public Transform player;				//Enemy has to know who the Player is
	public float attackRange;

	void Start() // Set Enemys Components
	{
		enemyAnimator = GetComponent<Animator>();
		enemyRigidbody = GetComponent<Rigidbody2D> ();
	}
	
	void Update () { // Enemy follow the Player
		float distanceToTarget = Vector2.Distance (transform.position,player.position);
		if (distanceToTarget < attackRange) {
			//start chasing the target - turn and move towards the player
			if (player.position.x > transform.position.x) {		
				// if the player is to the right
				if (!facingRight) {
					flip ();
				}
				enemyAnimator.SetTrigger ("Player_Close");
			}
			else {  // the player is to the left of the enemy
				if(facingRight) {
					flip ();
				}
				enemyAnimator.SetTrigger ("Player_Close");
			}
		}
	}

	void flip() {	 //flip the direction the enemy is facing
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
