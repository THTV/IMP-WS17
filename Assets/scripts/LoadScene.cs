using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScene : MonoBehaviour {

	public int page = 0;
	Animator anim;
	public GameObject touchtext;

	void Start () {
		anim = GetComponent<Animator>();
		touchtext = GameObject.FindGameObjectWithTag ("touchtext");
	}

	public void Update () {

		if (page==0) { // shows titlemenu on init // ""Elli's Adventure - Touch 2 start"
			anim.SetInteger("page", 0);
		}

		if (page==1) {	// after first touch -> switch to "Intro_1 Picture" (spaceship flying)
			Destroy(touchtext);
			anim.SetInteger("page", 1);
		}

		if (page==2) {	// switch from intro_1 to _2 (Spaceship got hit by comet)
			anim.SetInteger("page", 2);
		}

		if (page==3) {
			anim.SetInteger("page", 3);		// switch from intro_2 to _3 (Spaceship emergency landing on planet)
		}

		if (page==4) {
			Application.LoadLevel (1); 		// Load Game after Intro_3
		}
	}

	public void goOn() {
		page +=1;
	}
}
