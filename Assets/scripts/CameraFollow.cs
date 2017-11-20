using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

		public GameObject player;       //the player game object
		private Vector3 offset;         //the offset distance between player and camera

		void Start () 
		{
			offset = transform.position - player.transform.position;
		}

		// LateUpdate is called after Update each frame
		void LateUpdate () 
		{
			// Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
			transform.position = player.transform.position + offset;
		}
	}