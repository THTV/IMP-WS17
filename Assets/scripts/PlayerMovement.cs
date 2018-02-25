using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviour {

    public Button reloadButton;
    float directionX;
    private float horizontalMovement;
    public float moveSpeed = 5f;
    float counter;
    bool bulletAvailable = false;

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
    public int bulletCounter = 30;
    [SerializeField] int energie = 0; //show me number of energie collected



    void Start()
	{
		anim = GetComponent<Animator>();
		myRigidbody = GetComponent<Rigidbody2D> ();
        Button buttonReload = reloadButton.GetComponent<Button>();
        buttonReload.onClick.AddListener(reloadButtonClicked);
        counter = 0.3f;
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
            
        }
        counter -= Time.deltaTime;
        if(counter <= 0)
        {
            bulletAvailable = true;
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
    
    public void shoot()
    {
        Debug.Log("DOWNDOWNDOWN");
        anim.SetBool("shooting", true);
        anim.SetBool("weaponEmpty", false);
        if (bulletAvailable == true)
        {
            bulletCounter -= 1;
            counter = 0.3f;
        }
        bulletAvailable = false;


    }
    public void stopShooting()
    {
        anim.SetBool("shooting", false);
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
        GUILayout.Label("Energiezellen = " + energie);
    }
}




