using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

	public Transform target;				//Enemy has to know who the Player is
	public float chaseRange;
	public float speed;

	bool facingRight = true;				// check which direction enemy is facing
	bool canMove = true;					// To disable enemy Movement

	private Animator enemyAnimator;			// Reference to the enemys's animator component.
	private Rigidbody2D enemyRigidbody;		// And his Rigidbody


	void Start() // Set Enemys Components
	{
		enemyAnimator = GetComponent<Animator>();
		enemyRigidbody = GetComponent<Rigidbody2D> ();
	}
	

	void Update () { // Enemy follow the Player
		float distanceToTarget = Vector2.Distance (transform.position,target.position);
		if (distanceToTarget < chaseRange) {
			//start chasing the target - turn and move towards the player
			if (target.position.x > transform.position.x) {		
				// if the player is to the right
				if (facingRight) {
					flip ();
				}
				transform.Translate (Vector2.right * Time.deltaTime * speed);
			}
			else {  // the player is to the left of the enemy
				if(!facingRight) {
					flip ();
				}
				transform.Translate (Vector2.left * Time.deltaTime * speed);
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
