using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Energie : MonoBehaviour {

	public Text energieText;
	private PlayerMovement playerMovement;

	void Start () {
		playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
	}
		
	void Update () {
		energieText.text = "x " + playerMovement.energie;
	}
}
