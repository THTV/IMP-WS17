﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Bullets : MonoBehaviour {

	private PlayerMovement playerMovement;
	public Sprite[] spriteList;
	private SpriteRenderer spriteR;

	void Start () {
		playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
		spriteR = gameObject.GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		spriteR.sprite = spriteList[playerMovement.bulletCounter];
	}
}
