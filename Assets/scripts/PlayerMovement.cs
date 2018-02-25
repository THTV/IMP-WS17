using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviour {

    public Button shootButton;
    public Button reloadButton;
    float directionX;
    private float horizontalMovement;
    public float moveSpeed = 5f;

	bool canMove = true;								// To disable Player Movement
	Animator anim;										// Reference to the player's animator component.
	Rigidbody2D myRigidbody;

	//Sound Array
	public AudioClip[] audioclip;						// Creates an Array to store my GameSounds

	public int curHealth;								// Number of Lifes left
	public int maxHealth = 17;							// maximun Life
	[SerializeField] int cells = 0;						// Number of Cells collected
	bool dead = false;
    bool mustReload = false;
    //bool bulletEmpty = false;
    float countdownShooting = 10f;
    //int bulletCounter = 30;
    [SerializeField] int bulletCounter = 30;



    void Start()
	{
		anim = GetComponent<Animator>();
		myRigidbody = GetComponent<Rigidbody2D> ();
        Button buttonShoot = shootButton.GetComponent<Button>();
        Button buttonReload = reloadButton.GetComponent<Button>();
        buttonShoot.onClick.AddListener(shootButtonPressed);      //mit onPointerDown ?
        buttonReload.onClick.AddListener(reloadButtonClicked);
        
        
	}

	void Update() {
		if (curHealth > maxHealth) {
			curHealth = maxHealth;
		}
        if (canMove)
        {
            directionX = CrossPlatformInputManager.GetAxis("Horizontal");
            if(directionX < 0)
            {
                Debug.Log("LINKS");
                anim.SetInteger("Direction", 0);
                anim.SetFloat("MoveSpeed", 1);
            }
            else if (directionX > 0)
            {
                Debug.Log("Rechts");
                anim.SetInteger("Direction", 1);
                anim.SetFloat("MoveSpeed", 1);
            }
            else
            {
                anim.SetFloat("MoveSpeed", 0);
            }
            if(!mustReload)
            {
                countdownShooting -= Time.deltaTime;
                
            }
        }
    }

	void FixedUpdate()
	{
        myRigidbody.velocity = new Vector2(directionX * moveSpeed * 30, myRigidbody.velocity.y);
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
		if (other.gameObject.tag == "Hurt" || other.gameObject.tag == "Enemy") 
		{
			//anim.SetBool ("Hurt", true);
			curHealth--;
			Playsound(0); //Sound '0' == "argh-woman"
			if(curHealth>0)
			{
				GetComponent<Rigidbody2D>().AddForce(new Vector2(10f, 50f));
			}
            if (other.tag == "LifePickUp")
            {
                Destroy(other.gameObject);
                curHealth += 1;
            }
        }

		if (other.gameObject.tag == "Torch") {
			Playsound (2); //Sound '2' == "Torch"
		}
	}

	//Exit a Trigger     
	public void OnTriggerExit2D(Collider2D other)
	{
		
	}

    void shootButtonPressed()
    {
        if (!mustReload)
        {
            bulletCounter -= 1;
            //countdownShooting = 10f;
            anim.SetBool("shooting", true);
            anim.SetBool("weaponEmpty", false);
            
            if (bulletCounter == 0)
            {
                anim.SetBool("shooting", false);
                anim.SetBool("weaponEmpty", true);
            }
        }
    }
    void reloadButtonClicked()
    {
        anim.SetBool("shooting", false);
        anim.SetBool("weaponEmpty", true);
        anim.SetTrigger("reload");
        bulletCounter = 30;
    }

	
	//  SOUND
	void Playsound(int clip)
	{
		GetComponent<AudioSource>().clip = audioclip [clip];
		GetComponent<AudioSource>().Play ();
	}

    private void OnGUI()
    {
        GUILayout.Label("Bullets = " + bulletCounter);
    }
}




