using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviour {

    public Button reloadButton, ShootButton;
	public GameObject BulletToRight, BulletToLeft;
	Vector2 bulletPosition;
	public float fireRate = 0.5f;
	float nextFire = 0.0f;
    float directionX;
    float newPositionX;
    float oldPositionX;
    
    private float horizontalMovement;
    public float moveSpeed = 5f;
    float counter;
    bool bulletAvailable = false;
	bool canMove = true;								// To disable Player Movement
	public bool facingright = false;							// start Facing to the Left
	Animator anim;										// Reference to the player's animator component
	Rigidbody2D myRigidbody;

	public AudioClip[] audioclip;						// Creates an Array to store my GameSounds
	public int curHealth;								// Number of Lifes left
	public int maxHealth = 17;							// maximun Life
	[SerializeField] int cells = 0;						// Number of Cells collected
	bool dead = false;
    public bool mustReload = false;
    public int bulletCounter = 30;
    public int energie = 0; 							//show me number of energie collected

    void Start()
	{
		anim = GetComponent<Animator>();
		myRigidbody = GetComponent<Rigidbody2D> ();
       // Button buttonReload = reloadButton.GetComponent<Button>();
       // buttonReload.onClick.AddListener(reloadButtonClicked);
        counter = 0.3f;
        StartCoroutine(move());
    }

	void Update() {
		if (curHealth > maxHealth) {
			curHealth = maxHealth;
		}

        counter -= Time.deltaTime;
        if(counter <= 0)
        {
            bulletAvailable = true;
        }
    }

	void FixedUpdate()
	{
        myRigidbody.velocity = new Vector2(directionX * moveSpeed, myRigidbody.velocity.y);
        if (curHealth <= 0) //The Death
		{
			GetComponent<Rigidbody2D>().velocity = (new Vector2 (0f, 0f));
			Debug.Log ("YOU ARE DEAD");
			canMove = false;
			anim.SetTrigger ("dead");
			Playsound(1); //Sound '1' == "game_over"
			Invoke ("freeze", 4);
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
			curHealth--;
			Playsound(0); //Sound '0' == "argh-woman"           
        }

        if (other.tag == "LifePickUp")
        {
            Destroy(other.gameObject);
            curHealth += 1;
			Playsound(5);   //Sound '5' == "LifePickUp"
        }

        if (other.gameObject.tag == "Torch") {
			Playsound (2); //Sound '2' == "Torch"
		}
		if (other.tag == "Energiecrystal") 
		{
			Destroy(other.gameObject);
			energie++;
			Playsound(4);   //Sound '4' == "coin9"
		}
	}
    
    private IEnumerator move()
    {
        while (true)
        {
            if (canMove)
            {
                directionX = CrossPlatformInputManager.GetAxis("Horizontal");
                newPositionX = transform.position.x;

                if (newPositionX < oldPositionX)
                {
                    facingright = false;
                    anim.SetInteger("Direction", 0);
                    anim.SetFloat("MoveSpeed", 1);
                }
                else if (newPositionX > oldPositionX)
                {
                    facingright = true;
                    anim.SetInteger("Direction", 1);
                    anim.SetFloat("MoveSpeed", 1);
                }
                else if (newPositionX == oldPositionX)
                {
                    anim.SetFloat("MoveSpeed", 0);
                }
                oldPositionX = newPositionX;
            }
            yield return new WaitForSecondsRealtime(0.1f);
        }
    }
    public void shoot()
    {
        if (bulletAvailable == true && mustReload == false)
        {
            anim.SetBool("shooting", true);
            anim.SetBool("weaponEmpty", false);
            bulletCounter -= 1;
            counter = 0.3f;

			bulletPosition = transform.position;
			if (facingright) {
				bulletPosition += new Vector2 (+1f, +0.43f);
				Instantiate (BulletToRight, bulletPosition, Quaternion.identity);
				Playsound (3); // sound '3' == "bullet"

			} else {
				bulletPosition += new Vector2 (-1f, +0.43F);
				Instantiate (BulletToLeft, bulletPosition, Quaternion.identity);
				Playsound (3); // sound '3' == "bullet"
			}
        }
        bulletAvailable = false;
    }

    public void stopShooting()
    {
        anim.SetBool("shooting", false);
    }

	private IEnumerator ReloadWeapon() {
        mustReload = true;
        canMove = false;
		ShootButton.enabled = false;
		anim.SetBool("shooting", false);
		anim.SetBool("weaponEmpty", true);
		anim.SetTrigger("reload");
		Playsound (6); // sound '6' == "reload"

		yield return new WaitForSecondsRealtime (1);
        mustReload = false;
        anim.SetBool ("weaponEmpty", false);
		bulletCounter = 30;
		ShootButton.enabled = true;
		canMove = true;
	}
    
    public void reloadButtonClicked()
    {
        if(bulletCounter < 30)
        {
            StartCoroutine (ReloadWeapon());
        }
    }
		
	public void Playsound(int clip)
	{
		GetComponent<AudioSource>().clip = audioclip [clip];
		GetComponent<AudioSource>().Play ();
	}
}