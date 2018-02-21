﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class PlayerMovement : MonoBehaviour {


    private float horizontalMovement;
    public float moveSpeed = 1;

    //public VirtualJoystick moveJoystick;
    [Range(0, 1)]

	bool canMove = true;								// To disable Player Movement
	Animator anim;										// Reference to the player's animator component.
	Rigidbody2D myRigidbody;

	//Sound Array
	public AudioClip[] audioclip;						// Creates an Array to store my GameSounds

	public int curHealth;								// Number of Lifes left
	public int maxHealth = 17;							// maximun Life
	[SerializeField] int cells = 0;						// Number of Cells collected
	bool dead = false;
    


	void Start()
	{
		anim = GetComponent<Animator>();
		myRigidbody = GetComponent<Rigidbody2D> ();
	}

	void Update() {
		if (curHealth > maxHealth) {
			curHealth = maxHealth;
		}
        if (canMove)
        {
            if(Input.touchCount > 0)
            {
                Touch myTouch = Input.touches[0];

                if (myTouch.position.x < Screen.width / 2)
                {
                    anim.SetInteger("Direction", 0);
                    anim.SetFloat("MoveSpeed", 0);
                    if (myTouch.phase == TouchPhase.Stationary)
                    {
                        anim.SetFloat("MoveSpeed", moveSpeed);               
                        transform.Translate(myTouch.position.x, 0, moveSpeed * Time.deltaTime);
                        Debug.Log("LINKS");
                    }
                }
                else if(myTouch.position.x > Screen.width / 2)
                {
                    anim.SetInteger("Direction", 1);
                    anim.SetFloat("MoveSpeed", 0);
                    if (myTouch.phase == TouchPhase.Stationary)
                    {
                        anim.SetFloat("MoveSpeed", moveSpeed);                   
                        transform.Translate(myTouch.position.x, 0, moveSpeed * Time.deltaTime);
                        Debug.Log("RECHTS");
                    }
                }
            }
        }
    }

	void FixedUpdate()
	{
			
		if (curHealth < 0.5) //The Death
		{
			GetComponent<Rigidbody2D>().velocity = (new Vector2 (0f, 0f));
			Debug.Log ("YOU ARE DEAD");
			canMove = false;
			anim.SetBool ("Dead", true);
			Playsound(1); //Sound '1' == "game_over"
			Invoke ("freeze", 7);
		}
	}

	void freeze()
	{
		Time.timeScale = 0f;
	}

	//Enter a Trigger
	void OnTriggerEnter2D(Collider2D other) 
	{
		if (other.tag == "Cell") 
		{
			cells++;
		}

		if (other.gameObject.tag == "Hurt" || other.gameObject.tag == "Enemy") 
		{
			//anim.SetBool ("Hurt", true);
			curHealth--;
			Playsound(0); //Sound '0' == "argh-woman"
			if(curHealth>0)
			{
				GetComponent<Rigidbody2D>().AddForce(new Vector2(10f, 50f));
			}
		}

		if (other.gameObject.tag == "Torch") {
			Playsound (2); //Sound '2' == "Torch"
		}
	}

	//Exit a Trigger     
	public void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.tag == "Hurt") 
		{
			anim.SetBool ("Hurt", false);
		}
	}


	public void Move(float move)
	{
        /*
        if(canMove)
            
            {
				horizontalMovement = Input.GetAxis ("Horizontal");
				
				//Move the Player
			    myRigidbody.velocity = new Vector2(horizontalMovement * moveSpeed, myRigidbody.velocity.y);
			    //anim.SetFloat ("Speed", horizontalMovement);

                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    anim.SetInteger("Direction", 0);
                    anim.SetFloat("MoveSpeed", 10);
                    
                }
                else if (Input.GetKey(KeyCode.RightArrow))
                {
                    anim.SetInteger("Direction", 1);
                    anim.SetFloat("MoveSpeed",10);
                }
                else {
                    anim.SetFloat("MoveSpeed", 0);
                }
            }
            */
    }
	//  SOUND
	void Playsound(int clip)
	{
		GetComponent<AudioSource>().clip = audioclip [clip];
		GetComponent<AudioSource>().Play ();
	}
}




