using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


	public class PlayerMovement : MonoBehaviour {


		bool facingRight = true;							// For determining which way the player is currently facing.
		public VirtualJoystick moveJoystick;
		

		[Range(0, 1)]

		bool canMove = true;								// To disable Player Movement
		Animator anim;										// Reference to the player's animator component.
		Rigidbody2D myRigidbody;

		// GameMenu
		//bool inGameMenu = false;							// sets GameMenu by Default to off.
		bool dead = false;	

		//Sound Array
		public AudioClip[] audioclip;						// Creates an Array to store my GameSounds

		[SerializeField] int lifes = 5;						// Number of Lifes left
		[SerializeField] int cells = 0;						// Number of Cells collected



		void Start()
		{
			anim = GetComponent<Animator>();
			myRigidbody = GetComponent<Rigidbody2D> ();
		}

		void FixedUpdate()
		{
			
			if (lifes < 0.5) //The Death
			{
				GetComponent<Rigidbody2D>().velocity = (new Vector2 (0f, 0f));
				Debug.Log ("YOU ARE DEAD");
				canMove = false;
				anim.SetBool ("Dead", true);
				Playsound(3);
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
				Playsound(0);
				Destroy(other.gameObject);
				cells++;
			}

			if (other.gameObject.tag == "Hurt") 
			{
				anim.SetBool ("Hurt", true);
				lifes--;
				Playsound(1);
				if(lifes>0)
				{
					GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 50f));
				}
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

			if(canMove)
			{

				Vector3 dir = Vector3.zero;

				dir.x = Input.GetAxis ("Horizontal");
				dir.y = Input.GetAxis ("Vertical");

				if (dir.magnitude > 1) {
					dir.Normalize ();
				}

				if (move > 0 && !facingRight) {
					Flip ();
				} else if (move < 0 && facingRight) {
					Flip ();
				}

				if (moveJoystick.InputDirection != Vector3.zero) {
					dir = moveJoystick.InputDirection;
				}

				//Move the Player
			myRigidbody.velocity = new Vector2(dir.x*20, myRigidbody.velocity.y);
			anim.SetFloat ("speed", dir.x);


			}
		}

		void Flip ()
		{
			// Switch the way the player is labelled as facing.
			facingRight = !facingRight;

			// Multiply the player's x local scale by -1.
			Vector3 theScale = transform.localScale;
			theScale.x *= -1;
			transform.localScale = theScale;
		}

		//  SOUND
		void Playsound(int clip)
		{
			GetComponent<AudioSource>().clip = audioclip [clip];
			GetComponent<AudioSource>().Play ();
		}


		void OnGUI()
		{
			GUILayout.Label( "Energiezellen = " + cells );
			GUILayout.Label( "Leben = " + lifes );

			if (lifes < 0 || dead) 
			{
				GUILayout.Label("YOU ARE DEAD !!");
			}
		}
	}




