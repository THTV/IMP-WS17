using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerMovement))]
public class HUD : MonoBehaviour {

	public Sprite[] LifeBars;
	public Image LifeBarsHUD;

	public PlayerMovement character;

	void Start()
	{
		character = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement> ();
	}


	void Update() {
		LifeBarsHUD.sprite = LifeBars[character.curHealth];
	}
}
