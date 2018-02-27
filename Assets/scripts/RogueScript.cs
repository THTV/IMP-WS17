using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogueScript : MonoBehaviour {

	public Transform target;				//Enemy has to know who the Player is
	public float chaseRange;
	public float attackRange;
	public float speed;

	public AudioClip[] audioclip;			// Creates an Array to store my GameSounds

	public int enemyCurHealth;				// Number of Lifes left
	public int enemyMaxHealth = 8;			// maximun Life

	bool facingRight = true;				// check which direction enemy is facing
	bool canMove = true;					// To disable enemy Movement

	private Animator enemyAnimator;			// Reference to the enemys's animator component.
	private Rigidbody2D enemyRigidbody;		// And his Rigidbody

	void Start() // Set Enemys Components
	{
		enemyAnimator = GetComponent<Animator>();
		enemyRigidbody = GetComponent<Rigidbody2D> ();
		target = GameObject.FindGameObjectWithTag ("Player").transform;
	}


	void Update () { // Enemy follow the Player
		float distanceToTarget = Vector2.Distance (transform.position,target.position);
		if (distanceToTarget < chaseRange && distanceToTarget > attackRange) {
			//start chasing the target - turn and move towards the player
			enemyAnimator.SetBool("walking",true);
			if (target.position.x > transform.position.x) {		
				// if the player is to the right
				if (!facingRight) {
					flip ();
				}
				transform.Translate (Vector2.right * Time.deltaTime * speed);
				if (distanceToTarget < attackRange) {
					enemyAnimator.SetBool ("walking", false);
					enemyAnimator.SetTrigger ("attack");
				}
			}
			else {  // the player is to the left of the enemy
				if(facingRight) {
					flip ();
				}
				transform.Translate (Vector2.left * Time.deltaTime * speed);
				if (distanceToTarget < attackRange) {
					enemyAnimator.SetTrigger ("attack");
				}
			}
		}

		// Death of the Enemy
		if (enemyCurHealth < 1) {
			enemyDeath ();
		}
	}

	void flip() {	 //flip the direction the enemy is facing
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	//gettin some dmg from bullets
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "bullet") {
			enemyCurHealth--;
			enemyAnimator.SetTrigger ("hurt");
		}
        if(other.tag == "Fireball")
        {
            enemyCurHealth -= 5;
            enemyAnimator.SetTrigger("hurt");
        }
	}

	void enemyDeath() {
		enemyAnimator.SetTrigger ("death");
		Playsound(0); // Sound '0' == "rogue_dying"

		// Destroying some parts to leave a Dead, walkable Body behind
		Destroy (enemyRigidbody);
		Destroy (GetComponent<CapsuleCollider2D> ());
		Destroy (GetComponent<BoxCollider2D> ());
		Destroy (this); // this == destroys this script on the GameObject
	}

	void Playsound(int clip)
	{
		GetComponent<AudioSource>().clip = audioclip [clip];
		GetComponent<AudioSource>().Play ();
	}
}
