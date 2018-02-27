using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballSkill : MonoBehaviour {

	public float velX;
	public float velY;
	Rigidbody2D rb;

	void Start () {
		rb = GetComponent<Rigidbody2D> ();
	}

	void Update () {
		rb.velocity = new Vector2 (velX, velY);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Enemy") {
			Destroy(gameObject);           
		}
	}
}
