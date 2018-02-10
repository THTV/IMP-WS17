using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

	public Transform playerTransform;		//Enemy has to know where the Player is
	public float chaseRange;
	public float speed;

	bool facingRight = true;				// check which direction enemy is facing
	bool canMove = true;					// To disable enemy Movement

	private Animator enemyAnimator;			// Reference to the enemys's animator component.

	void Awake() // Set Enemys Components
	{
		playerTransform = GameObject.FindGameObjectWithTag ("Player").transform;
		enemyAnimator = GetComponent<Animator>();
	}

	void flip() {	 //flip the direction the enemy is facing
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	void Update () { // Enemy follow the Player
		float distanceToTarget = Vector3.Distance (transform.position,playerTransform.position);
		Debug.DrawLine(transform.position,playerTransform.position,Color.yellow);

		if (distanceToTarget < chaseRange) {
			//start chasing the target - turn and move towards the player
			Debug.DrawLine(transform.position,playerTransform.position,Color.red);

			if (playerTransform.position.x > transform.position.x) {		
				// if the player is to the right
				if (facingRight) {
					flip();
				}
				transform.Translate (Vector2.right * Time.deltaTime * speed);

			} else {  // the player is to the left of the enemy
				if (!facingRight) {
					flip();
				}
				transform.Translate (Vector2.left * Time.deltaTime * speed);
			}
		}
	}

//	void setNewIdlePoint() {
//		//set a new random position to walk to
//		idleToPosition = new Vector2 (transform.position.x + Random.Range (-4, 4), transform.position.y);
//	}
//
//	void enemyIdle() {
//		
//		if (idleToPosition.x == transform.position.x) { 	// if the enemy is idled to the idleposition
//			setNewIdlePoint();										// set an new position to idle to
//		}
//
//		if (idleToPosition.x > transform.position.x) {
//			if (facingRight) {
//				flip();
//			}
//			transform.Translate (Vector2.right * Time.deltaTime * speed);
//		} 
//		else {
//			if (!facingRight) {
//				flip();
//			}
//			transform.Translate (Vector2.left * Time.deltaTime * speed);
//		}
//	}
}

