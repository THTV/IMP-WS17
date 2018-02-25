using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUp : MonoBehaviour {


	public PlayerMovement Player;
	Animator anim; // Reference to the player's animator component.
	public AudioClip[] audioclip; // Creates an Array to store our GameSounds
	[SerializeField] int energie = 0; //show me number of energie collected

	void Awake() // Setting up references.
	{
		anim = GetComponent<Animator>();
	}

	//Enter a Trigger
	void OnTriggerEnter2D(Collider2D other) 
	{
		if (other.tag == "Energiecrystal") 
		{
			Destroy(other.gameObject);
			energie++;
			Playsound(0);   //Sound '0' == "coin9"
		}
	}

	//  SOUND
	void Playsound(int clip)
	{
		GetComponent<AudioSource>().clip = audioclip [clip];
		GetComponent<AudioSource>().Play ();
	}

	void OnGUI()
	{
		//GUILayout.Label( "Energiezellen = " + energie );
	}


}
