using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FireballSkill : MonoBehaviour {

	public float velX;
	public float velY;
	Rigidbody2D rb;
	Animator anim;

	public Transform playerTransform;
	public Transform targetOfFireball;
	public GameObject FireballRight, FireballLeft;
	private PlayerMovement PlayerMovement;
	Vector2 FireballPosition;
	public float speed = 5.0f;

	void Start () {
		PlayerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
		playerTransform = GameObject.FindGameObjectWithTag ("Player").transform;
		rb = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator>();
        
	}

	void Update () {
		rb.velocity = new Vector2 (velX, velY);

	// Fireball chase destination
		float distanceToTarget = Vector2.Distance (transform.position,targetOfFireball.position);
		Debug.DrawLine(transform.position,targetOfFireball.position,Color.red);

			if (transform.position.x > targetOfFireball.position.x) {
				Debug.Log ("enemy to my RIGHT");
				anim.SetBool("Right", true);
				anim.SetBool("Left", false);
				transform.Translate (Vector2.right * Time.deltaTime * speed);
			}

			if (transform.position.x < targetOfFireball.position.x) {  	
				Debug.Log ("enemy to my RIGHT");
				transform.Translate (Vector2.left * Time.deltaTime * speed);
				anim.SetBool("Left", true);
				anim.SetBool("Right", false);
			}

			if (transform.position.y > targetOfFireball.position.y) {
				Debug.Log ("enemy DOWN");
				transform.Translate (Vector2.down * Time.deltaTime * speed);
				anim.SetBool("Up", false);
				anim.SetBool("Down", true);
			}

			if (transform.position.y < targetOfFireball.position.y) {
				Debug.Log ("enemy UP");
				transform.Translate (Vector2.up * Time.deltaTime * speed);
				anim.SetBool("Up", true);
				anim.SetBool("Down", false);
			}
	}

	public void UseFireball() {
		FireballPosition = transform.position;

		if (PlayerMovement.facingright) {
			FireballPosition += new Vector2 (+1f, +0.43f);
			Instantiate (FireballRight, FireballPosition, Quaternion.identity);

		} else {
			FireballPosition += new Vector2 (-1f, +0.43F);
			Instantiate (FireballLeft, FireballPosition, Quaternion.identity);
            
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Enemy") {
			Destroy(gameObject);           
		}
	}
}
