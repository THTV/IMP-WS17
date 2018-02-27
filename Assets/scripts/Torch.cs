using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour {

	public Transform target;		// Torch will be light when player in range
	public float range;
	private Animator torchAnimator;
	private BoxCollider2D torchCollider;

	void Start() // Set Enemys Components
	{
		torchAnimator = GetComponent<Animator>();
		torchCollider = GetComponent<BoxCollider2D> ();
		target = GameObject.FindGameObjectWithTag ("Player").transform;
	}
		
	void Update () {
		float distanceToTarget = Vector2.Distance (transform.position,target.position);
		if (distanceToTarget < range) {
			//ignite torch when player is close
			torchAnimator.SetTrigger("PlayerNear");
			Destroy (torchCollider, 0.3f);  // Destroying the Collider after 0.3 seconds so that the ignite
		}									// Torch sound won't trigger again and again and again...
	}
}
