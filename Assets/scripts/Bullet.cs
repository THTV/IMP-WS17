using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public float velX = 5f;
	float velY = 0f;
	Rigidbody2D rb;

	void Start () {
		rb = GetComponent<Rigidbody2D> ();

		// to avoid trash we destroy bullets after 8 seconds
		StartCoroutine (KillBullets());
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

	private IEnumerator KillBullets() {
		yield return new WaitForSecondsRealtime (8);
		Destroy (gameObject);
	}
}
