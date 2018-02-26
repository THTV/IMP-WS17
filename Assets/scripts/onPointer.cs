using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class onPointer : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

    private bool isDown = false;
    private PlayerMovement playerMovement;
    private int bulletCounter;
   

    private void Start()
    {
        //playerMovement = gameObject.GetComponent<PlayerMovement>();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }
    // Update is called once per frame
    void Update () {
        bulletCounter = playerMovement.bulletCounter;
        if (isDown)
        {
            Debug.Log(bulletCounter);
            if(bulletCounter > 0)
            {
                playerMovement.shoot();
            }
            else
            {
                playerMovement.stopShooting();
            }
            
        }
        else
        {
            playerMovement.stopShooting();
        }
	}

    

    public void OnPointerDown(PointerEventData data)
    {
        Debug.Log("Button is being held down");
        isDown = true;
    }
    public void OnPointerUp(PointerEventData data)
    {
        Debug.Log("Button is being released now");
        isDown = false;
    }
}
